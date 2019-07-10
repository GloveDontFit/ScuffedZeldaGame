using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour
{


    public bool contextActive;
    public GameObject contextClue;
    // Start is called before the first frame update
    private void Awake()
    {

        
    }
    public void ChangeContext()
    {
       
            contextActive = !contextActive;
        if (contextActive)
        {
            contextClue.SetActive(true);
        }
        else
        {
            contextClue.SetActive(false);
        }
        

    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
