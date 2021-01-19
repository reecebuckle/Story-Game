using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue; //requires dialogue information
    public Interaction interaction; //requires interaction information

    //checks whether player has hit 2d collider of NPC to prompt dialogue
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            if (interaction.interactionHasOccured()) 
                FindObjectOfType<DialogueManager>().ShowInteractedMessage();
            else 
                FindObjectOfType<DialogueManager>().BeginInteraction(dialogue, interaction);
        }

    }

    //checks whether player has left 2d collider of NPC to end dialogue
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            FindObjectOfType<DialogueManager>().EndDialogue();

    }
}