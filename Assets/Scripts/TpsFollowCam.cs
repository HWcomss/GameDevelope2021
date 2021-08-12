﻿using UnityEngine;

public class TpsFollowCam : MonoBehaviour
{
    public bool useRotation = true;
    public bool isCursorLock = false;

    public float MouseXSensitivity = 6f;
    public float MouseYSensitivity = 6f;
    public bool isInvertedY = false;

    public Transform target;
    private float lookAtOffset = -.75f;  //offset for LookAt height
    private Vector3 TempTarget;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        if (isCursorLock)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isCursorLock)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;

        TempTarget = target.position;
        TempTarget -= Vector3.up * lookAtOffset;

        //mouse rotation
        int invMouseY;

        if (isInvertedY) invMouseY = -1;
        else invMouseY = 1;

        if(useRotation)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y") * invMouseY;


            offset = Quaternion.AngleAxis(mouseX * MouseXSensitivity, Vector3.up) * offset;
            offset = Quaternion.AngleAxis(mouseY * MouseYSensitivity, Vector3.left) * offset;
        }
        
        transform.LookAt(TempTarget);
    }

    void Update()
    {
        //racast
        Debug.DrawLine(this.transform.position, TempTarget, Color.cyan);

        RaycastHit hit = new RaycastHit();
        if(Physics.Linecast(TempTarget, this.transform.position, out hit) && hit.transform.tag != "Player")
        {
            Debug.DrawRay(hit.point, Vector3.left, Color.red);

            transform.position = Vector3.Lerp(transform.position, 
                new Vector3(hit.point.x, hit.point.y, hit.point.z),
                1f);
        }
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, TempTarget);
    }*/
}