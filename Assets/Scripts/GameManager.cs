using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Reference to Player")]
    public GameObject player;

    [Header("References to other managers")]
    public ActionSystem actionSystem;
    public DialogueManager dialogueManager;

    [Header("References to other Canvas objects")]
    public GameObject journalCanvas;
    public GameObject objectCanvas;
    public GameObject dialogueCanvas;
    public GameObject canvasOutcome1;
    public GameObject canvasOutcome2;
    public GameObject canvasOutcome3;

    [Header("Integrated Audio Manager")]
    public AudioClip sceneMusic;
    public AudioClip churchMusic;
    public AudioClip houseMusic;
    public AudioSource soundManager;

    [Header("Add Crossfade Transitioner")]
    public Animator transition;

    [Header("NPCs for Different Days")]
    public GameObject day0NPCS;
    public GameObject day1NPCS;
    public GameObject day2NPCS;

    [Header("Endings Unlocked")]
 

    private int currentDay;
    private bool canAccessOwlHouse; //can the player access this house
    private bool canAccessTavern; // can player access tavern?
    private bool letterAvailable;// has the player unlocked the letter?
    private bool foundLetter; // did player find conspiracy letter?
    private bool foundContract;// did player find contract?

    public void Awake()
    {
        //initially spawn in house, so begin with playing house music
        soundManager.clip = houseMusic;
        soundManager.Play();

        // in case accidentally set wrong in unity 
        day0NPCS.SetActive(true);
        day1NPCS.SetActive(false);
        day2NPCS.SetActive(false);
        canvasOutcome1.SetActive(false);
        canvasOutcome2.SetActive(false);
        canvasOutcome3.SetActive(false);

        //begin day counter
        currentDay = 0;

        //all false initially
        canAccessOwlHouse = false;
        canAccessTavern = false;
        foundContract = false;
        letterAvailable = false;
        foundLetter = false;
    }


    /*
    * IEnumerator is used to wait for 0.3 seconds before transitioning player (allowing animation to run)
    * All the enter/exit methods are built the same way
    */
    public IEnumerator EnterChurch()
    {
        //Play transition
        transition.SetTrigger("Start");

        //Wait for 0.2 seconds
        yield return new WaitForSeconds(0.3f);

        //Update player position
        player.transform.position = new Vector3(-20, 20, 0);

        //Change audio clip
        soundManager.Stop();
        soundManager.clip = churchMusic;
        soundManager.Play();
    }

    public IEnumerator EnterHouse()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0.4f);
        player.transform.position = new Vector3(69, -24, 0);
        soundManager.Stop();
        soundManager.clip = houseMusic;
        soundManager.Play();
    }

    public IEnumerator LeaveHouse()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0.4f);
        player.transform.position = new Vector3(-25, -3, 0);
        soundManager.Stop();
        soundManager.clip = sceneMusic;
        soundManager.Play();
    }

    public IEnumerator LeaveChurch()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0.4f);
        player.transform.position = new Vector3(-12, -3, 0);
        soundManager.Stop();
        soundManager.clip = sceneMusic;
        soundManager.Play();
    }

    public IEnumerator EnterOwlHouse()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0.4f);
        player.transform.position = new Vector3(-19, -24, 0);
        //TODO: Add new music? 
    }

    public IEnumerator LeaveOwlHouse()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0.4f);
        player.transform.position = new Vector3(12, -3, 0);
        //TODO: Add new music? 
    }

    public IEnumerator EnterTavern()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0.4f);
        player.transform.position = new Vector3(-21, -47, 0);
        //TODO: Add new music? 
    }

    public IEnumerator LeaveTavern()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0.4f);
        player.transform.position = new Vector3(6, -3, 0);
        //TODO: Add new Music
    }

    /*
    * Used to simulate resting for one day
    */
    public IEnumerator RestForDay()
    {
        currentDay++;
        actionSystem.ResetActionPoints();

        if (currentDay == 1)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(0.4f);
            day0NPCS.SetActive(false);
            day1NPCS.SetActive(true);
        }
        else if (currentDay == 2)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(0.4f);
            day1NPCS.SetActive(false);
            day2NPCS.SetActive(true);
        }
        else if (currentDay == 3)
        {
            day2NPCS.SetActive(false);
            transition.SetTrigger("End"); //fade to black
            yield return new WaitForSeconds(0.5f);
            
            //Remove player from game!
            player.gameObject.SetActive(false);   

            //Close all canvases
            dialogueCanvas.gameObject.SetActive(false);
            objectCanvas.gameObject.SetActive(false);
            journalCanvas.gameObject.SetActive(false);

            //If player found all the evidence, give them all 3 options
            if (foundLetter && foundContract)
                LoadCanvas3();
            //If player found half the evidence, give them 2 options
            else if (foundLetter || foundContract)
                LoadCanvas2();
            //If player found no evidence, give them 1 option
            else
                LoadCanvas1();
        }
    }

    /*
    * Method for updating game world following player interactions
    */
    public void UpdateGameWorld(int choiceID)
    {
        if (choiceID == 1)
            canAccessOwlHouse = true;

        if (choiceID == 2)
            canAccessTavern = true;

        if (choiceID == 3)
        {
            foundContract = true;
            actionSystem.SpecialAppend("You picked up the sellswords contract");
            actionSystem.DisplayContract();
        }

        if (choiceID == 4)
            letterAvailable = true;
    }


    /*
    * Loaded at end of the game if all options were available
    */
    public void LoadCanvas3()
    {
        canvasOutcome3.SetActive(true);
    }

    /*
    * Loaded at end of the game if two options were available
    */
    public void LoadCanvas2()
    {
        canvasOutcome2.SetActive(true);
    }

    /*
    * Loaded at end of the game if no options were available
    */
    public void LoadCanvas1()
    {
        canvasOutcome1.SetActive(true);
    }

    /*
    * Selected by player from canvas
    */
    public void LoadEnding3()
    {
        Debug.Log("Loading Ending Three");
        //update static reference of ending unlocked
        EndingsUnlocked.Ending3 = true;
        SceneManager.LoadScene("Ending 3");

    }

    /*
    * Selected by player from canvas
    */
    public void LoadEnding2()
    {
        Debug.Log("Loading Ending Two");
        //update static reference of ending unlocked
        EndingsUnlocked.Ending2 = true;
        SceneManager.LoadScene("Ending 2");


    }

    /*
    * Selected by player from canvas
    */
    public void LoadEnding1()
    {
        Debug.Log("Loading Ending One");
        //update static reference of ending unlocked
        EndingsUnlocked.Ending1 = true;
        SceneManager.LoadScene("Ending1 Proper");
    }


    /*
    * Method for handling player choices, the response is true if yes, false if no
    */
    public void EvaluateChoice(int choiceID, bool response)
    {
    }

    // Removes letter from game and updates bool
    public void TakeLetter()
    {
        foundLetter = true;
        letterAvailable = false;
        actionSystem.SpecialAppend("You found a letter conspiring to end House Grasshopper");
        actionSystem.DisplayLetter();
    }

    //Returns whether player can access owl house
    public bool CheckOwlEntryCondition()
    {
        return canAccessOwlHouse;
    }

    //Returns whether player can access owl house
    public bool CheckTavernEntryCondition()
    {
        return canAccessTavern;
    }

    //Reeturns whether player can see bookshelf
    public bool BookshelfUnlocked()
    {
        return letterAvailable;
    }

    //returns current day
    public int getCurrentDay()
    {
        return currentDay;
    }

    //returns if letter is picked up
    public bool letterPickedUp() {
        return foundLetter;
    }

    //returns if letter is picked u
    public bool contractPickedUp() {
        return foundContract;
    }
}
