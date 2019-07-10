using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactable
{
    public bool buttonPressed;
    public BoolValue isPressed;
    public KeyDoor door;
     Animator anim;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&collision.isTrigger)
        {
            if (door.thisDoorType==KeyDoor.DoorType.Button&&buttonPressed==false)
            {
                buttonPressed = true;
                isPressed.RuntimeValue = buttonPressed;
                anim.SetBool("ButtonPressed", true);
                door.OpenDoor();
            }
           
        }
    }
    void Start()
    {
        buttonPressed = isPressed.RuntimeValue;
        anim = GetComponent<Animator>();
        if (buttonPressed == true)
        {
            anim.SetBool("ButtonPressed", true);
            door.OpenDoor();
        }

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
