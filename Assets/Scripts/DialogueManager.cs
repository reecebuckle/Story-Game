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

    

    [Header("ActionSystem to record interactions")]
    public ActionSystem ASystem;

    [Header("Animator to close/open dialogue box")]
    public Animator animator;

    private Queue<string> sentences;

    public float textSpeed = 0.01f; //0.5

    private Dialogue dialogue; //store a local copy (gets overridden, could be buggy)

    private void Awake()
    {
        //Doesn't need to be shown initially
        nextButton.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }

    /*
    *  Used to initiate dialogue if user presses yes and decides to talk to NPC
    */
    public void StartDialogue(Dialogue dialogue)
    {
        this.dialogue = dialogue;
        Debug.Log("Talking to " + dialogue.name);
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;
        string spendActionPointMsg = "Spend one action point talking to " + dialogue.name + " ?";

        sentences.Clear();
        //spend point text displays regardless of whom you talk to
        sentences.Enqueue(spendActionPointMsg);

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
        if (sentence == "ask choice") {
            //Int datatype is set to 0 by default (and never null)
            //If it's anything other than null, we pass that to record choice
            if (dialogue.choiceID != 0)
                PresentChoice(dialogue.choiceID);

        } else {
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
        yesButton.SetActive(true);
        noButton.SetActive(true);
    }

    /*
    * Used to record the input of the choice 
    */
    public void PresentChoice(int choiceID) {
        nextButton.SetActive(false);
        yesChoiceButton.SetActive(true);
        noChoiceButton.SetActive(true);
        //TODO freeze player in position whilst making choice (or in general whilst interacting?)
        //TODO Handle player not being able to be presenting with the same choices TWICE!
        //wait for player to click a button..!
        Debug.Log("presenting choices for " + choiceID);
    }

    public void RecordYesChoice() {
        Debug.Log("Player selected yes");
        yesChoiceButton.SetActive(false);
        noChoiceButton.SetActive(false);
        EndDialogue();

        //SEND THE CHOICE TO ACTION HANDLER!
    }

    public void RecordNoChoice() {
        Debug.Log("Player selected no");
        yesChoiceButton.SetActive(false);
        noChoiceButton.SetActive(false);
        EndDialogue();

        //SEND THE CHOICE !
    }

     // public string getCurrentDialoguePartner()
    // {
    //     return nameText.text;
    // }


}
