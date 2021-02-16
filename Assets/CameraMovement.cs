using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform target;
    public float lerpValue = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.parent;
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, target.position, lerpValue);
    }
}
