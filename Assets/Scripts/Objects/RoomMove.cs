using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector3 playerPush;
  public  float textDisplayTime;
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;
    public GameObject virtualCamera;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (needText)
            {
                StartCoroutine(ShowText());
            }
            virtualCamera.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position+=playerPush;
            virtualCamera.SetActive(false);
        }
       
    }
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponentInChildren<CameraScript>().gameObject;
    }
    IEnumerator ShowText()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(textDisplayTime);
        text.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
