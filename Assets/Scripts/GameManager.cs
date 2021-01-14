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
        player.transform.position = new Vector3(-13, 20, 0);

        //Change audio clip
        soundManager.Stop();
        soundManager.clip = churchMusic;
        soundManager.Play();

    }

    public void EnterHouse()
    {
        //Update player position
        player.transform.position = new Vector3(-5, -20, 0);

        //Change audio clip
        soundManager.Stop();
        soundManager.clip = houseMusic;
        soundManager.Play();
    }

    public void LeaveHouse()
    {
        //Update player position
        player.transform.position = new Vector3(-14, -3, 0);

        //Change audio clip
        soundManager.Stop();
        soundManager.clip = sceneMusic;
        soundManager.Play();
    }

    public void LeaveChurch()
    {
        //Update player position
        player.transform.position = new Vector3(6, -3, 0);
        
        //Change audio clip
        soundManager.Stop();
        soundManager.clip = sceneMusic;
        soundManager.Play();

    }
}
