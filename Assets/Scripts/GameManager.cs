using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Reference to Player")]
    public GameObject player;

    [Header("Reference to Action System")]
    public ActionSystem actionSystem;

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
        //short term fix preventing an overly cluttered journal
        actionSystem.ResetActionPoints();

        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0.4f);
        //TODO check to see interactions for day is 0 before allowing you to move on!

        //increment day count
        currentDay++;
        if (currentDay == 1)
        {
            day0NPCS.SetActive(false);
            day1NPCS.SetActive(true);
        }
        else if (currentDay == 2)
        {
            day1NPCS.SetActive(false);
            day2NPCS.SetActive(true);
        }
        else if (currentDay == 3)
        {
            day2NPCS.SetActive(false);
            //TODO PRESENT PLAYER CHOICE HERE THAT THEN LEADS ONTO NEXT THING

            // current day 4 load pivotal thing, whcih will show the 3 button canvas options
            // UI 
            // For any other future day, throw a not able to rest message/prevent user from resting!
            // then depending on option this leads into 1 of 3 cutscene animations

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

        if (choiceID == 3) {
            foundContract = true;
            actionSystem.SpecialAppend("You picked up the sellswords contact");
        }
            
        if (choiceID == 4)
            letterAvailable = true;
    }

    /*
    * Method for handling player choices, the response is true if yes, false if no
    */
    public void EvaluateChoice(int choiceID, bool response)
    {
    }

    // Removes letter from game and updates bool
    public void TakeLetter()    { 
        foundLetter = true;
        letterAvailable = false;
        actionSystem.SpecialAppend("You found a letter conspiring to end House Grasshopper");
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
    public bool BookshelfUnlocked() {
        return letterAvailable;
    }

    //returns current day
    public int getCurrentDay()
    {
        return currentDay;
    }
}
