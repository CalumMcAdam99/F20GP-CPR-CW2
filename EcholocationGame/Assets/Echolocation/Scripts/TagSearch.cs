using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[RequireComponent(typeof(Rigidbody2D))]
public class TagSearch : MonoBehaviour
{
    private Transform enemy;
    private Vector3 target;
    Vector3 targetPos;
    private GameObject closest;
    public float min = 1f;
    public float max = 1000f;
    public float distance = Mathf.Infinity;

    // Start is called before the first frame update
    void Start()
    {
        FindClosestEnemy();
        try
        {


            enemy = closest.transform;
            target = new Vector3(enemy.position.x, enemy.position.y, enemy.position.z);
            //once the closest enemy has been found it changes it rotation to face this enemy
            Vector3 lookDir = target;// - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
            //rb.rotation = angle;

        }
        catch (Exception)
        {
        }

    }

    public void FindClosestEnemy()
    {
        //searches for all enemy tags and concatenates them to one searchable array
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Foot");
        GameObject[] gos1 = GameObject.FindGameObjectsWithTag("Item");
        GameObject[] allGos = gos.Concat(gos1).ToArray();
        closest = null;
        Vector3 position = transform.position;

        min = min * min;
        max = max * max;
        //goes through each enemy object in the array
        foreach (GameObject go in allGos)
        {
            //calculates the distance between the enemy and the players projectile
            Vector3 diff = go.transform.position - position;
            //sets the current closest distance to the absoloute distance
            float curDistance = diff.sqrMagnitude;
            //checks whether or not that enemy is closer to any previous enemy
            if (curDistance < distance && curDistance >= min && curDistance <= max)
            {
                //which if one is found to be closer it updates the closest varaible to the enemy object that is currently being checked
                closest = go;
                distance = curDistance;
            }
        }
        //return closest;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
