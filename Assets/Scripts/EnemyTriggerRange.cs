using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerRange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            GetComponentInParent<EnemyRange>().isShooting = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            GetComponentInParent<EnemyRange>().isShooting = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
