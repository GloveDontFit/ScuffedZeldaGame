using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinManager : MonoBehaviour
{
    public Inventory playerInventory;
    public Text coinAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisplayCoins()
    {
         coinAmount.text = playerInventory.numberOfCoins.ToString();
    }
}
