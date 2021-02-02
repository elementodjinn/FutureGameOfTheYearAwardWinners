using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Written by Kyle Kreml
    #region Variables

    //This is just a general speed multiplier for the player, haven't tried anything else yet.
    public float speed;

    float moveHorizontal = 0f;
    float moveVertical = 0f;

    private Rigidbody2D playerRigidbody;

    private Animator animator;
    private SpriteRenderer SR;
    private PhotonView PV;

    #endregion

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
        PV = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (!PV.IsMine) return;     //disables it if the owner is not the current viewer.
        moveHorizontal = 0f;
        moveVertical = 0f;
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


        //Animation part
        animator.SetFloat("VerticalSpeed", moveVertical);
        animator.SetFloat("HorizontalSpeed", moveHorizontal);
        if (moveHorizontal > 0)
        {
            SR.flipX = false;
        }
        else if (moveHorizontal < 0)
        {
            SR.flipX = true;
        }

    }
    void FixedUpdate()
    {
        if (!PV.IsMine) return;     //disables it if the owner is not the current viewer.

        //setting velocity because I don't know how to make smooth acceleration with a maximum velocity. 
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        playerRigidbody.velocity = movement * speed;
    }
}
