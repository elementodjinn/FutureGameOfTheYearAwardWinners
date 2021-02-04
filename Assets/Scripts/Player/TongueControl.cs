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
    public float TongueLength;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        cam = Camera.main;
        mouthLocation = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {

        if (!PV.IsMine) return;
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition)- mouthLocation.position;
        transform.position = (Vector2)mouthLocation.position + Vector2.ClampMagnitude(mousePos, TongueLength);

    }
}
