using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Written by Kyle Kreml
    #region Variables

    //This is just a general speed multiplier for the player, haven't tried anything else yet.
    public float speed;

    private Rigidbody2D playerRigidbody;

    #endregion

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        if (Input.GetKey("a"))
        {
            moveHorizontal = -1f;
        }
        if (Input.GetKey("d"))
        {
            moveHorizontal = 1f;
        }
        if (Input.GetKey("s"))
        {
            moveVertical = -1f;
        }
        if (Input.GetKey("w"))
        {
            moveVertical = 1f;
        }
        //Here I am simply attempting to make the player not move 'faster' if they move diagonally. Haven't tried anything else yet.
        if (Mathf.Abs(moveHorizontal) == Mathf.Abs(moveVertical) && moveHorizontal != 0)
        {
            moveHorizontal *= .7f;
            moveVertical *= .7f;
        }

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //setting velocity because I don't know how to make smooth acceleration with a maximum velocity. 
        playerRigidbody.velocity = movement * speed;
    }
}
