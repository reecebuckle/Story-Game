using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ActionSystem : MonoBehaviour
{
    [Header("References to Journal Canvas")]
    public TextMeshProUGUI DisplayActions;
    public GameObject journalPanel;
    public GameObject nextPage;
    public GameObject previousPage;
    public TextMeshProUGUI day0;
    public TextMeshProUGUI day1;
    public TextMeshProUGUI day2;
    public TextMeshProUGUI day3;
    public TextMeshProUGUI day4;

    [Header("References to Object Interaction Panel")]
    public GameObject ObjectInteractionPanel;
    public TextMeshProUGUI ObjectInteractionText;

    [Header("References to Managers")]
    public DialogueManager dialogueManager;
    public GameManager gameManager;
    private int currentPageNo; //current page number being shown
    private int numberOfActions; //Current number of interactions!
    private string newLine = " \n";



    // Start is called before the first frame update
    private void Start()
    {
        numberOfActions = 3;
        currentPageNo = 1;
        journalPanel.gameObject.SetActive(false);
        ObjectInteractionPanel.gameObject.SetActive(false);
        //Display starting number of actions
        DisplayActions.text = "Remaining actions: " + numberOfActions;
    }

    /*
    * Invoked when an action is used
    */
    public void ConsumeAction(Interaction interaction)
    {
        numberOfActions--;
        //update display
        DisplayActions.text = "Remaining actions: " + numberOfActions;

        //append journal with interaction information on correct day
        int currentDay = gameManager.getCurrentDay();
        if (currentDay == 0)
        {
            day0.text += interaction.getKeyInformation();
            day0.text += "\n";
        }
        else if (currentDay == 1)
        {
            day1.text += interaction.getKeyInformation();
            day1.text += "\n";
        }
        else if (currentDay == 2)
        {
            day2.text += interaction.getKeyInformation();
            day2.text += "\n";
        }
    }

    /*
    * Method used to append a special keypoint (not in interaction object)
    */
    public void SpecialAppend(string achievement)
    {
        int currentDay = gameManager.getCurrentDay();
        if (currentDay == 0)
        {
            day0.text += achievement;
            day0.text += "\n";
        }
        else if (currentDay == 1)
        {
            day1.text += achievement;
            day1.text += "\n";
        }
        else if (currentDay == 2)
        {
            day2.text += achievement;
            day2.text += "\n";
        }
    }

    /*
    * Invoked when a day is reset 
    */
    public void ResetActionPoints()
    {
        numberOfActions = 3;
        //update display
        DisplayActions.text = "Remaining actions: " + numberOfActions;
    }

    /*
    * Used to display Journal
    */
    public void DisplayJournal()
    {
        journalPanel.gameObject.SetActive(true);
        //can easily add if else statements to show current day on opening
        int currentDay = gameManager.getCurrentDay();

        if (currentDay == 0)
        {
            day0.gameObject.SetActive(true);
            previousPage.SetActive(false);
            nextPage.SetActive(true);
            currentPageNo = 1;
        }
        else if (currentDay == 1)
        {
            day1.gameObject.SetActive(true);
            previousPage.SetActive(true);
            nextPage.SetActive(true);
            currentPageNo = 2;
        }
        else if (currentDay == 2)
        {
            day2.gameObject.SetActive(true);
            previousPage.SetActive(true);
            nextPage.SetActive(false);
            currentPageNo = 3;
        }
    }

    /*
    * Used to close journmal, make sure all pages are closed
    */
    public void CloseJournal()
    {
        journalPanel.gameObject.SetActive(false);
        day0.gameObject.SetActive(false);
        day1.gameObject.SetActive(false);
        day2.gameObject.SetActive(false);
    }

    /*
    * Display Next page
    */
    public void ShowNextPage()
    {

        // If turning from page 1 -> 2
        if (currentPageNo == 1)
        {
            day0.gameObject.SetActive(false);
            day1.gameObject.SetActive(true);
            nextPage.SetActive(true);
            previousPage.SetActive(true);

            // If turning from page 2 -> 3
        }
        else if (currentPageNo == 2)
        {
            day1.gameObject.SetActive(false);
            day2.gameObject.SetActive(true);
            nextPage.SetActive(false);
            previousPage.SetActive(true);
        }
        //increment page count
        currentPageNo++;
    }

    /*
    * Display previous page
    */
    public void ShowPreviousPage()
    {
        // If turning from Page 2 -> 1
        if (currentPageNo == 2)
        {
            day0.gameObject.SetActive(true);
            day1.gameObject.SetActive(false);
            nextPage.SetActive(true);
            previousPage.SetActive(false);

            //If turning from page 3 -> 2    
        }
        else if (currentPageNo == 3)
        {
            day1.gameObject.SetActive(true);
            day2.gameObject.SetActive(false);
            previousPage.SetActive(true);
            nextPage.SetActive(true);
        }

        //decrement page count
        currentPageNo--;
    }


    /*
     * Opens the panel that contains text for object interactions
     */
    public void OpenObjectInteractionPanel(string msg)
    {
        ObjectInteractionText.text = msg;
        ObjectInteractionPanel.gameObject.SetActive(true);
    }

    /*
     * Closes the panel that contains text for object interactions 
     */
    public void CloseObjectInteractionPanel()
    {
        ObjectInteractionPanel.gameObject.SetActive(false);
    }

    /*
    * Returns remaining number of actions
    */
    public int getRemainingActions()
    {
        return numberOfActions;
    }

}