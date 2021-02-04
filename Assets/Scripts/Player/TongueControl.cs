using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueControl : MonoBehaviour
{
    #region variables
    private Camera cam;
    private PhotonView PV;
    private Transform mouthLocation;
    private Vector2 lastLocation;
    private float movedDistance;
    private bool canAttack;
    private Collider2D tongueCollider;
    public float TongueLength;
    public float speedThreshold;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        tongueCollider = GetComponent<CircleCollider2D>();
        cam = Camera.main;
        mouthLocation = transform.parent;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine) return; // do not run following script if it is not current viewer's character.

        lastLocation = transform.position;
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition)- mouthLocation.position;
        transform.position = (Vector2)mouthLocation.position + Vector2.ClampMagnitude(mousePos, TongueLength);
        movedDistance = ((Vector2)transform.position - lastLocation).magnitude;

        tongueCollider.enabled = movedDistance >= speedThreshold; //activate only when the speed of the tongue exceeds a threshold
    
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().takeDamage((int)movedDistance + 1, transform.position);
        }
    }
}
