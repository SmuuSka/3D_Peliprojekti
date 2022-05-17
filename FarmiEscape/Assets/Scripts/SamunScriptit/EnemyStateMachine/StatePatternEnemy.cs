using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class StatePatternEnemy : MonoBehaviour
{
    
    public Collider[] colliderList = new Collider[0];
    public Collider[] chaseList = new Collider[0];
    public LayerMask playerLayer;

    public float searchTurnSpeed;
    public float searchDuration;
    public float sightRange;
    //new
    public float walkPointRange, attackRange, timeSec;
    public Vector3 walkPoint;

    //public Transform[] waypoints; *waypoint*
    public Transform eye;
    //public MeshRenderer indicator; *indicator*

    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public IEnemyState currentState;
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public NavMeshAgent navMeshAgent;

    private void Awake()
    {
     
        patrolState = new PatrolState(this);
        alertState = new AlertState(this);
        chaseState = new ChaseState(this);

        
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        currentState = patrolState;
    }

    private void Update()
    {
        currentState.UpdateState();
        Debug.Log("Current State: " + currentState);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    public void OnDrawGizmos()
    {
       // Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(eye.position, sightRange);
        Gizmos.DrawWireSphere(eye.position, sightRange);
    }
}
