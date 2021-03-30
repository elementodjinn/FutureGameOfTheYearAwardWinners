using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    [SerializeField]
    private CanvasGroup healthGroup;

    private Transform target;
    private PlayerHealth targetHealth;

    public Transform Target { get => target; set => target = value; }
    public PlayerHealth TargetHealth { get => targetHealth; set => targetHealth = value; }

    public Vector3 origin; // Where the object was instantiated and where it will wander around
    public float wanderRange; // how far the enemy can wander from its origin
    private Vector3 randomSpot; // Determines where the Enemy will wander to within it's allowed wanderRange.
    private float waitTime;
    private float lastAttack = 0f;

    public float attackCooldown = 2f;
    public float attackRange = 1.5f;
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
        if (Vector2.Distance(transform.position, target.position) > attackRange && Time.time - lastAttack > attackCooldown) 
        {
            //moves in to attack if attack is off cooldown and out of range
            direction = (target.transform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else if(Time.time - lastAttack > attackCooldown)
        {
            targetHealth.takeDamage(1); //player takes damage
            lastAttack = Time.time;     //attack timer resets
        } 
        else
        {
            direction = (target.transform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
        }
    }

    private void Wander() //Enemies wander when they do not have a target, and they wander around their spawn location in a radius determined by wanderRange
    {
        direction = (randomSpot - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, randomSpot, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, randomSpot) < 0.2f) //checks to see if it's near enough to the destination to choose a new destination or wait.
        {
            if (waitTime <= 0)
            {
                randomSpot = origin + new Vector3(Random.Range(-wanderRange, wanderRange), Random.Range(-wanderRange, wanderRange), 0); //chooses a new spot to wander to
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
