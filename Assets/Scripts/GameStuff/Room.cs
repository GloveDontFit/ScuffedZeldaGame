using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Enemy[] enemies;
    public Pot[] pots;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&&!other.isTrigger)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
            }
            for (int j = 0; j < pots.Length; j++)
            {
                ChangeActivation(pots[j], true);
            }
            //activate all enemies
            //activate all pots
        }
    }
   

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
            }
            for (int j = 0; j < pots.Length; j++)
            {
                ChangeActivation(pots[j], false);
            }
        }
    }
    public void ChangeActivation(Component component, bool activation)
    {
        component.gameObject.SetActive(activation);
    }
}

