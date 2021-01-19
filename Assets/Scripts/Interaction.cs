using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public string keyInformation;
    private bool hasInteracted;

    void Start()
    {
        hasInteracted = false;
    }

    public bool interactionHasOccured()
    {
        return hasInteracted;
    }

    public void setInteractionToTrue() {
        hasInteracted = true;
    }

    public string getKeyInformation()
    {
        return name + ": " + keyInformation + "\n";
    }
}