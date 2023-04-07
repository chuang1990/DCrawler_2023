using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static bool gamePaused = false;

    [SerializeField] private GameObject pauseMenu;
    public void StartGame()
    {
        gamePaused = false;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TitleMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseMenu()
    {
        gamePaused = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        gamePaused = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseMenu.SetActive(false);
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape) && gamePaused == false)    
        {
            PauseMenu();
        }
    }
}
