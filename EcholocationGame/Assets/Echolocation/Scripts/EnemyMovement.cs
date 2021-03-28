using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    float startingTime = 10f;
    float currentTime = 10f;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float sightRange;
    public bool playerInSight;

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
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    // need to do
    private void Searching()
    {

    }

    private void Chasing()
    {
        agent.SetDestination(player.position);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        // if (collisioninfo.gameObject.tag == "tag"){
        //   agent.SetDestination(collisionInfo.gameObject.tag == "tag");
        //}
    }
}

    
