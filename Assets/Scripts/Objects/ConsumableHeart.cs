using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableHeart : Powerup
{
    public FloatValue heartContainers;
    public FloatValue playerHealth;
    public float amountToIncrease;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&collision.isTrigger)
        {
            playerHealth.RuntimeValue += amountToIncrease;
            if (playerHealth.RuntimeValue>heartContainers.RuntimeValue*2)
            {
                playerHealth.RuntimeValue = heartContainers.RuntimeValue * 2;
            }
            powerupSignal.Raise();
            Destroy(gameObject);
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
