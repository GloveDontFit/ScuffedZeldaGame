using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBackFix : MonoBehaviour
{
   public Collider2D parent;
   public Collider2D child;
    Rigidbody2D rb;
   
    // Start is called before the first frame update
    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(child,parent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
