using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameISPaused = false;
    public GameObject pauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameISPaused)
            {
                Time.timeScale = 1f;
                Resume();
            }
            else {
                Time.timeScale = 0f;
                Pause(); }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameISPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true );
        Time.timeScale = 0f;
        GameISPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
