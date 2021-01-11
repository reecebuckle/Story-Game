using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionSystem : MonoBehaviour
{
    public TextMeshProUGUI DisplayActions;

    private int numberOfActions;

    // Start is called before the first frame update
    void Start()
    {
        numberOfActions = 2;
    }

    // Update is called once per frame
    void Update()
    {
        displayNumberOfActions(numberOfActions);
    }

    public void useUpAction()
    {
        numberOfActions --;
    }

    public void resetActions()
    {
        numberOfActions = 2;
    }

    public void displayNumberOfActions(int actionsRemaining)
    {
        DisplayActions.text = "Number of actions: " + actionsRemaining;
    }


}
