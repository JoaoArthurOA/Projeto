using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zaskar : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 70;
    int currentHealth;

    public bool Pisando;
    public LayerMask LayerChao;
    public GameObject Chao;
    public Transform attackPointz;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 20;
    private Rigidbody2D rig;
    public bool direcao;

    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public float combo = 0f;

    public GameObject HPZ;
    public GameObject winh;
    public float tempWinz = 0;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            tempWinz += Time.deltaTime;
            if (tempWinz >= 4)
            {
                winh.gameObject.SetActive(true);
                canvas.gameObject.SetActive(true);
            }
        }

        if (currentHealth == 60)
        {
            HPZ.GetComponent<HPZ>().Animaco.SetBool("HPZ1", true);
        }

        if (currentHealth == 50)
        {
            HPZ.GetComponent<HPZ>().Animaco.SetBool("HPZ2", true);
            HPZ.GetComponent<HPZ>().Animaco.SetBool("HPZ1", false);
        }

        if (currentHealth == 40)
        {
            HPZ.GetComponent<HPZ>().Animaco.SetBool("HPZ3", true);
            HPZ.GetComponent<HPZ>().Animaco.SetBool("HPZ2", false);
        }

        if (currentHealth == 30)
        {
            HPZ.GetComponent<HPZ>().Animaco.SetBool("HPZ4", true);
            HPZ.GetComponent<HPZ>().Animaco.SetBool("HPZ3", false);
            HPZ.GetComponent<HPZ>().Animaco.SetBool("HPZ2", false);
        }

        if (currentHealth == 20)
        {
            HPZ.GetComponent<HPZ>().Animaco.SetBool("HPZ5", true);
            HPZ.GetComponent<HPZ>().Animaco.SetBool("HPZ4", false);
        }

        if (currentHealth == 10)
        {
            HPZ.GetComponent<HPZ>().Animaco.SetBool("HPZ6", true);
            HPZ.GetComponent<HPZ>().Animaco.SetBool("HPZ5", false);
            HPZ.GetComponent<HPZ>().Animaco.SetBool("HPZ4", false);
        }

        if (currentHealth == 0)
        {
            HPZ.GetComponent<HPZ>().Animaco.SetBool("HPZ7", true);
            HPZ.GetComponent<HPZ>().Animaco.SetBool("HPZ6", false);
        }

        Pisando = Physics2D.OverlapCircle(Chao.transform.position, 2f, LayerChao);
        animator = gameObject.GetComponent<Animator>();

        if (Input.GetAxisRaw("Movez") > 0f)
        {
            animator.SetBool("Andarz", true);
            gameObject.transform.Translate(new Vector2(0.01f, 0));
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            direcao = false;
        }

        if (Input.GetAxisRaw("Movez") < 0f)
        {
            animator.SetBool("Andarz", true);
            gameObject.transform.Translate(new Vector2(0.01f, 0));
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            direcao = true;
        }

        if (Input.GetAxisRaw("Movez") == 0f)
        {
            animator.SetBool("Andarz", false);
        }

        if (Input.GetButtonDown("Jumpz"))
        {


            if (Pisando == true)
            {
                rig.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);

            }



        }

        if (Pisando == true)
        {
            animator.SetBool("Pularz", false);
        }
        if (Pisando == false)
        {
            animator.SetBool("Pularz", true);
            animator.SetBool("Andarz", false);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -4));
        }

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Meleez") && Pisando == true)
            {
                combo++;
                if (combo == 1)
                {
                    Ataque();

                }
                if (combo == 2)
                {
                    AtaqueB();
                    nextAttackTime = Time.time + 1f / attackRate;
                    combo++;

                }
           
            }
        }
        if (combo == 3)
        {
            combo = 0;
        }

        void Ataque()
        {
            animator.SetTrigger("Ataquez");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointz.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Hadarai>().TakeDamage(attackDamage);
            }
        }

        void AtaqueB()
        {
            animator.SetTrigger("AtaqueBz");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointz.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Hadarai>().TakeDamage(attackDamage);
            }
        }

   
    }
    void OnDrawGizmosSelected()
    {
        if (attackPointz == null)
            return;
        Gizmos.DrawWireSphere(attackPointz.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died");

        animator.SetBool("Death", true);

        


    }
}

