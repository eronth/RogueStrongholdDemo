using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void Settings()
    {
        Debug.Log("Settings...");
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Quiting...");
    }

    public void QuitToDesktop()
    {
        Debug.Log("Quiting to desktop...");
        Application.Quit(); // Todo move this to a general location.
    }

    public void SaveMenu()
    {
        Debug.Log("Saving...");
    }

    public void LoadMenu()
    {
        Debug.Log("Loading...");
    }
}