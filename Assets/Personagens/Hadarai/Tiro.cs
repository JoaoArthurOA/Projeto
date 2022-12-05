using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour
{
    private BoxCollider2D hadaraiCollider, tiroCollider;
    public int attackDamage = 10;
    // Start is called before the first frame update
    void Start()
    {
        tiroCollider = GetComponent<BoxCollider2D>();
        hadaraiCollider = GameObject.Find("Hadarai").GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(tiroCollider, hadaraiCollider, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Zaskar>(out Zaskar zaskarComponent))
        {
            zaskarComponent.TakeDamage(attackDamage);
            
        }

        Destroy(gameObject);





    }
}
