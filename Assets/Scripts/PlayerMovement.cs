﻿/**
* The following character controller was adapted from Brackey's 2D Player Movement controller
* https://www.youtube.com/watch?v=dwcT-Dch0bA&list=PLPV2KyIb3jR6TFcFuzI2bB7TMNIIBpKMQ&index=2&ab_channel=Brackeys
* And is available as free to use
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    [Header("Movement parameters")]
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    private bool jump = false;
    private GameObject currentlyInteractingTo;

    //Collision detection bools
    private bool inInteractionZone = false;
    private bool touchingChair = false;
    private bool exitHouse = false;
    private bool enterHouse = false;
    private bool enterChurch = false;
    private bool exitChurch = false;

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Player Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            Debug.Log("jumping");
            animator.SetBool("IsJumping", true);
        }

        //these control player options when touching a collider
        if (Input.GetKeyDown(KeyCode.E))
        {

            if (touchingChair) {
                Debug.Log("Touching bed and pressed E, time to go sleep");
                //TODO: Implement loading next scene/day etc
                //probably wise to start new coroutine with a cool down here to prevent spammage of E
            } 

            if (exitHouse) 
               FindObjectOfType<GameManager>().LeaveHouse();
            
            if (enterHouse) 
                FindObjectOfType<GameManager>().EnterHouse();

            if (exitChurch) 
                FindObjectOfType<GameManager>().LeaveChurch();
            
            if (enterChurch) 
                FindObjectOfType<GameManager>().EnterChurch();
            
        }
    }

    public void OnLanding()
    {
        Debug.Log("landing");
        animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            inInteractionZone = true;
            currentlyInteractingTo = other.gameObject;
        }

        //if player collides with chair, set as true so they can press E..
        if (other.tag == "Sleep Chair")
            touchingChair = true;

        if (other.tag == "Exit House")
            exitHouse = true;

        if (other.tag == "Enter House")
            enterHouse = true;   

        if (other.tag == "Exit Church")
            exitChurch = true;

        if (other.tag == "Enter Church")
            enterChurch = true;     
            

        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("NPC"))
            {
                inInteractionZone = false;
                currentlyInteractingTo = null;
            }

            if (other.tag == "Sleep Chair")
                touchingChair = false;

            if (other.tag == "Exit House")
                exitHouse = false;

            if (other.tag == "Enter House")
                enterHouse = false;

            if (other.tag == "Exit Church")
                exitChurch = false;

            if (other.tag == "Enter Church")
                enterChurch = false;
        }

        public void interactionHasOccured()
        {
            if (inInteractionZone)
            {
                currentlyInteractingTo.GetComponent<Interacted>().interactionOccured();
                Debug.Log("interacting with " + currentlyInteractingTo.GetComponent<DialogueTrigger>().dialogue.name);
            }
        }
    }