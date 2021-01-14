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

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public GameObject nextButton;
    public GameObject yesButton;
    public GameObject noButton;

    public ActionSystem ASystem;

    public Animator animator;

    private Queue<string> sentences;

    public float textSpeed = 0.01f; //0.5

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
        yesButton.SetActive(true);
        noButton.SetActive(true);

    }

    public string getCurrentDialoguePartner()
    {
        return nameText.text;
    }


}
