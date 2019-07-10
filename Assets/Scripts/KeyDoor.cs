using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : Interactable
{
    public enum DoorType
    {
        Key,
        Enemy,
        Button
    }
    [Header("DoorVariables")]
    public Inventory playerInventory;
    public DoorType thisDoorType;
    public bool open=false;
    public SpriteRenderer doorSprite;
    public BoxCollider2D phyicsCollider;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.isTrigger && open == false)
        {
            inRange = true;
            context.Raise();
           
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&&other.isTrigger&&open==false)
        {
            inRange = false;
            context.Raise();

        }

    }
    // Start is called before the first frame update
    void Start()
    {
       
    }
    public void CloseDoor()
    {

        doorSprite.enabled = true;
        phyicsCollider.enabled = true;
    }
    public void OpenDoor()
    {
        if (thisDoorType==DoorType.Key)
        {
            doorSprite.enabled = false;
            phyicsCollider.enabled = false;
            if (playerInventory.numberOfKeys > 0)
            {
                playerInventory.numberOfKeys--;
                Debug.Log("Open");
                context.Raise();
                open = true;
                
            }
           
        }
        else 
        {
            Debug.Log("Open");
            context.Raise();
            open = true;
            doorSprite.enabled = false;
            phyicsCollider.enabled = false;
        }
       
        
        
        
    }
    // Update is called once per frame
    void Update()
    {
        if (inRange==true&& Input.GetButtonDown("Interact"))
        {
            OpenDoor();
            
        }
    }
}
