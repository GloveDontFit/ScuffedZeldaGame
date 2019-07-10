using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Powerup
{
    public Signal recievedCoin;
    public Inventory playerInventory;
    public FloatValue playerCoins;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&collision.isTrigger)
        {

            playerInventory.AddCoin();
            recievedCoin.Raise();
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
