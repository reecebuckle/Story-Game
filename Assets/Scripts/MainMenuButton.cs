using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    
    /*
     * Loads main menu
     */
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
