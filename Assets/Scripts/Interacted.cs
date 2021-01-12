using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacted : MonoBehaviour
{
    private bool interaction;
    public ActionSystem actionSystem;
    public string name;

    // Start is called before the first frame update
    void Start()
    {
        interaction = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void interactionOccured()
    {
        if (interaction == false)
        {
            interaction = true;
            actionSystem.useUpAction();
        }


    }

    public void resetInteraction()
    {
        interaction = false;
    }

    public bool hasBeenInteracted()
    {
        return interaction;
    }
}
