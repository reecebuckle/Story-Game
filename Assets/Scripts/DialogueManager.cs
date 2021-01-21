using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
*The following scripts (and Dialogue.cs / DialogueTriggrer.cs) was adapted and expanded upon 
*from a Brackey's dialogue tutorial for the purpose of this game
*https://www.youtube.com/watch?v=_nRzoTzeyxU&t=5s&ab_channel=Brackeys
*However this dialogue system has been expanded and modified on heavily!
*/
public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Canvas Objects")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject nextButton;
    public GameObject yesButton;
    public GameObject noButton;

    [Header("Special choice buttons")]
    public GameObject yesChoiceButton;
    public GameObject noChoiceButton;

    [Header("References to other Systems")]
    public ActionSystem actionSystem;
    public GameManager gameManager;
    public PlayerMovement player;
    private Rigidbody2D playerRB;

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
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    /*
    * Used to begin an interaction
    */

    public void BeginInteraction(Dialogue dialogue, Interaction interaction)
    {
        this.dialogue = dialogue; //this could be buggy
        this.interaction = interaction; //this could be buggy

        //only allow an interaction to begin if player has remaining actions
        if (actionSystem.getRemainingActions() > 0) 
            StartDialogue(dialogue);
        else 
            ShowOutOfActionsMessage();
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
        //prevent player from moving until sequence is finished
        FreezePlayerPosition();
        nextButton.SetActive(true);


        if (sentences.Count == 0)
        {
            //if end of sentences reached and was a unique interaction, inform game manager
            if (dialogue.choiceID != 0)
                NotifyAction(dialogue.choiceID);

            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

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
        //incase player is frozen. unfreeze
        playerRB.constraints = RigidbodyConstraints2D.None;
    }

    /*
    * This is shown instead of StartDialogue if the player has already interacted with the NPC
    */
    public void ShowInteractedMessage(Dialogue dialogueU)
    {
        nameText.text = dialogueU.name;
        animator.SetBool("IsOpen", true);
        sentences.Clear();
        string alreadyInteracted = "(I've already interacted with them today...)";
        sentences.Enqueue(alreadyInteracted);
        DisplayFirstSentence();
    }

    /*
    * Used to show out of actions!
    */
    public void ShowOutOfActionsMessage() {
        animator.SetBool("IsOpen", true);
        sentences.Clear();
        string alreadyInteracted = "(It's getting late, I should head back home...)";
        sentences.Enqueue(alreadyInteracted);
        DisplayFirstSentence();
    }

    /*
    * Used to update game world without asking player a choice
    */
    public void NotifyAction(int choiceID)
    {
        gameManager.UpdateGameWorld(choiceID);
    }


    /*
    * Used to record the input of the choice 
    */
    public void PresentChoice(int choiceID)
    {
        nextButton.SetActive(false);
        yesChoiceButton.SetActive(true);
        noChoiceButton.SetActive(true);
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
        //choiceID tells the game manager how to evulate the choice, true represents yes
        gameManager.EvaluateChoice(dialogue.choiceID, false);
    }

    /*
    * When player chooses yes during the inteaction
    */
    public void RecordYesChoice()
    {
        yesChoiceButton.SetActive(false);
        noChoiceButton.SetActive(false);
        EndDialogue();
        //choiceID tells the game manager how to evulate the choice, true represents yes
        gameManager.EvaluateChoice(dialogue.choiceID, true);
    }

    /*
    *Invoke this when you wish to freeze player position
    */
    public void FreezePlayerPosition()
    {
        playerRB.constraints = RigidbodyConstraints2D.FreezePosition;
    }
}
