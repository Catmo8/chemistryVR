using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrHands : MonoBehaviour
{

    public GameObject collidingObject;
    public GameObject heldObject;

    public Transform bothHeldCenter;
    public Transform otherHand;
    public static Transform firstHand;
    public static Transform secondHand;

    static int handCount;

    public Animator anim;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "TwoHandInteractable" || other.tag == "OneHandInteractable")
        {
            collidingObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == collidingObject)
        {
            collidingObject = null;
        }
        if(handCount == 2 && other.gameObject == bothHeldCenter.GetChild(0))
        {
            anim.SetInteger("Pose", 0);
            handCount--;
            bothHeldCenter.GetChild(0).SetParent(otherHand);
            firstHand = otherHand;
        }
    }
    #region inputMapping
    public bool isLeftHand;
    private string grip;
    private bool gripHeld;

    // Start is called before the first frame update
    void Start()
    {
        if (isLeftHand)
        {
            grip = "Left Grip";
        }
        else
            grip = "Right Grip";
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Flex", Input.GetAxis(grip));

        if(Input.GetAxis(grip) > 0.5f && !gripHeld)
        {
            gripHeld = true;
            if (collidingObject)
                Grab();
        }
        else if (Input.GetAxis(grip) < 0.5f && gripHeld)
        {
            gripHeld = false;
            if (transform.childCount == 2)
                Release();
            else if (handCount == 2)
            {
                anim.SetInteger("Pose", 0);
                handCount--;
                bothHeldCenter.GetChild(0).SetParent(otherHand);
                firstHand = otherHand;
            }
        }
        
        if (handCount == 2)
        {
            bothHeldCenter.position = firstHand.position;
            bothHeldCenter.LookAt(secondHand);
        }
    }

    void Grab()
    {
        anim.SetInteger("Pose", 1);
        handCount++;
        if (collidingObject.tag == "OneHandInteractable")
            handCount = 1;
        else if (handCount == 2)
        {
            secondHand = transform;
            bothHeldCenter.position = firstHand.position;
            bothHeldCenter.LookAt(secondHand);
            collidingObject.transform.SetParent(bothHeldCenter);
            return;
        }
        firstHand = transform;
        collidingObject.transform.SetParent(transform);
        otherHand.GetComponentInChildren<Animator>().SetInteger("Pose", 0);
    }

    void Release()
    {
        anim.SetInteger("Pose", 0);
        transform.GetChild(1).SetParent(null);
        handCount--;
        firstHand = null;
    }
}
