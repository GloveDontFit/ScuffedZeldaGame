using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public Powerup thisLoot;
    public int lootChance;
  
}
[CreateAssetMenu]
public class LootTable : ScriptableObject
{

    public Loot[] lootItems;
  public Powerup LootPowerUp()
    {
        Random rng = new Random();
        int cumlativeProb = 0;
        int currentProb = Random.Range(0, 100);
        for (int i = 0; i < lootItems.Length; i++)
        {
            cumlativeProb += lootItems[i].lootChance;
            if (currentProb<=cumlativeProb)
            {
                return lootItems[i].thisLoot;
            }
        }
        return null;
    }
    
    
}
