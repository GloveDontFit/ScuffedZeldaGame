using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public Powerup droppedLoot;
    public LootTable lootTable;
    bool potDestroyed=false;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Smash()
    {
        if (potDestroyed == false)
        {
            potDestroyed = true;
            anim.SetTrigger("Break");
            DropLoot();
           Destroy( gameObject.GetComponent<Collider2D>());
        }
      
    }
    public void DropLoot()
    {
        if (lootTable != null)
        {
            droppedLoot = lootTable.LootPowerUp();
            if (droppedLoot != null)
            {
                Instantiate(droppedLoot, transform.position, Quaternion.identity);
            }
        }


    }
    public void DestroyPot()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
