using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeartContainer : Powerup
{
    public Signal RaiseItem;
    public Signal recievedHeart;
    [Header("Contents")]
   public Inventory playerInventory;
    
    bool contextOff = false;
    [Header("Signals And Dialog")]
    public float dialogDelay;
    public Text dialogText;
    public GameObject dialogBox;
  public  bool canTrigger = true;
    public Item contents;
    public bool isOpen;
    string itemDescription;
    [Header("Animator")]
    Animator anim;
    // Start is called before the first frame update


    void Start()
    {
        itemDescription = contents.itemDescription;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && canTrigger == false)
        {
            
            dialogBox.SetActive(false);
            RaiseItem.Raise();
            Destroy(gameObject);

        }
    }
   
    private void RaiseHeart()
    {




        playerInventory.AddItem(contents);
        recievedHeart.Raise();
        RaiseItem.Raise();
        dialogBox.SetActive(true);
        StartCoroutine(ShowDialogText());

    }
    IEnumerator ShowDialogText()
    {
        for (int i = 0; i < itemDescription.Length; i++)
        {
            string currentText = itemDescription.Substring(0, i);
            dialogText.text = currentText;
            yield return new WaitForSeconds(dialogDelay);
         
        }
       
       
       
    }
    

   
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&collision.isTrigger)
        {
           
            if (canTrigger==true)
            {
                RaiseHeart();
               
                GetComponent<SpriteRenderer>().enabled = false;
                canTrigger = false;
                
            }
            
        }
      
    }
   
}
