using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;


	 //checks whether player has hit 2d collider of NPC to prompt dialogue
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
           FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        
    }

    //checks whether player has left 2d collider of NPC to end dialogue
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
			FindObjectOfType<DialogueManager>().EndDialogue();
        
    }
}