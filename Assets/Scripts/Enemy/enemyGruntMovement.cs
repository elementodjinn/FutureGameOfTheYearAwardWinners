using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGruntMovement : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D myRigidbody;



    private bool moving; // is the enemy moving or not

    public float timeBetweenMove; //how long it takes between each movement
    private float timeBetweenMoveCounter;
    public float timeToMove; //how long it takes to move and how long it moves for
    private float timeToMoveCounter;

    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        timeBetweenMoveCounter = timeBetweenMove;
        timeToMoveCounter = timeToMove;

    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            timeToMoveCounter -= Time.deltaTime;
            myRigidbody.velocity = moveDirection; // how fast emeny is moving

            if (timeBetweenMoveCounter < 0f)
            {
                moving = false;
                timeBetweenMoveCounter = timeBetweenMove;
            }
        }
        else
        {
            timeBetweenMoveCounter -= Time.deltaTime; //time it takes one update of the screen
            myRigidbody.velocity = Vector2.zero;//stop moving
            if (timeBetweenMoveCounter < 0f)
            {
                moving = true;
                timeToMoveCounter = timeToMove; //reset counter

                moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f); //pick random num between 1 & -1 on X & Y axis --> chooses enemy direction. Might want to change later to go towards player if within certain range. 
            }
        }

        

    }
}

