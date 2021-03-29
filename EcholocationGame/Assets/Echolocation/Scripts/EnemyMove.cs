using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyMove : MonoBehaviour
{
    float startingTime = 10f;
    float currentTime = 7f;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    public Vector3 coord;
    bool walkPointSet;
    public float walkPointRange;

    public float sightRange;
    public bool playerInSight;

    public bool canSearch;
    public bool foundClosest;
    public int isSearched;

    public float min = 1f;
    public float max = 1000f;
    public float distance;
    GameObject[] gosItems;
    GameObject[] gosFoot;
    List<Vector3> goList;
    private int itemtagCount;
    private int foottagCount;

    private float delayInSeconds = 0.25f;

    void Start()
    {
        isSearched = 0;
        canSearch = true;
        foundClosest = false;

    }

    private void Awake()
    {
        //player = GameObject.Find("PlayerObj").transform;
        agent = this.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        if (playerInSight) Chasing();
        currentTime -= 1 * Time.deltaTime;

        if (currentTime <= 0f)
        {
            walkPointSet = false;
            currentTime = startingTime;
        }

        if (isSearched == 1)
        {
            isSearched = 0;
        }

        itemtagCount = GameObject.FindGameObjectsWithTag("Item").Length;
        foottagCount = GameObject.FindGameObjectsWithTag("Foot").Length;

        if(foundClosest == true)
        {

            UnityEngine.Debug.Log(coord);
            agent.SetDestination(coord);
            foundClosest = false;
        }


        if (!playerInSight) Patroling();

        //   if (SoundHeard) Searching();

    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) agent.SetDestination(walkPoint);


        Vector3 distanceToPoint = transform.position - walkPoint;
        if (distanceToPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void Chasing()
    {
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.name == "Item Particle System(Clone)")
        {

            SetItemTargets();
        }
        else if (other.gameObject.name == "Particle System(Clone)")
        {
            SetFootTargets();
        }
        else if (other.gameObject.name == "Crawl Particle System(Clone)")
        {
            SetFootTargets();
        }
        else if (other.gameObject.name == "Sprint Particle System(Clone)")
        {
            SetFootTargets();
        }
    }

    public void SetItemTargets()
    {
        if(itemtagCount >0)
        {
            if (isSearched == 0 && canSearch == true)
            {
                gosItems = GameObject.FindGameObjectsWithTag("Item");
                isSearched = 1;
                canSearch = false;
                FindClosestTarget(gosItems);
                StartCoroutine(SearchDelay());
            }
        }

    }

    public void SetFootTargets()
    {

        if (foottagCount >0)
        {
            if (isSearched == 0 && canSearch == true)
            {
                gosFoot = GameObject.FindGameObjectsWithTag("Foot");
                isSearched = 1;
                canSearch = false;
                FindClosestTarget(gosFoot);
                StartCoroutine(SearchDelay());
            }
        }
    }

    IEnumerator SearchDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        canSearch = true;
    }

    public void FindClosestTarget(GameObject[] gos)
    {
        distance = Mathf.Infinity;
        goList = new List<Vector3>();
        Vector3 position = transform.position;

        min = min * min;
        max = max * max;
        //goes through each enemy object in the array
        foreach (GameObject go in gos)
        {
            if (go != null)
            {
                goList.Add(go.transform.position);
            }
            foreach(Vector3 go1 in goList)
            {
                //calculates the distance between the target and the enemy
                Vector3 diff = go1 + position;
                //sets the current closest distance to the absoloute distance
                float curDistance = diff.sqrMagnitude;
                //checks whether or not that target is closer to any previous target
                if (curDistance < distance && curDistance >= min && curDistance <= max)
                {
                    //which if one is found to be closer it updates the closest varaible to the target object that is currently being checked
                    coord = go1;
                    distance = curDistance;

                }
            }
        }
        foundClosest = true;
    }
}


