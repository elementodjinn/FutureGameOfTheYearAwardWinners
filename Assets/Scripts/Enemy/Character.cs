using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*[RequireCompnent(typeof(Rigidbody2D))]
[RequireCompnent(typeof(Animator))]*/

public abstract class Character : MonoBehaviour // can't exist w/o enemy
{
    [SerializeField]
    protected float speed;

    protected Vector2 direction;

    private Animator myAnimator;
    private Rigidbody2D myRigidbody;

    [SerializeField]
    protected Transform hitBox;

    public bool moving
    {
        get
        {
            return direction.x != 0 && direction.y != 0;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        myAnimator = GetComponent<Animator>(); // animations
        myRigidbody = GetComponent<Rigidbody2D>(); // ref to rigidbody
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    protected virtual void Update() // allows override update in enemy script
    {
        animControls();
    }

    public void Move()
    {
        myRigidbody.velocity = direction.normalized * speed; // how fast emeny is moving, framerate independent
        
    }

    public void animControls()
    {

        if (moving)
        {
            ActivateLayer("Enemy_Walk"); // walking

            myAnimator.SetFloat("x", direction.x); //change direction when moving left or right
            myAnimator.SetFloat("y", direction.y); // change direction when moving up or down
        }
        else
        {
            ActivateLayer("Enemy_Idle"); // idle animation
        }
        // add attack later
    }


    public void ActivateLayer(string LayerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }

        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(LayerName), 1);
    }

    public virtual void TakeDamage(float damage)
    {
        //health
    }
}
