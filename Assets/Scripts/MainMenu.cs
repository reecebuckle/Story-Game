using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Awake() {
        Debug.Log("Unlocked ending 1 = " + EndingsUnlocked.Ending1);
        Debug.Log("Unlocked ending 2 = " + EndingsUnlocked.Ending2);
        Debug.Log("Unlocked ending 3 = " + EndingsUnlocked.Ending3);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

