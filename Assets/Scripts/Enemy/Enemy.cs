using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    [SerializeField]
    private CanvasGroup healthGroup;

    private Transform target;

    public Transform Target { get => target; set => target = value; }

    public Vector3 origin; // Where the object was instantiated
    public float wanderRange; // how far the enemy can wander from its origin
    private Vector3 randomSpot;
    private float waitTime;
    public float movementRestInterval;
    private Rigidbody2D RB;


    protected override void Start()
    {
        waitTime = movementRestInterval;
        RB = GetComponent<Rigidbody2D>();
        origin = hitBox.transform.position;
        randomSpot = origin + new Vector3(Random.Range(-wanderRange, wanderRange), Random.Range(-wanderRange, wanderRange), 0);
        base.Start();
    }

    protected override void Update()
    {
        if (target != null)
        {
            AttackBehavior();
        }
        else
        {
            Wander();
        }
        base.Update();
    }

    public override Transform Select()
    {
        healthGroup.alpha = 1;
        return base.Select();
    }

    public override void  DeSelect()
    {
        healthGroup.alpha = 0;
        base.DeSelect();
    }

    protected virtual void AttackBehavior() //allows for override in different enemy behaviors
    {
        direction = (target.transform.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void Wander()
    {
        direction = (randomSpot - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, randomSpot, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, randomSpot) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = origin + new Vector3(Random.Range(-wanderRange, wanderRange), Random.Range(-wanderRange, wanderRange), 0);
                waitTime = movementRestInterval;
                direction = Vector2.zero; //reset movement. stop moving
            }
            else
            {
                waitTime -= Time.deltaTime;
            }

        }
    }

}
