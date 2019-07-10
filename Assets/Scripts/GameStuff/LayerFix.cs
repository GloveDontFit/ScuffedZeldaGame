using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerFix : MonoBehaviour
{
    public int offset;
    private Renderer myRenderer;
    private int sortingOrderBase=5000;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
      
        myRenderer = GetComponent<Renderer>();
    }
    // Update is called once per frame
    private void LateUpdate()
    {

        myRenderer.sortingOrder =  (int)transform.position.y + offset;
    }
    void Update()
    {
        
    }
}
