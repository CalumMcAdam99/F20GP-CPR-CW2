using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    float startingTime = 10f;
    float currentTime = 7f;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float sightRange;
    public bool playerInSight;

    private GameObject closest;
    public float min = 1f;
    public float max = 1000f;
    public float distance = Mathf.Infinity;

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
        agent.SetDestination(player.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.name == "Item Particle System(Clone)")
        {
            SetItemTargets();
        }

        if (other.gameObject.name == "Particle System(Clone)")
        {
            SetItemTargets();
        }
        else if (other.gameObject.name == "Crawl Particle System(Clone)")
        {
            SetItemTargets();
        }
        else if (other.gameObject.name == "Sprint Particle System(Clone)")
        {
            SetItemTargets();
        }
    }

    public void SetItemTargets()
    {
        GameObject[] gosItems = GameObject.FindGameObjectsWithTag("Item");
        FindClosestTarget(gosItems);
    }

    public void SetFootTargets()
    {
        GameObject[] gosFoot = GameObject.FindGameObjectsWithTag("Foot");
        FindClosestTarget(gosFoot);
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
        agent.SetDestination(closest.transform.position);
    }
}


