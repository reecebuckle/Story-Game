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


    public void Awake()
    {
        //initially spawn in house, so begin with playing house music
        soundManager.clip = houseMusic;
        soundManager.Play();
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
}
