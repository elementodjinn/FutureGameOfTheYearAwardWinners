using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntWorkerBehavior : Enemy
{
    protected override void Start()
    {
        base.Start();
    }
    /*
    protected override void AttackBehavior()
    {
        direction = (Target.transform.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
    }
    */
}
