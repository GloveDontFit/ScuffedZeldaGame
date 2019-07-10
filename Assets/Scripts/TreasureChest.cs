using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TreasureChest : Interactable
{
    [Header("Contents")]
    public Inventory playerInventory;
    public BoolValue storedOpen;
       bool contextOff=false;
    [Header("Signals And Dialog")]
    public float dialogDelay;
    public Text dialogText;
    public GameObject dialogBox;
    public Signal raiseItem;
    public Item contents;
    public bool isOpen;
    string itemDescription;
    [Header("Animator")]
    Animator anim;
    // Start is called before the first frame update
    
    
    void Start()
    {
        itemDescription = contents.itemDescription;
        anim = GetComponent<Animator>();
        isOpen = storedOpen.RuntimeValue;
        if (isOpen == true)
        {
            anim.SetTrigger("Open");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && inRange == true)
        {
            if (isOpen==false)
            {
                OpenChest();
            }
            else
            {
                ChestIsAlreadyOpen();
               
            }
            
        }
        else if(inRange==false)
        {

            
            //chest is already open
        }
    }
    public void ChestIsAlreadyOpen()
    {
       
      
        raiseItem.Raise();

        dialogBox.SetActive(false);
        inRange = false;
    }
    private void OpenChest()
    {
       
        isOpen = true;
        storedOpen.RuntimeValue = isOpen;
        anim.SetTrigger("Open");
        playerInventory.AddItem(contents);
        context.Raise();
        raiseItem.Raise();
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger && isOpen == false)
        {
            
                inRange = true;
                context.Raise();
            
           
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger&&isOpen==false)
        {
            
                inRange = false;
                context.Raise();

           

        }
    }

}
