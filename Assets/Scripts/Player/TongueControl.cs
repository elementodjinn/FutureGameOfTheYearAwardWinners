using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueControl : MonoBehaviour
{
    #region variables
    private PhotonView PV;
    private TongueFX FX;

    //########## Tongue 
    //movement 
    private Rigidbody2D RB;
    public Camera cam;
    public float TongueLength;
    public float speedThreshold;
    private Transform mouthLocation; //transform of the mouth where tongue extends out from

    //attack
    private Vector2 lastLocation;
    private float movedDistance;
    private bool canAttack;
    private Collider2D tongueCollider;
    public GameObject splashPrefab;

    //attack cooldown
    private int attackDelay = 20;
    private int currentAttackDelay = 0;


    //########## Test Option
    public bool NOMOUSE_OPTION = false;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        PV = GetComponent<PhotonView>();
        FX = GetComponent<TongueFX>();
        tongueCollider = GetComponent<CircleCollider2D>();
        mouthLocation = transform.parent;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (currentAttackDelay <= 0)
            {
                Vector2 mousePos = (cam.ScreenToWorldPoint(Input.mousePosition) - mouthLocation.position);
                RB.AddForce(mousePos * TongueLength);
                currentAttackDelay = attackDelay;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PV.IsMine) return; // do not run following script if it is not current viewer's character.
        if (NOMOUSE_OPTION) return; //return if no mouse is turned on;

   

        if (currentAttackDelay > 0) currentAttackDelay--;


        /*
        if (currentAttackDelay-- <= 0)
        {
            lastLocation = transform.position;
            Vector2 mousePos = Vector2.ClampMagnitude(cam.ScreenToWorldPoint(Input.mousePosition) - mouthLocation.position,TongueLength);
            RB.MovePosition(Vector2.MoveTowards(transform.position, cam.ScreenToWorldPoint(Input.mousePosition), 1f));
           // Vector2.MoveTowards(transform.position, mousePos, 0.2f);
            //RB.AddForce()

            //transform.position = (Vector2)mouthLocation.position + Vector2.ClampMagnitude(mousePos, TongueLength);
            movedDistance = ((Vector2)transform.position - lastLocation).magnitude;

            tongueCollider.enabled = movedDistance >= speedThreshold; //activate only when the speed of the tongue exceeds a threshold
        }*/
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (RB.velocity.magnitude < speedThreshold) return;
            //Debug.Log(RB.velocity.magnitude);
            collision.gameObject.GetComponent<EnemyHealth>().takeDamage((int)movedDistance + 1, transform.position);
            Vector3 colliisionPos = collision.transform.position;
            FX.oneTimeEffect(TongueFX.effectTypes.ichor, collision.transform.position, Quaternion.LookRotation(transform.position - colliisionPos));
            bounce(colliisionPos, movedDistance + 1);

        }

    }


    private void bounce(Vector3 bounceFrom, float amount)
    {
        currentAttackDelay = attackDelay;
        RB.AddForce((transform.position - bounceFrom).normalized*amount * 2);
        

    }
    public void SetDelay(int newDelay)
    {
        attackDelay = newDelay;
    }

    public void givePoision()
    {
        
    }
}
