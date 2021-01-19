using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ActionSystem : MonoBehaviour
{
    public TextMeshProUGUI DisplayActions;

    public DialogueManager dialogueManager;

    public TextMeshProUGUI journalText;

    private int numberOfActions; //Current number of interactions!

    // Start is called before the first frame update
    private void Start()
    {
        numberOfActions = 4;
        //Set journal to empty
        journalText.text = "";
        journalText.gameObject.SetActive(false);

        //Display starting number of actions
        DisplayActions.text = "Number of remaining actions: " + numberOfActions;
    }

    /*
    * Invoked when an action is used
    */
    public void ConsumeAction(Interaction interaction)
    {
        numberOfActions--;
        //update display
        DisplayActions.text = "Number of actions: " + numberOfActions;
        //append journal with interaction information
        journalText.text += interaction.getKeyInformation();
    }

    /*
    * Invoked when a day is reset 
    */
    public void ResetActionPoints()
    {
        numberOfActions = 4;
        //update display
        DisplayActions.text = "Number of actions: " + numberOfActions;
    }

    /*
    * Used to display Journal
    */
    public void DisplayJournal()
    {
        journalText.gameObject.SetActive(true);
    }

    /*
    * Used to close journmal
    */
    public void CloseJournal()
    {
        journalText.gameObject.SetActive(false);
    }

    /*
    * Temporarily solution to clear journal at the end of the day
    */
    public void ClearJournal() {
         journalText.text = "";
    }

    /*
    * Returns remaining number of actions
    */
    public int getRemainingActions() {
        return this.numberOfActions;
    }
}