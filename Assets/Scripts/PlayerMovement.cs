/**
* The following character controller was adapted from Brackey's 2D Player Movement controller
* https://www.youtube.com/watch?v=dwcT-Dch0bA&list=PLPV2KyIb3jR6TFcFuzI2bB7TMNIIBpKMQ&index=2&ab_channel=Brackeys
* And is available as free to use
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    private bool inInteractionZone = false;
    private GameObject currentlyInteractingTo;
    private bool touchingChair = false;

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

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (touchingChair) {
                Debug.Log("Touching bed and pressed E, time to go sleep");
                //TODO: Implement loading next scene/day etc
                //probably wise to start new coroutine with a cool down here to prevent spammage of E
            }
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