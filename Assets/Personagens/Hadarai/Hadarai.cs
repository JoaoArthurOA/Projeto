using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hadarai : MonoBehaviour
{
    public Animator Animacao;
    public bool Pisando;
    public LayerMask LayerChao;
    public GameObject Chao;
    public GameObject Spec;
    public GameObject InstSpec;
    public float TimeSpec;
    public bool IsSpec;
    public bool SpecNow;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 10;
    private Rigidbody2D rig;
    public bool direcao;

    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public float combo = 0f;
    public int maxHealth = 70;
    public int currentHealth;

    public GameObject HPH;
    public GameObject winz;
    public GameObject canvas;

    public float tempWinh = 0;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
       
        Animacao = gameObject.GetComponent<Animator>();

        Pisando = Physics2D.OverlapCircle (Chao.transform.position, 2f, LayerChao); 

        if ( currentHealth <= 0)
        {
            tempWinh += Time.deltaTime;
            if (tempWinh >= 4)
            {
                winz.gameObject.SetActive(true);
                canvas.gameObject.SetActive(true);
            }
        }

        if ( currentHealth == 60)
        {
            HPH.GetComponent<HP>().Animaco.SetBool("HP1", true);
        }

        if (currentHealth == 50)
        {
            HPH.GetComponent<HP>().Animaco.SetBool("HP2", true);
            HPH.GetComponent<HP>().Animaco.SetBool("HP1", false);
        }

        if (currentHealth == 40)
        {
            HPH.GetComponent<HP>().Animaco.SetBool("HP3", true);
            HPH.GetComponent<HP>().Animaco.SetBool("HP2", false);
        }

        if (currentHealth == 30)
        {
            HPH.GetComponent<HP>().Animaco.SetBool("HP4", true);
            HPH.GetComponent<HP>().Animaco.SetBool("HP3", false);
            HPH.GetComponent<HP>().Animaco.SetBool("HP2", false);
        }

        if (currentHealth == 20)
        {
            HPH.GetComponent<HP>().Animaco.SetBool("HP5", true);
            HPH.GetComponent<HP>().Animaco.SetBool("HP4", false);
        }

        if (currentHealth == 10)
        {
            HPH.GetComponent<HP>().Animaco.SetBool("HP6", true);
            HPH.GetComponent<HP>().Animaco.SetBool("HP5", false);
            HPH.GetComponent<HP>().Animaco.SetBool("HP4", false);
        }

        if (currentHealth == -10 )
        {
            HPH.GetComponent<HP>().Animaco.SetBool("HP7", true);
            HPH.GetComponent<HP>().Animaco.SetBool("HP6", false);
        }
        //Andar
        if (Input.GetAxisRaw ("Horizontal") > 0f) 
        {
            Animacao.SetBool("Andar", true);
            gameObject.transform.Translate(new Vector2 ( 0.01f, 0));
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            direcao = false;
        }

        if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            Animacao.SetBool("Andar", true);
            gameObject.transform.Translate(new Vector2( 0.01f, 0));
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            direcao = true;
        }

        if (Input.GetAxisRaw("Horizontal") == 0f)
        {
            Animacao.SetBool("Andar", false);
        }

        //Pular
        if (Input.GetButtonDown("Vertical"))
        {
            

            if (Pisando == true)
            {
                rig.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
                
            }
            

        }

       if ( Pisando == true)
        {
            Animacao.SetBool("Pular", false);
        }
       if ( Pisando == false)
        {
            Animacao.SetBool("Pular", true);
            Animacao.SetBool("Andar", false);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -4));
        }

       //Especial
        if (Pisando == true)
        {


            if (Input.GetButtonDown("Special"))
            {
                Animacao.SetBool("Spec", true);
                IsSpec = true;
                
            }
            else
            {
                Animacao.SetBool("Spec", false);
            }
        }

        if(SpecNow == true )
        {
            if (direcao == false)
            {
                InstSpec = (GameObject)Instantiate(Spec, gameObject.transform.position, Quaternion.identity);
                InstSpec.GetComponent<Rigidbody2D>().AddForce(new Vector2(700f, 0));
                IsSpec = false;
                TimeSpec = 0;
                SpecNow = false;

            }

            if( direcao == true )
            {
                InstSpec = (GameObject)Instantiate(Spec, gameObject.transform.position, Quaternion.identity);
                InstSpec.GetComponent<Rigidbody2D>().AddForce(new Vector2( -700f, 0));
                IsSpec = false;
                TimeSpec = 0;
                SpecNow = false;
                
            }
        }

     

        if (IsSpec == true)
        {
            TimeSpec += Time.deltaTime;
            if(TimeSpec >= 0.4f)
            {
                SpecNow = true;
            }
        }

        //Combate Corpo-a-Corpo

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Melee") && Pisando == true)
            {
                combo++;
                if (combo == 1)
                {
                    Ataque();
                   
                }
                if (combo == 2)
                {
                    AtaqueB();
                    
                    
                }
                if (combo == 3)
                {
                    AtaqueC();
                    nextAttackTime = Time.time + 1f / attackRate;
                    combo++;
                    
                }
            }         
        }
       if ( combo == 4)
        {
            combo = 0;
        }
        
       void Ataque()
        {
          Animacao.SetTrigger("Ataque");

          Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Zaskar>().TakeDamage(attackDamage);
            }
        }

        void AtaqueB()
        {
            Animacao.SetTrigger("AtaqueB");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Zaskar>().TakeDamage(attackDamage);
            }
        }

        void AtaqueC()
        {
            Animacao.SetTrigger("AtaqueC");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Zaskar>().TakeDamage(attackDamage);
            }
        }


    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Animacao.SetTrigger("Hurth");

        if (currentHealth <= 0)
        {
            Die();
        }

   
      
    }

    void Die()
    {
        Debug.Log("Enemy died");

        Animacao.SetBool("Deathh", true);

        


    }

    

}
