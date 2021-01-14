using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Important Game Objects")]

    public GameObject player;

    public void EnterChurch() {
        player.transform.position = new Vector3(-13,20,0);
        //TODO CHANGE MUSIC
        
    }

    public void EnterHouse() {
         player.transform.position = new Vector3(-5,-20,0);
        //TODO CHANGE MUSIC
        
    }

    public void LeaveHouse() {
        player.transform.position = new Vector3(-14,-3,0);
        //TODO CHANGE MUSIC
    }

    public void LeaveChurch() {
         player.transform.position = new Vector3(6,-3,0);
        //TODO CHANGE MUSIC

    }
}
