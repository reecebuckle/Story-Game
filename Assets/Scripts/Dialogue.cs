using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue
{
    /*
    * Encapsulates player name and dialogue information as array of strings.
    * Add to NPC you wish to add dialogue too
    */
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;

}
