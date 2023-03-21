using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.Time;

public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        SceneManager.LoadScene("Level1");
    }

    public void PlayLevelOne() {
        SceneManager.LoadScene("Level1");
        if (Time.timeScale == 0) {
            Time.timeScale = 1;
        }
    }

    public void PlayLevelTwo() {
        SceneManager.LoadScene("Level2");
        if (Time.timeScale == 0) {
            Time.timeScale = 1;
        } 
    }

    public void togglePause() {
        if (Time.timeScale == 0) {
            Time.timeScale = 1;
        } else {
            Time.timeScale = 0;
        }
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
