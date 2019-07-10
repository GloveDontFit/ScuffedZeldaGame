using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBottle : Powerup
{
    public FloatValue currentMana;
    public float powerupAmount;
    public Signal raiseMagic;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.isTrigger)
        {
            currentMana.RuntimeValue += powerupAmount;
            raiseMagic.Raise();
            powerupSignal.Raise();
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
