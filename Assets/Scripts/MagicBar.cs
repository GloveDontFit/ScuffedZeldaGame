using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MagicBar : MonoBehaviour
{
    public FloatValue equipedManaCost;
    public FloatValue currentMagicAmount;
    public Slider magicSlider;
    public Inventory playerInventory;
    // Start is called before the first frame update
    void Start()
    {
        magicSlider.maxValue = playerInventory.maxMagic;
        magicSlider.value = currentMagicAmount.RuntimeValue;
    }
    public void UpdateSlider()
    {
        magicSlider.value = currentMagicAmount.RuntimeValue;
    }
    public void IncreaseMagic()
    {
       
        if (currentMagicAmount.RuntimeValue==playerInventory.maxMagic)
        {
            currentMagicAmount.RuntimeValue = playerInventory.maxMagic;
        }
        UpdateSlider();
    }
    public void DecreaseMagic()
    {
        Debug.Log("Decreasing");
        if (currentMagicAmount.RuntimeValue!=0)
        {
            currentMagicAmount.RuntimeValue -= equipedManaCost.RuntimeValue;
           
        }
        UpdateSlider();

    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
