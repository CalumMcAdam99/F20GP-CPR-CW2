using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public AudioClip collisionAudio;

    float throwForce = 500;
    Vector3 objectPos;
    float distance;

    public bool canHold = true;
    public GameObject item;
    public GameObject tempParent;

    private bool isHolding = false;
    private bool objectThrown = false;

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(item.transform.position, tempParent.transform.position);
        if (distance >= 1f)
        {
            isHolding = false;
        }

        //Check if isHolding
        if (isHolding)
        {
            item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            item.transform.SetParent(tempParent.transform);

            if(Input.GetMouseButtonDown(1))
            {
                item.GetComponent<Rigidbody>().AddForce(tempParent.transform.forward * throwForce);
                isHolding = false;
                objectThrown = true;
            }
        }
        else
        {
            objectPos = item.transform.position;
            item.transform.SetParent(null);
            item.GetComponent<Rigidbody>().useGravity = true;
            item.transform.position = objectPos;
        }
    }

    private void OnMouseDown()
    {
        if (distance <= 1f)
        {
            isHolding = true;
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().detectCollisions = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (objectThrown)
        {
            if (collision.gameObject.GetComponent<Player>())
            {
                //Do nothing
            } else
            {
                AudioSource.PlayClipAtPoint(collisionAudio, transform.position, 0.5f);
                objectThrown = false;
            }
        }
    }
}
