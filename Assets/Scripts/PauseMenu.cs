using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    bool pause=false;
    public GameObject pauseMenu;
    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
      
    }
    public void Quit()
    {
        SceneManager.LoadScene("StartMenu");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void TogglePause()
    {
        pause = !pause;
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            Resume();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
