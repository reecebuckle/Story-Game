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

    public Interacted romeoInteraction;
    public Interacted julietInteraction;
    public Interacted bakerInteraction;
    public Interacted oldManInteraction;

    private int numberOfActions;

    private Interacted[] day1Actions;


    // Start is called before the first frame update
    private void Start()
    {
        numberOfActions = 4;
        
        //Don't even need to do this, whoopsi
        //romeoInteraction = GameObject.Find("Robin Day 0").GetComponent<Interacted>();
        //julietInteraction = GameObject.Find("Scarlett Day 0").GetComponent<Interacted>();
        //bakerInteraction = GameObject.Find("Baker Day 0").GetComponent<Interacted>();
        //oldManInteraction = GameObject.Find("Old Man Day 0").GetComponent<Interacted>();
        

        journalText.text = "";
        journalText.gameObject.SetActive(false);
        

        Interacted[] day1Actions = {romeoInteraction, julietInteraction, bakerInteraction,oldManInteraction};
        setDay1Array(day1Actions);
    }

    // Update is called once per frame
    void Update()
    {
        displayNumberOfActions(numberOfActions);
        if (hasGotNoActions())
        {
            Debug.Log("Has Romeo been interacted with? " +romeoInteraction.hasBeenInteracted());
            Debug.Log("Has Juliet been interacted with? " + julietInteraction.hasBeenInteracted());

            Debug.Log("Has random member been interacted with? " + oldManInteraction.hasBeenInteracted());
            
            
            //whatHasBeenActivatedInDay1();
            //resetInteractions(day1Actions);
            //resetActions();
            //SceneManager.LoadScene("House");
        }


    }

    public void useUpAction()
    {
        numberOfActions --;
    }

    //call this when new day starts, resets action points
    public void resetActions()
    {
        numberOfActions = 2;
    }

    public void displayNumberOfActions(int actionsRemaining)
    {
        DisplayActions.text = "Number of actions: " + actionsRemaining;
    }

    public bool hasGotNoActions()
    {
        if (numberOfActions <= 0)
            return true;
        else
            return false;
    }

    public void resetInteractions(Interacted[] dayBools)
    {
        Debug.Log(dayBools.Length);
        foreach (Interacted action in dayBools)
        {
            action.resetInteraction();
        }
    }

    public void setDay1Array(Interacted[] newArray)
    {
        day1Actions = newArray;
    }

    public Interacted[] getDay1Array()
    {
        return day1Actions;
    }

    public void whatHasBeenActivatedInDay1()
    {
        journalText.text = "";
        Interacted[] temp = getDay1Array();
        foreach (Interacted action in temp)
        {
            if (action.hasBeenInteracted())
            {
                journalText.text += action.getKeyInformation();
            }
        }

        //journalText.gameObject.SetActive(true);
    }

    public void displayJournal()
    {
        whatHasBeenActivatedInDay1();
        journalText.gameObject.SetActive(true);
        
    }

    public void closeJournal()
    {
        journalText.gameObject.SetActive(false);
    }



}