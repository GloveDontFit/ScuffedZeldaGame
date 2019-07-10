using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{

    public float damage;
    public float knocktime;
    public float thrust;
    float defaultSpeed;
    GameObject enemyObject;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Breakable")&&this.gameObject.CompareTag("WeaponHitBox"))
        {
            if (collision!=null)
            {
                Debug.Log("Smash!");
                collision.GetComponent<Pot>().Smash();
            }
            
        }
        
        if (collision.gameObject.CompareTag("Enemy")|| collision.gameObject.CompareTag("Player"))
        {

            
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();
           


            if (hit != null)
            {
                Vector2 difference = (Vector2)hit.transform.position - (Vector2)transform.position;
                difference = difference.normalized * thrust;

                hit.AddForce(difference, ForceMode2D.Impulse) ;
                if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    Debug.Log("EnemyStaggered");
                    collision.GetComponent<Enemy>().knock(hit, knocktime,damage);
                  

                }
                if (collision.gameObject.CompareTag("Player") && collision.isTrigger)
                {

                    if (collision.GetComponent<PlayerMovement>().currentState != PlayerMovement.PlayerState.stagger)
                    {
                       
                        hit.GetComponent<PlayerMovement>().currentState = PlayerMovement.PlayerState.stagger;
                        collision.GetComponent<PlayerMovement>().knock(hit, knocktime, damage);
                    }
                   

                   

                }

               
            }

                


            }
           
        }
    }
    
   
   


