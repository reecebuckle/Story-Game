using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    [Header("Ending Buttons")]
    public GameObject ending1Button;
    public GameObject ending2Button;
    public GameObject ending3Button;

    public void Awake()
    {
        Debug.Log("Unlocked ending 1 = " + EndingsUnlocked.Ending1);
        Debug.Log("Unlocked ending 2 = " + EndingsUnlocked.Ending2);
        Debug.Log("Unlocked ending 3 = " + EndingsUnlocked.Ending3);
    }

    public void LoadAvailableEndings() {
        //set to false by default
        ending1Button.SetActive(false);
        ending2Button.SetActive(false);
        ending3Button.SetActive(false);

        if (EndingsUnlocked.Ending1)
            ending1Button.SetActive(true);

        if (EndingsUnlocked.Ending2)
            ending2Button.SetActive(true);

        if (EndingsUnlocked.Ending3)
            ending3Button.SetActive(true);
    }

    public void DeloadEndingButtons() {
        ending1Button.SetActive(false);
        ending2Button.SetActive(false);
        ending3Button.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadEnding1()
    {
        SceneManager.LoadScene("Ending 1");
    }

    public void LoadEnding2()
    {
        SceneManager.LoadScene("Ending 2");
    }

    public void LoadEnding3()
    {
        SceneManager.LoadScene("Ending 3");
    }
}

