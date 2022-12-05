// declarando as variaveis para o inicio do jogo

const canvas = document.getElementById('board');
const ctx = canvas.getContext('2d');
const canvasNext = document.getElementById('next');
const ctxNext = canvasNext.getContext('2d');

// declarando os pontos , niveis e linhas do jogo

let accountValues = {
  score: 0,
  level: 0,
  lines: 0
}

// função que vai mudando os pontos, niveis, linhas 

function updateAccount(key, value) {
  let element = document.getElementById(key);
  if (element) {
    element.textContent = value;
  }
}

// chamando as peças proxy é um objeto que é usado para customizar, chamar, atrinuir algo
let account = new Proxy(accountValues, {
  set: (target, key, value) => {
    target[key] = value;
    updateAccount(key, value);
    return true;
  }
});

//  declarando request que vai isar o ID como referencia 

let requestId;

moves = {
  [KEY.DOWN]: p => ({ ...p, x: p.x - 1 }),
  [KEY.UP]: p => ({ ...p, x: p.x + 1 }),
  [KEY.RIGHT]: p => ({ ...p, y: p.y + 1 }),
  [KEY.SPACE]: p => ({ ...p, y: p.y + 1 }),
  [KEY.LEFT]: p => board.rotate(p)
};

// declarando o quadro para iniciar

let board = new Board(ctx, ctxNext);
addEventListener();
initNext();

// chamando o proximo objeto da lista

function initNext() {
  // Calculate size of canvas from constants.
  ctxNext.canvas.width = 4 * BLOCK_SIZE;
  ctxNext.canvas.height = 4 * BLOCK_SIZE;
  ctxNext.scale(BLOCK_SIZE, BLOCK_SIZE);
}

// função que vai movimentando as peças do jogo

function addEventListener() {
  document.addEventListener('keydown', event => {
    if (event.keyCode === KEY.P) {
      pause();
    }
    if (event.keyCode === KEY.ESC) {
      gameOver();
    } else if (moves[event.keyCode]) {
      event.preventDefault();
      // Get new state
      let p = moves[event.keyCode](board.piece);
      if (event.keyCode === KEY.SPACE) {
        // Hard drop
        while (board.valid(p)) {
          account.score += POINTS.HARD_DROP;
          board.piece.move(p);
          p = moves[KEY.SPACE](board.piece);
        }       
      } else if (board.valid(p)) {
        board.piece.move(p);
        if (event.keyCode === KEY.LEFT) {
          account.score += POINTS.SOFT_DROP;         
        }
      }
    }
  });
}

// função para resetar o jogo

function resetGame() {
  account.score = 0;
  account.lines = 0;
  account.level = 0;
  board.reset();
  time = { start: 0, elapsed: 0, level: LEVEL[account.level] };
}

// função que inicia o jogo

function play() {
  resetGame();
  time.start = performance.now();
  // If we have an old game running a game then cancel the old
  if (requestId) {
    cancelAnimationFrame(requestId);
  }

  animate();
}

// funçao para animação do jogo

function animate(now = 0) {
  time.elapsed = now - time.start;
  if (time.elapsed > time.level) {
    time.start = now;
    if (!board.drop()) {
      gameOver();
      return;
    }
  }

  //limpar o quadro para iniciar novamente o jogo.
  ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height);

  board.draw();
  requestId = requestAnimationFrame(animate);
}

 //função quando perde o jogo com o nome pode mudar para o que quiser tamanho e cor
function gameOver() {
  cancelAnimationFrame(requestId);
  ctx.fillStyle = 'yellow';
  ctx.fillRect(1, 3, 8, 1.2);
  ctx.font = '1px Arial';
  ctx.fillStyle = 'black';
  ctx.fillText('PLAY NEW', 1.8, 4);
}

// função para pausar o jogo
function pause() {
  if (!requestId) {
    animate();
    return;
  }

  cancelAnimationFrame(requestId);
  requestId = null;
  
  ctx.fillStyle = 'black';
  ctx.fillRect(1, 3, 8, 1.2);
  ctx.font = '1px Arial';
  ctx.fillStyle = 'yellow';
  ctx.fillText('PAUSED', 3, 4);
}

