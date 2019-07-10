using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeartMananger : MonoBehaviour
{
    Animator playerAnim;
    public float heartContainerValue = 2f;
    public FloatValue playerCurrentHealth;
    public FloatValue heartContainers;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    bool heartFound = false;
    public float tempHealth;
    // Start is called before the first frame update
    private void Update()
    {
      //  Debug.Log("HeartContainersAre:" + heartContainers.RuntimeValue);
       // Debug.Log("Health:" + playerCurrentHealth.RuntimeValue);
    }
    void Start()
    {
        heartContainers.RuntimeValue = playerCurrentHealth.initialValue / heartContainerValue;
        InitHearts();
    }

    public void InitHearts()
    {
        for (int i = 0; i < heartContainers.RuntimeValue; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite=fullHeart;

        }


    }
  public void AddHeart()
    {
        heartContainers.RuntimeValue++;
        InitHearts();
        playerCurrentHealth.RuntimeValue =heartContainers.RuntimeValue*heartContainerValue;
       
       


      
    }
    public void UpdateHearts()
    {

        tempHealth = playerCurrentHealth.RuntimeValue;
        for (int i = 0; i <= heartContainers.RuntimeValue; i++)
        {
            if (i * 2 < tempHealth - 1)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (i * 2 == tempHealth-1)
            {
                hearts[i].sprite = halfFullHeart;
            }
            else if (i * 2 > tempHealth-1)
            {
                hearts[i].sprite = emptyHeart;
            }
            
            





        }




        //
        /*
         float tempHealth = playerCurrentHealth.RuntimeValue ;
         for (int i =0; i <heartContainers.RuntimeValue; i++)
         {

             if (tempHealth>=i*2)
             {

                 hearts[i].sprite = fullHeart;
                 //fullHeart
             }
             else if (tempHealth<=i*2)
             {
                 hearts[i].sprite = emptyHeart;
                 //empty heart
             }
             else 
             {
                 hearts[i].sprite = halfFullHeart;
                 //half heart
             }
         }
          
        heartFound = false;
        for (float i = playerCurrentHealth.RuntimeValue / 2; i >=0; i--)
        {
            if (heartFound == false)
            {
                if (hearts[(int)i].sprite == emptyHeart)
                {

                }
                else if (hearts[(int)i].sprite == halfFullHeart)
                {
                    heartFound = true;
                    hearts[(int)i].sprite = emptyHeart;
                }
                else if (hearts[(int)i].sprite == fullHeart)
                {
                    heartFound = true;
                    hearts[(int)i].sprite = halfFullHeart;
                }
            }


        }
        */
    }
}




