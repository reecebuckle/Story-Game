using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Tavern : MonoBehaviour
{
    private GameObject interactText;
    private GameObject player;

    private SpriteRenderer spriteRenderer;
    public Sprite nonHighlightedVersion;
    public Sprite highlightedVersion;

    private bool inPlace;

    
    //Awake function
    private void Awake()
    {
        interactText = GameObject.Find("Interaction Text");
        player = GameObject.Find("Player");
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        interactText.gameObject.SetActive(false);
        inPlace = false;
        Debug.Log("Start() been called");
        spriteRenderer.sprite = nonHighlightedVersion;
    }

    //checks whether player has hit 2d collider of the door
    //displays interact message, shows highlighted door
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision with player");
        if (other.CompareTag("Player"))
        {
            inPlace = true;
            interactText.gameObject.SetActive(true);
            spriteRenderer.sprite = highlightedVersion;
        }
    }

    //checks whether player has left 2d collider of the door
    //clears interact message from screen, changes sprite to original
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inPlace = false;
            interactText.gameObject.SetActive(false);
            spriteRenderer.sprite = nonHighlightedVersion;
        }
    }


    // Update is called once per frame
    void Update()
    {
        //if player interacts in the tavern collision, new scene is loaded
        if (player.GetComponent<PlayerController>().playerInputControls.Interaction.Interact.triggered && inPlace)
        {
            SceneManager.LoadScene("Tavern");
        }

        


    }
}
