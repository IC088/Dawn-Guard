﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public void PlayGame ()
    {
        SceneManager.LoadScene("Game Scene");
    }


    public void Tutorial()

    {
        SceneManager.LoadScene("Tutorial");
    }


    public void QuitGame()

    {
        Application.Quit();
    }
}
