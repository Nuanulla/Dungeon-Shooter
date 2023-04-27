using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu_Pause : MonoBehaviour
{
    public static bool GamePaused = false;

    public GameObject Menu_Pause_UI;

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
        Menu_Pause_UI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    void Pause()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
        Menu_Pause_UI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    void Exit()
    {
        Menu_Pause_UI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
        SceneManager.LoadScene("Menu");
    }

    public void ResumeButton()
    {
        Resume();
    }

    public void ExitButton()
    {
        Exit();
    }
}
