using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UfoController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
     //Patroling
    public Vector3 walkPoint, enemymovepoint;
    bool walkPointSet;
    public float walkPointRange;
     //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, enemyWalkPointReach;
    public LayerMask whatIsPlayer, whatIsGround;
    private void Awake()
    

    {
        player = GameObject.Find("Player1").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    
 
    // Start is called before the first frame update
    void Start()
    {
        enemymovepoint = SearchWalkPoint();
     
        Debug.Log("walkpointisset" + enemymovepoint);  
        Patroling(enemymovepoint);     
    }
    private IEnumerator NextWalkPointDelay()
    {
        yield return new WaitForSeconds(3f);
       // SearchWalkPoint();
        Patroling(SearchWalkPoint());
        StopCoroutine(NextWalkPointDelay());
    }

    // Update is called once per frame
    void Update()    
     {
        
        // //Check for sight and attack range
        // playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        // //playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        // if (!playerInSightRange && !playerInAttackRange && enemyWalkPointReach) Patroling();
        // //if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        // //if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }
    private void Patroling(Vector3 target)
    {
        agent.SetDestination(target);
        if(agent.remainingDistance <= 1)
        {
            enemyWalkPointReach = true;
            StartCoroutine(NextWalkPointDelay());
        }         
    }
    //  private void Patroling()

    // {
    //     if (!walkPointSet) SearchWalkPoint();
    //     if (walkPointSet)
    //         agent.SetDestination(walkPoint);
    //     Vector3 distanceToWalkPoint = transform.position - walkPoint;
    //     //Walkpoint reached
    //     if (distanceToWalkPoint.magnitude < 1f)
    //         walkPointSet = false;
    //         enemyWalkPointReach = true;
    // }
    private Vector3 SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        
        //if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
          //  walkPointSet = true;
        return walkPoint;
    }

    // private void SearchWalkPoint()

    // {

    //     //Calculate random point in range

    //     float randomZ = Random.Range(-walkPointRange, walkPointRange);

    //     float randomX = Random.Range(-walkPointRange, walkPointRange);



    //     walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);



    //     if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))

    //         walkPointSet = true; 
    //         StartCoroutine(NextWalkPointDelay());

    // }
}

