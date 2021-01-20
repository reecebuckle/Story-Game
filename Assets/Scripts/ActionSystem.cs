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

    [Header("References to Managers")]
    public DialogueManager dialogueManager;
    public GameManager gameManager;
    private int currentPageNo; //current page number being shown
    private int numberOfActions; //Current number of interactions!

    // Start is called before the first frame update
    private void Start()
    {
        numberOfActions = 4;
        currentPageNo = 0;
        journalPanel.gameObject.SetActive(false);
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
            day0.text += interaction.getKeyInformation();
        else if (currentDay == 1)
            day1.text += interaction.getKeyInformation();
        else if (currentDay == 2)
            day2.text += interaction.getKeyInformation();
        else if (currentDay == 3)
            day3.text += interaction.getKeyInformation();
    }

    /*
    * Method used to append a special keypoint (not in interaction object)
    */
    public void SpecialAppend(string achievement) {
        int currentDay = gameManager.getCurrentDay();
        if (currentDay == 0)
            day0.text += achievement;
        else if (currentDay == 1)
            day1.text += achievement;
        else if (currentDay == 2)
            day2.text += achievement;
        else if (currentDay == 3)
            day3.text += achievement;
    }

    /*
    * Invoked when a day is reset 
    */
    public void ResetActionPoints()
    {
        numberOfActions = 4;
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
        day0.gameObject.SetActive(true); //show day 0 initially
        previousPage.SetActive(false);
        nextPage.SetActive(true);
    }

    /*
    * Used to close journmal
    */
    public void CloseJournal()
    {
        journalPanel.gameObject.SetActive(false);
    }

    /*
    * Display Next page
    */
    public void ShowNextPage() {
        if (currentPageNo == 0) {
            day0.gameObject.SetActive(false);
            day1.gameObject.SetActive(true);
            previousPage.SetActive(true); //allow previous page when turning from 0 -> 1
        } else if (currentPageNo == 1) {
            day1.gameObject.SetActive(false);
            day2.gameObject.SetActive(true);
        } else if (currentPageNo == 2) {
            day2.gameObject.SetActive(false);
            day3.gameObject.SetActive(true);
        } else if (currentPageNo == 3) {
            day3.gameObject.SetActive(false);
            day4.gameObject.SetActive(true);
            nextPage.SetActive(false); //remove next button when turning from page 3 -> 4
        }
        //increment page count [0,1,2,3,4]
        currentPageNo++;
    }

    /*
    * Display previous page
    */
    public void ShowPreviousPage() {
        //not possible to see previous button when on page 0
        if (currentPageNo == 1) {
            day0.gameObject.SetActive(true);
            day1.gameObject.SetActive(false);
            previousPage.SetActive(false); //disallow previous page when turning from 1 -> 0
        } else if (currentPageNo == 2) {
            day1.gameObject.SetActive(true);
            day2.gameObject.SetActive(false);
        } else if (currentPageNo == 3) {
            day2.gameObject.SetActive(true);
            day3.gameObject.SetActive(false);
        } else if (currentPageNo == 4) {
            day3.gameObject.SetActive(true);
            day4.gameObject.SetActive(false);
            nextPage.SetActive(true); //reset next button when turning from page 4 -> 3
        } 
        //increment page count
        currentPageNo--;
    }

    /*
    * NOT USED CURRENTLY 
    * Clear all pages in journal
    */
    public void ClearJournal()
    {
        day0.text = "";
        day1.text = "";
        day2.text = "";
        day3.text = "";
    }

    /*
    * Returns remaining number of actions
    */
    public int getRemainingActions()
    {
        return numberOfActions;
    }
    
}