using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextSpriteSwitcher : MonoBehaviour
{
    public bool exclamation = false;
    public Sprite ExclamationPoint;
    public Sprite QuestionMark;
    // Start is called before the first frame update
    void Start()
    {
      
    }
    public void SwitchSprite()
    {
        exclamation = !exclamation;
    }
   
   
    // Update is called once per frame
    void Update()
    {
        if (exclamation==true)
        {
            GetComponent<SpriteRenderer>().sprite = ExclamationPoint;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = QuestionMark;
        }
    }
}
