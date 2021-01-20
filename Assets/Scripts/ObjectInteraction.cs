using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public Sprite highlightedSprite;

    private void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            this.GetComponent<SpriteRenderer>().sprite = highlightedSprite;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.GetComponent<SpriteRenderer>().sprite = null;
    }
}
