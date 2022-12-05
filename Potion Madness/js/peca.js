
//classe da peça
class Piece {
  x;
  y;
  color;
  shape;
  ctx;
  typeId;

  constructor(ctx) {
    this.ctx = ctx;
    this.spawn();
  }
  //gerar a peça

  spawn() {
    this.typeId = this.randomizeTetrominoType(COLORS.length - 1);
    this.shape = SHAPES[this.typeId];
    this.color = COLORS[this.typeId];
    this.x = 0;
    this.y = 0;
  }
  // ficar estatico 

  draw() {
    this.ctx.fillStyle = this.color;
    this.shape.forEach((row, y) => {
      row.forEach((value, x) => {
        if (value > 0) {
				this.ctx.fillRect(this.x + x, this.y + y, 1, 1);

        }
      });
    });
  }
  //mover a peçar

  move(p) {
    this.x = p.x;
    this.y = p.y;
    this.shape = p.shape;
  }

// inicio por onde a peça vai começar
  setStartingPosition() {
    this.x = this.typeId === 4 ? 4 : 3;
  }
 
 //vai gerando a peça automaticamente.

  randomizeTetrominoType(noOfTypes) {
    return Math.floor(Math.random() * noOfTypes + 1);
  }
}
