using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyRoom : DungeonRoom
{
    
    int numberOfEnemies;
    public GameObject[] doors; 
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void CloseAllDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            
            doors[i].gameObject.SetActive(true);
        }
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            Debug.Log("InRoom");
            
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
            StartCoroutine(CloseDoors());
        }
    }
    IEnumerator CloseDoors()
    {
        yield return new WaitForSeconds(1f);
        CloseAllDoors();
    }


    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
               // enemies[i].gameObject.SetActive(false);
            }
            for (int j = 0; j < pots.Length; j++)
            {
                ChangeActivation(pots[j], false);
            }
        }
    }
    public void CheckEnemies()
    {
        
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.activeInHierarchy)
            {
                return;
               
            }
           
           
        }
       
            OpenAllDoors();
        
       
       
    }
   
    public void OpenAllDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
