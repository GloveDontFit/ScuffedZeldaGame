using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAmmunition : MonoBehaviour
{
    Rigidbody2D rb;
   public float speed;
    Vector3 direction;
    GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        
        
        rb = GetComponent<Rigidbody2D>();

        
        target = FindObjectOfType<PlayerMovement>().gameObject;
        direction = GetComponentInParent<EnemyRange>().GiveDirection();
       

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
       
        transform.position += direction * speed * Time.deltaTime;
        Destroy(gameObject, 2f);
    } 
}
