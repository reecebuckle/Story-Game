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

    [Header("References to Othr Manager")]
    public GameManager gameManager;
    public ActionSystem actionSystem;

    [Header("References to the Bookshelf and letter")]
    public GameObject letter;
    public GameObject bookshelf;
    public Sprite highlightedSprite;

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
    private bool byBookshelf = false;
    private bool cooldown = false;

    private void Start()
    {
        bookshelf.GetComponent<SpriteRenderer>().sprite = null;
    }

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Player Speed", Mathf.Abs(horizontalMove));

        // if (Input.GetButtonDown("Jump"))
        // {
        //     jump = true;
        //     animator.SetBool("IsJumping", true);
        // }

        //these control player options when touching a collider
        if (Input.GetKeyDown(KeyCode.E) && !cooldown)
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

            if (enterOwlHouse && gameManager.CheckOwlEntryCondition())
                StartCoroutine(gameManager.EnterOwlHouse());

            if (exitOwlHouse)
                StartCoroutine(gameManager.LeaveOwlHouse());

            if (enterTavern && gameManager.CheckTavernEntryCondition())
                StartCoroutine(gameManager.EnterTavern());

            if (exitTavern)
                StartCoroutine(gameManager.LeaveTavern());

            if (byBookshelf && gameManager.BookshelfUnlocked())
            {
                letter.gameObject.SetActive(false);
                gameManager.TakeLetter();
            }

            StartCoroutine(WaitForCooldown());
        }
    }

    /*
    * After pressing E, wait for 1 second before being able to press E again
    */
    public IEnumerator WaitForCooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(1f);
        cooldown = false;
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
           
            if (actionSystem.getRemainingActions() != 0)
            {
                string altMsg = "Press E to Sleep. WARNING: you have remaining actions";
                touchingChair = true;
                actionSystem.OpenObjectInteractionPanel(altMsg);
            }
            else
            {
                string altMsg = "Press E to Sleep";
                touchingChair = true;
                actionSystem.OpenObjectInteractionPanel(altMsg);
            }
        }


        if (other.tag == "Enter House")
        {
            enterHouse = true;
            actionSystem.OpenObjectInteractionPanel(msg);
        }


        if (other.tag == "Exit House")
        {
            exitHouse = true;
            actionSystem.OpenObjectInteractionPanel(msg);
        }


        if (other.tag == "Enter Church")
        {
            enterChurch = true;
            actionSystem.OpenObjectInteractionPanel(msg);
        }


        if (other.tag == "Exit Church")
        {
            exitChurch = true;
            actionSystem.OpenObjectInteractionPanel(msg);
        }


        if (other.tag == "Enter Owl House")
        {

            if (gameManager.CheckOwlEntryCondition() == false)
            {
                string altMsg = "You cannot enter the Owl House currently";
                actionSystem.OpenObjectInteractionPanel(altMsg);
            }
            else
            {
                actionSystem.OpenObjectInteractionPanel(msg);
                enterOwlHouse = true;
            }

        }

        if (other.tag == "Exit Owl House")
        {
            exitOwlHouse = true;
            actionSystem.OpenObjectInteractionPanel(msg);
        }

        if (other.tag == "Enter Tavern")
        {

            if (!gameManager.CheckTavernEntryCondition())
            {
                string altMsg = "You cannot enter the Tavern currently";
                actionSystem.OpenObjectInteractionPanel(altMsg);
            }
            else
            {
                actionSystem.OpenObjectInteractionPanel(msg);
                enterTavern = true;
            }
        }

        if (other.tag == "Exit Tavern")
        {
            exitTavern = true;
            actionSystem.OpenObjectInteractionPanel(msg);
        }

        if (other.tag == "Bookshelf" && gameManager.BookshelfUnlocked())
        {
            byBookshelf = true;
            string altMsg = "Press E to take letter";
            actionSystem.OpenObjectInteractionPanel(altMsg);
            bookshelf.GetComponent<SpriteRenderer>().sprite = highlightedSprite;
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
            actionSystem.CloseObjectInteractionPanel();
        }


        if (other.tag == "Exit House")
        {
            exitHouse = false;
            actionSystem.CloseObjectInteractionPanel();
        }


        if (other.tag == "Enter House")
        {
            enterHouse = false;
            actionSystem.CloseObjectInteractionPanel();
        }


        if (other.tag == "Exit Church")
        {
            exitChurch = false;
            actionSystem.CloseObjectInteractionPanel();
        }


        if (other.tag == "Enter Church")
        {
            enterChurch = false;
            actionSystem.CloseObjectInteractionPanel();
        }


        if (other.tag == "Enter Owl House")
        {
            enterOwlHouse = false;
            actionSystem.CloseObjectInteractionPanel();
        }


        if (other.tag == "Exit Owl House")
        {
            exitOwlHouse = false;
            actionSystem.CloseObjectInteractionPanel();
        }


        if (other.tag == "Enter Tavern")
        {
            enterTavern = false;
            actionSystem.CloseObjectInteractionPanel();
        }


        if (other.tag == "Exit Tavern")
        {
            exitTavern = false;
            actionSystem.CloseObjectInteractionPanel();
        }

        if (other.tag == "Bookshelf")
        {
            byBookshelf = false;
            actionSystem.CloseObjectInteractionPanel();
            bookshelf.GetComponent<SpriteRenderer>().sprite = null;
        }

    }
}