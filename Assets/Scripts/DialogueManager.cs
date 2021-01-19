using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
*The following script (and Dialogue.cs / DialogueTriggrer.cs) was adapted and expanded upon 
*from a Brackey's dialogue tutorial for the purpose of this game
*https://www.youtube.com/watch?v=_nRzoTzeyxU&t=5s&ab_channel=Brackeys
*/
public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue and Name text mesh pro objects")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    [Header("Yes, No and Next buttons")]
    public GameObject nextButton;
    public GameObject yesButton;
    public GameObject noButton;

    [Header("Special choice buttons")]
    public GameObject yesChoiceButton;
    public GameObject noChoiceButton;

    [Header("Reference to Action System")]
    public ActionSystem actionSystem;

    [Header("Animator to close/open dialogue box")]
    public Animator animator;

    private Queue<string> sentences;

    public float textSpeed = 0.01f; 

    private Dialogue dialogue; //store a local copy (gets overridden, could be buggy)
    private Interaction interaction; //store a local copy (gets overridden, could be buggy)

    private void Awake()
    {
        //Just in case they were switched on in the unity editor
        nextButton.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        yesChoiceButton.SetActive(false);
        noChoiceButton.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }

    /*
    * Used to begin an interaction
    */

    public void BeginInteraction(Dialogue dialogue, Interaction interaction){
        this.dialogue = dialogue; //this could be buggy
        this.interaction = interaction; //this could be buggy
       
        StartDialogue(dialogue);
    }

    /*
    *  Used to initiate dialogue if user presses yes and decides to talk to NPC
    */
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        yesButton.SetActive(true);
        noButton.SetActive(true);

        //Ask player if they wish to interact
        nameText.text = dialogue.name;
        string spendActionPointMsg = "Spend one action point talking to " + dialogue.name + " ?";
        sentences.Clear();
        sentences.Enqueue(spendActionPointMsg);
        Debug.Log("Talking to " + dialogue.name);

        foreach (string sentence in dialogue.sentences)
            sentences.Enqueue(sentence);

        DisplayFirstSentence();
    }

    /*
    *Used to display first sentence
    */
    public void DisplayFirstSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    /*
    * Used to load the next sentence
    * Dialogue object is only parsed incase a choiceID is present
    */
    public void DisplayNextSentence()
    {
        nextButton.SetActive(true);

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();

        //if sentence is a special type (prompt choices option)
        if (sentence == "ask choice")
        {
            //Int datatype is set to 0 by default (and never null)
            //If it's anything other than null, we pass that to record choice
            if (dialogue.choiceID != 0)
                PresentChoice(dialogue.choiceID);

        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    /*
    * Used to type out the dialogue sentence at a set speed
    */
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            //yield return new WaitForSeconds(textSpeed);
            yield return null;
        }
    }

    /*
    * Used to close dialogue box prematurely or if all options have been exhausted
    */
    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        nextButton.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        yesChoiceButton.SetActive(false);
        noChoiceButton.SetActive(false);
    }

    /*
    * This is shown instead of StartDialogue if the player has already interacted with the NPC
    */
    public void ShowInteractedMessage()
    {
        animator.SetBool("IsOpen", true);
        sentences.Clear();
        string alreadyInteracted = "(I've already interacted with them today...)";
        sentences.Enqueue(alreadyInteracted);
        DisplayFirstSentence();
    }

    /*
    * Used to record the input of the choice 
    */
    public void PresentChoice(int choiceID)
    {
        nextButton.SetActive(false);
        yesChoiceButton.SetActive(true);
        noChoiceButton.SetActive(true);
        //TODO freeze player in position whilst making choice (or in general whilst interacting?)
        //TODO Handle player not being able to be presenting with the same choices TWICE!
        //wait for player to click a button..!
    }

    /*
    * Record that this interaction has occured
    */
    public void RecordInteraction()
    {
        interaction.setInteractionToTrue();
        actionSystem.ConsumeAction(interaction);
    }

    /*
    * When player chooses no during the inteaction
    */
    public void RecordNoChoice()
    {
        yesChoiceButton.SetActive(false);
        noChoiceButton.SetActive(false);
        EndDialogue();
        //SEND THE CHOICE TO ACTION MANAGER
    }

    /*
    * When player chooses yes during the inteaction
    */
    public void RecordYesChoice()
    {
        yesChoiceButton.SetActive(false);
        noChoiceButton.SetActive(false);
        EndDialogue();
        //SEND THE CHOICE TO ACTION MANAGER
    }
}
