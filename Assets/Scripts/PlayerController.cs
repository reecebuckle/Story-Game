//refering to these vids
//https://youtu.be/yRI44aYLDQs pt1
//https://youtu.be/w1vC32e11wU pt2

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //speed var can be modded in editor but not by other classes
    [SerializeField] private float speed;

    //getting the input control maps
    private PlayerInputControls playerInputControls;

    //called before start
    private void Awake()
    {
        playerInputControls = new PlayerInputControls();
    }

    //enables control maps
    private void OnEnable()
    {
        playerInputControls.Enable();
    }

    //disables control maps
    private void OnDisable()
    {
        playerInputControls.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Read movement value
        float movementInput = playerInputControls.Movement.Move.ReadValue<float>();

        //Move the player
        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInput * speed * Time.deltaTime;
        transform.position = currentPosition;
        

    }
}
