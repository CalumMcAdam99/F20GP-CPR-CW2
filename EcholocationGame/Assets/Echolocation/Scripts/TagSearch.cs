using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class TagSearch : MonoBehaviour
{
    private GameObject closest;
    public float min = 1f;
    public float max = 1000f;
    public float distance = Mathf.Infinity;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Particle System(Clone)")
        {
            SetItemTargets();
        }
        else if (collision.gameObject.name == "Crawl Particle System(Clone)")
        {
            SetItemTargets();
        }
        else if (collision.gameObject.name == "Sprint Particle System(Clone)")
        {
            SetItemTargets();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.name == "Item Particle System(Clone)")
        {
            SetItemTargets();
        }
    }

    public void SetItemTargets()
    {
        GameObject[] gosItems = GameObject.FindGameObjectsWithTag("Item");
    }

    public void SetFootTargets()
    {
        GameObject[] gosFoot = GameObject.FindGameObjectsWithTag("Foot");
    }

    public void FindClosestTarget(GameObject[] gos)
    {
        closest = null;
        Vector3 position = transform.position;

        min = min * min;
        max = max * max;
        //goes through each enemy object in the array
        foreach (GameObject go in gos)
        {
            //calculates the distance between the target and the enemy
            Vector3 diff = go.transform.position - position;
            //sets the current closest distance to the absoloute distance
            float curDistance = diff.sqrMagnitude;
            //checks whether or not that target is closer to any previous target
            if (curDistance < distance && curDistance >= min && curDistance <= max)
            {
                //which if one is found to be closer it updates the closest varaible to the target object that is currently being checked
                closest = go;
                distance = curDistance;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
