using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Moderator : MonoBehaviour
{
    void Update()
    {
        if (GameObject.Find("Player") == null)
        {
            SceneManager.LoadScene("Menu");
            // Implement Results Menu here
        }
        if (GameObject.Find("Troll 1") == null)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
