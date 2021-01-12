using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndDay : MonoBehaviour
{
    [Header("Text to Display")]
    public GameObject m_Text;


    //initially set text to inactive
    private void Awake()
    {
        m_Text.SetActive(false);
    }


    //checks whether player has left 2d collider of chair
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            m_Text.SetActive(true);

    }

    //checks whether player has left 2d collider of chair
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            m_Text.SetActive(false);
    }
}

