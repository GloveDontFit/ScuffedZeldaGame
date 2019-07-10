using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 direction;
    public float manaCost;
    public float speed;
    public float lifetime;
    public float lifetimeCounter;
    // Start is called before the first frame update
    void Start()
    {
        lifetimeCounter = lifetime;
        rb = GetComponent<Rigidbody2D>();
    }
   
    // Update is called once per frame
    public void Setup(Vector2 velocity,Vector3 direction)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
       
    }
    void Update()
    {


        
        lifetimeCounter -= Time.deltaTime;
        if (lifetimeCounter<=0)
        {
            Destroy(gameObject);
        }
       
    }
}
