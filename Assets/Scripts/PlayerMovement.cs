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
    private GameObject currentlyInteractingTo;

    //Collision detection bools
    private bool inInteractionZone = false;
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
            Debug.Log("jumping");
            animator.SetBool("IsJumping", true);
        }

        //these control player options when touching a collider
        if (Input.GetKeyDown(KeyCode.E))
        {

            if (touchingChair)
            {
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

            if (enterOwlHouse)
                FindObjectOfType<GameManager>().EnterOwlHouse();

            if (exitOwlHouse)
                FindObjectOfType<GameManager>().LeaveOwlHouse();

            if (enterTavern)
                FindObjectOfType<GameManager>().EnterTavern();

            if (exitTavern)
                FindObjectOfType<GameManager>().LeaveTavern();

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
        if (other.CompareTag("NPC"))
        {
            inInteractionZone = true;
            currentlyInteractingTo = other.gameObject;
        }

        if (other.tag == "Sleep Chair")
            touchingChair = true;

        if (other.tag == "Enter House")
            enterHouse = true;

        if (other.tag == "Exit House")
            exitHouse = true;

        if (other.tag == "Enter Church")
            enterChurch = true;

        if (other.tag == "Exit Church")
            exitChurch = true;

        if (other.tag == "Enter Owl House") 
            enterOwlHouse = true;

        if (other.tag == "Exit Owl House") 
            exitOwlHouse = true;

        if (other.tag == "Enter Tavern") 
            enterTavern = true;

        if (other.tag == "Exit Tavern") 
            exitTavern = true;


    }

    /*
    * The following methods check if player has left the 2D collider of a specific game object
    */
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

        if (other.tag == "Enter Owl House")
            enterOwlHouse = false;

        if (other.tag == "Exit Owl House") 
            exitOwlHouse = false;

        if (other.tag == "Enter Tavern") 
            enterTavern = false;

        if (other.tag == "Exit Tavern") 
            exitTavern = false;
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