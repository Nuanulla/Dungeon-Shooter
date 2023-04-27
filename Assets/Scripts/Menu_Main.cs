using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Main : MonoBehaviour
{
    public void startButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void quitButton()
    {
        Application.Quit();
    }
    
}
