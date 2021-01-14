using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Important Game Objects")]
    public GameObject player;


    [Header("Integrated Audio Manager")]
    public AudioClip sceneMusic;
    public AudioClip churchMusic;
    public AudioClip houseMusic;

    public AudioSource soundManager;

    [Header("Day 0 NPCs")]
    public GameObject robinDay0;
    public GameObject scarlettDay0;
    public GameObject bakerDay0;
    public GameObject oldManDay0;

    [Header("Day 1 NPCs")]
    public GameObject robinDay1;
    public GameObject scarlettDay1;
    public GameObject bakerDay1;
    public GameObject ibisDay1;

    [Header("Day 2 NPCs")]
    public GameObject robinDay2;
    public GameObject scarlettDay2;
    public GameObject oldManDay2;
    public GameObject ibisDay2;
    public GameObject adventurerDay2;

    [Header("Day 3 NPCs")]
    public GameObject bakerDay3;
    public GameObject ibisDay3;
    public GameObject adventurerDay3;

    private GameObject[] day0NPCS;
    private GameObject[] day1NPCS;
    private GameObject[] day2NPCS;
    private GameObject[] day3NPCS;



    public void Awake()
    {
        //initially spawn in house, so begin with playing house music
        soundManager.clip = houseMusic;
        soundManager.Play();

        GameObject[] day0NPCS = {robinDay0, scarlettDay0, bakerDay0, oldManDay0 };
        GameObject[] day1NPCS = {robinDay1, scarlettDay1, bakerDay1, ibisDay1 };
        GameObject[] day2NPCS = {robinDay2, scarlettDay2, oldManDay2, ibisDay2, adventurerDay2 };
        GameObject[] day3NPCS = {bakerDay3, ibisDay3, adventurerDay3};

        setDay0NPCS(day0NPCS);
        setDay1NPCS(day1NPCS);
        setDay2NPCS(day2NPCS);
        setDay3NPCS(day3NPCS);
        

    }


    public void EnterChurch()
    {
        //Update player position
        player.transform.position = new Vector3(-20, 20, 0);

        //Change audio clip
        soundManager.Stop();
        soundManager.clip = churchMusic;
        soundManager.Play();
   
    }

    public void EnterHouse()
    {
        //Update player position
        player.transform.position = new Vector3(69, -24, 0);

        //Change audio clip
        soundManager.Stop();
        soundManager.clip = houseMusic;
        soundManager.Play();
    }

    public void LeaveHouse()
    {
        //Update player position
        player.transform.position = new Vector3(-25, -3, 0);

        //Change audio clip
        soundManager.Stop();
        soundManager.clip = sceneMusic;
        soundManager.Play();
    }

    public void LeaveChurch()
    {
        //Update player position
        player.transform.position = new Vector3(-12, -3, 0);

        //Change audio clip
        soundManager.Stop();
        soundManager.clip = sceneMusic;
        soundManager.Play();

        deleteDayNPC(day0NPCS);
        deleteDayNPC(day1NPCS);
        deleteDayNPC(day2NPCS);
        deleteDayNPC(day3NPCS);


        loadDayNPC(day2NPCS);
        loadDayNPC(day3NPCS);
    }

    public void EnterOwlHouse()
    {
        //Update player position
        player.transform.position = new Vector3(-19, -24, 0);

        //TODO: Add new music? 
    }

    public void LeaveOwlHouse()
    {
        //Update player position
        player.transform.position = new Vector3(12, -3, 0);

        //TODO: Add new music? 
    }

    public void EnterTavern()
    {
        //Update player position
        player.transform.position = new Vector3(-21, -47, 0);

        //TODO: Add new music? 

    }

    public void LeaveTavern()
    {
        //Update player position
        player.transform.position = new Vector3(6, -3, 0);

        //TODO: Add new Music

    }

    public void loadDayNPC(GameObject[] npcs)
    {
        foreach (GameObject npc in npcs)
        {
            npc.gameObject.SetActive(true);
        }
    }

    public void deleteDayNPC(GameObject[] npcs)
    {
        foreach (GameObject npc in npcs)
        {
            npc.gameObject.SetActive(false);
        }
    }

    public void setDay0NPCS(GameObject[] npcs)
    {
        day0NPCS = npcs;
    }

    public void setDay1NPCS(GameObject[] npcs)
    {
        day1NPCS = npcs;
    }

    public void setDay2NPCS(GameObject[] npcs)
    {
        day2NPCS = npcs;
    }

    public void setDay3NPCS(GameObject[] npcs)
    {
        day3NPCS = npcs;
    }

    public GameObject[] getDay0NPCS()
    {
        return day0NPCS;
    }

    public GameObject[] getDay1NPCS()
    {
        return day1NPCS;
    }

    public GameObject[] getDay2NPCS()
    {
        return day2NPCS;
    }

    public GameObject[] getDay3NPCS()
    {
        return day3NPCS;
    }



}
