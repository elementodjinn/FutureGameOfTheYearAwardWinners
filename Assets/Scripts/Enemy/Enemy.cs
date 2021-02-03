using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    [SerializeField]
    private CanvasGroup healthGroup;

    private Transform target;

    public Transform Target { get => target; set => target = value; }

    public Transform[] moveSpots; // array of spots the enemy can move to
    private int randomSpot;
    private float waitTime;
    public float startWaitTime;


    protected override void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
        base.Start();
    }

    protected override void Update()
    {
        FollowTarget();
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

    private void FollowTarget()
    {
        if (target != null)
        {
            direction = (target.transform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            direction = (moveSpots[randomSpot].position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

            if(Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    randomSpot = Random.Range(0, moveSpots.Length);
                    waitTime = startWaitTime;
                    direction = Vector2.zero; //reset movement. stop moving
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }

            }


        }
    }

}
