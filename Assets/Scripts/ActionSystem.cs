using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ActionSystem : MonoBehaviour
{
    public TextMeshProUGUI DisplayActions;

    public DialogueManager dialogueManager;

    public Interacted romeoInteraction;
    public Interacted julietInteraction;
    public Interacted owlInteraction;
    public Interacted publicMemberInteraction;

    private int numberOfActions;

    private Interacted[] day1Actions = new Interacted[10];


    // Start is called before the first frame update
    private void Start()
    {
        numberOfActions = 2;
        //Interacted[] day1Actions = {romeoInteraction, julietInteraction, owlInteraction, publicMemberInteraction};
        //day1Actions[0] = romeoInteraction;
        //day1Actions[1] = julietInteraction;
        //day1Actions[2] = OwlInteraction;
        //day1Actions[3] = publicMemberInteraction;
        romeoInteraction = GameObject.Find("hat-man-idle-1").GetComponent<Interacted>();
        julietInteraction = GameObject.Find("woman-idle1").GetComponent<Interacted>();
        owlInteraction = GameObject.Find("bearded-idle-1").GetComponent<Interacted>();
        publicMemberInteraction = GameObject.Find("wizard-idle-1").GetComponent<Interacted>();

    }

    // Update is called once per frame
    void Update()
    {
        displayNumberOfActions(numberOfActions);
        if (hasGotNoActions())
        {
            resetActions();
            //resetInteractions();
            Debug.Log("Has Romeo been interacted with? " +romeoInteraction.hasBeenInteracted());
            Debug.Log("Has Juliet been interacted with? " + julietInteraction.hasBeenInteracted());
            Debug.Log("Has Owl been interacted with? " + owlInteraction.hasBeenInteracted());
            Debug.Log("Has random member been interacted with? " + publicMemberInteraction.hasBeenInteracted());
            SceneManager.LoadScene("House");
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

    public void resetInteractions()
    {
        foreach (Interacted action in day1Actions)
        {
            action.resetInteraction();
        }
    }

}
