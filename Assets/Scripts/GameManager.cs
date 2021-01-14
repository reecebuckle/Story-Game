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

    [Header("Add Crossfade Transitioner")]
    public Animator transition;


    public void Awake()
    {
        //initially spawn in house, so begin with playing house music
        soundManager.clip = houseMusic;
        soundManager.Play();
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
}
