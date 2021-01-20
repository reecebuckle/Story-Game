/**
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

    [Header("Reference to Game Manager")]
    public GameManager gameManager;

    //Collision detection bools
    private bool touchingChair = false;
    private bool exitHouse = false;
    private bool enterHouse = false;
    private bool enterChurch = false;
    private bool exitChurch = false;
    private bool enterTavern = false;
    private bool exitTavern = false;
    private bool enterOwlHouse = false;
    private bool exitOwlHouse = false;

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Player Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        //these control player options when touching a collider
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (touchingChair)
                StartCoroutine(gameManager.RestForDay());

            if (exitHouse)
                StartCoroutine(gameManager.LeaveHouse());

            if (enterHouse)
                StartCoroutine(gameManager.EnterHouse());

            if (exitChurch)
                StartCoroutine(gameManager.LeaveChurch());

            if (enterChurch)
                StartCoroutine(gameManager.EnterChurch());

            if (enterOwlHouse)
            {

                if (gameManager.CheckOwlEntryCondition())
                    StartCoroutine(gameManager.EnterOwlHouse());
                else
                    //TODO: display a message saying you don't have acess
                    Debug.Log("TODO cannot enter house currently!");
            }


            if (exitOwlHouse)
                StartCoroutine(gameManager.LeaveOwlHouse());

            if (enterTavern) {
                if (gameManager.CheckTavernEntryCondition())
                    StartCoroutine(gameManager.EnterTavern());
                else
                    //TODO: display a message saying you don't have acess
                    Debug.Log("TODO cannot enter house currently!");
            }

            if (exitTavern)
                StartCoroutine(gameManager.LeaveTavern());
        }
    }

    /*
    * Handles player movement 
    */
    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }

    /*
    * Invoked when player collides with ground to stop jumping animation
    */
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    /*
    * The following methods check if player has collided with a speicfic game object
    */
    void OnTriggerEnter2D(Collider2D other)
    {
        string msg = "Press E to " + other.tag;

        if (other.tag == "Sleep Chair")
        {
            msg = "Press E to sleep, consumes all remaining actions";
            touchingChair = true;
            gameManager.actionSystem.OpenObjectInteractionPanel(msg);
        }
           

        if (other.tag == "Enter House")
        {
            enterHouse = true;
            gameManager.actionSystem.OpenObjectInteractionPanel(msg);
        }
            

        if (other.tag == "Exit House") {
            exitHouse = true;
            gameManager.actionSystem.OpenObjectInteractionPanel(msg);
        }
            

        if (other.tag == "Enter Church") {
            enterChurch = true;
            gameManager.actionSystem.OpenObjectInteractionPanel(msg);
        }
            

        if (other.tag == "Exit Church") {
            exitChurch = true;
            gameManager.actionSystem.OpenObjectInteractionPanel(msg);
        }
            

        if (other.tag == "Enter Owl House") {
            enterOwlHouse = true;
            gameManager.actionSystem.OpenObjectInteractionPanel(msg);
        }
            

        if (other.tag == "Exit Owl House") {
            exitOwlHouse = true;
            gameManager.actionSystem.OpenObjectInteractionPanel(msg);
        }
            

        if (other.tag == "Enter Tavern") {
            enterTavern = true;
            gameManager.actionSystem.OpenObjectInteractionPanel(msg);
        }
            

        if (other.tag == "Exit Tavern") {
            exitTavern = true;
            gameManager.actionSystem.OpenObjectInteractionPanel(msg);
        }
            
    }

    /*
    * The following methods check if player has left the 2D collider of a specific game object
    */
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Sleep Chair")
        {
            touchingChair = false;
            gameManager.actionSystem.CloseObjectInteractionPanel();
        }
            

        if (other.tag == "Exit House")
        {
            exitHouse = false;
            gameManager.actionSystem.CloseObjectInteractionPanel();
        }
            

        if (other.tag == "Enter House")
        {
            enterHouse = false;
            gameManager.actionSystem.CloseObjectInteractionPanel();
        }
            

        if (other.tag == "Exit Church")
        {
            exitChurch = false;
            gameManager.actionSystem.CloseObjectInteractionPanel();
        }
            

        if (other.tag == "Enter Church")
        {
            enterChurch = false;
            gameManager.actionSystem.CloseObjectInteractionPanel();
        }
            

        if (other.tag == "Enter Owl House")
        {
            enterOwlHouse = false;
            gameManager.actionSystem.CloseObjectInteractionPanel();
        }
            

        if (other.tag == "Exit Owl House")
        {
            exitOwlHouse = false;
            gameManager.actionSystem.CloseObjectInteractionPanel();
        }
            

        if (other.tag == "Enter Tavern")
        {
            enterTavern = false;
            gameManager.actionSystem.CloseObjectInteractionPanel();
        }
            

        if (other.tag == "Exit Tavern")
        {
            exitTavern = false;
            gameManager.actionSystem.CloseObjectInteractionPanel();
        }
            
    }
}