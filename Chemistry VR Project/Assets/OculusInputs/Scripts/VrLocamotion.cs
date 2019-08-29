using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrLocamotion : MonoBehaviour
{
    public bool isLeftHand;
    public bool SnapTurn;
    public bool SmoothTurn;

    private string trackpadX;
    private string trackpadY;
    private Vector2 trackpad;

    private string trackpadTurning;
    private float turningAroundY;

    public Transform vrRig;
    public Transform director;
    private Vector3 playerForward;
    private Vector3 playerRight;

    public float movementSpeed = 1.0f;
    public float smoothTurnSpeed = 1.0f;
    public float turnAngle = 1.0f;

    public CapsuleCollider upperBody;
    public CapsuleCollider lowerBody;
    public float leaningDistance;

    bool leaningOver = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Leanable")
        {
            leaningOver = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Leanable")
        {
            leaningOver = false;
        }
    }

    void OnEnable()
    {
        if(isLeftHand)
        {
            trackpadX = "Left Horizontal";
            trackpadY = "Left Vertical";
            if(SmoothTurn)
            {
                trackpadTurning = "Right Horizontal";
            }
        }
        else
        {
            trackpadX = "Right Horizontal";
            trackpadY = "Right Vertical";
            if(SmoothTurn)
            {
                trackpadTurning = "Left Horizontal";
            }
        }
    }

    void Update()
    {
        upperBody.center = new Vector3(director.localPosition.x, director.localPosition.y - 0.42f, director.localPosition.z);
        if (!leaningOver)
        {
            lowerBody.center = new Vector3(director.localPosition.x, -1.25f, director.localPosition.z);
        }

        if (Vector3.Distance(upperBody.center, lowerBody.center) > leaningDistance)
        {
            lowerBody.center = Vector3.MoveTowards(lowerBody.center, new Vector3(upperBody.center.x, -1.25f, upperBody.center.z), 0.01f);
        }

        trackpad = new Vector2(Input.GetAxis(trackpadX), Input.GetAxis(trackpadY));
        
        if (trackpad.magnitude < 0.2f)
        {
            trackpad = Vector2.zero;
        }

        if (SmoothTurn)
        {
            turningAroundY = Input.GetAxis(trackpadTurning);
            if (Mathf.Abs(turningAroundY) < 0.2f)
            {
                turningAroundY = 0;
            }
            vrRig.Rotate(Vector3.up * turningAroundY * turnAngle * smoothTurnSpeed);
        }
        playerForward = director.forward;
        playerForward.y = 0;
        playerForward.Normalize();
        playerRight = director.right;
        playerRight.y = 0;
        playerRight.Normalize();

        vrRig.Translate(playerForward * trackpad.y * Time.deltaTime * movementSpeed, Space.World);
        vrRig.Translate(playerRight * trackpad.x * Time.deltaTime * movementSpeed, Space.World);
    }
}