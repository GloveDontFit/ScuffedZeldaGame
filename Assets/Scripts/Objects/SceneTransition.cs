using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransition : MonoBehaviour
{
    [Header("TransitionVariables")]
    public GameObject FadeInPanel;
    public GameObject FadeOutPanel;
    
   
    [Header("Scene variables")]
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public string SceneToLoad;
    public float fadeWait;
    bool inHouse=false;
    private void Awake()
    {
        if (FadeInPanel!=null)
        {
            GameObject panel = Instantiate(FadeInPanel,Vector3.zero,Quaternion.identity) as GameObject;
            Destroy(panel, 1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&collision.isTrigger==true)
        {
            StartCoroutine(FadeControl());
            playerStorage.moveX = collision.GetComponent<PlayerMovement>().change.x;
            playerStorage.moveY = collision.GetComponent<PlayerMovement>().change.y;
           
            playerStorage.initialValue = playerPosition;
        }
       
       
    }
    public IEnumerator FadeControl()
    {
        if (FadeOutPanel!=null)
        {
            Instantiate(FadeOutPanel, Vector3.zero, Quaternion.identity);
        }
       
        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}
