using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public float maxMagic = 10f;
  
    public Item currentItem;
    public List<Item> items= new List<Item>();
    public int numberOfKeys;
    public int numberOfCoins;
    public bool CheckForItem(Item item)
    {
        if (items.Contains(item))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void Start()
    {
        ClearCurrentItem();
    }
    public void AddCoin()
    {
        numberOfCoins += 1;

    }
   
    public void AddItem(Item itemToAdd)
        
    {

        currentItem = itemToAdd;
       
        if (itemToAdd.isKey==true)
        {
            numberOfKeys++;
        }
        else
        {
            if (!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
       
    }
    public void ClearCurrentItem()
    {
        currentItem = null;
    }
}
