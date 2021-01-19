using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue
{
    /*
    * Encapsulates player name and dialogue information as array of strings.
    * Add to NPC you wish to add dialogue too
    * Only add a choiceID (greater than 0) if you wish to trigger a choice - based event
    */
    public int choiceID; //requires choiceID (make sure each character has a unique one!!)
    public string name;
    [TextArea(3, 10)]
    public string[] sentences;
}
