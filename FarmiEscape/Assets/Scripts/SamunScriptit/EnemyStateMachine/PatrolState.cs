using UnityEngine;

public class PatrolState : IEnemyState
{
    private StatePatternEnemy enemy;
    int nextWaypoint;
    private Vector3 nextPoint = Vector3.zero;
    private float timeSec;


    public PatrolState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Patrol();
        Look();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ToAlertState();
        }
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }

    public void ToPatrolState()
    {

    }

    void Look()
    {
        Debug.DrawRay(enemy.eye.position, enemy.eye.forward * enemy.sightRange, Color.green);

        

        //RaycastHit hit;
        //if (Physics.Raycast(enemy.eye.position, enemy.eye.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        //{
        //    Debug.DrawRay(enemy.eye.position, enemy.eye.forward * enemy.sightRange, Color.red);
        //    enemy.chaseTarget = hit.transform;
        //    ToChaseState();
        //}



        //Debug.DrawRay(enemy.eye.position, hit.point, Color.red);
        //enemy.chaseTarget = hit.transform;
        //ToChaseState();

        enemy.colliderList = Physics.OverlapSphere(enemy.eye.position, enemy.sightRange, enemy.playerLayer);
        if (enemy.colliderList.Length > 0)
        {
            enemy.chaseTarget = enemy.colliderList[0].gameObject.transform;
            ToAlertState();
        }
        else
        {
            return;
        }
        
    }

    void Patrol()
    {
        //Debug.Log("next point: " + nextPoint);

        if (nextPoint == Vector3.zero)
        {
            enemy.navMeshAgent.SetDestination(SetNextWalkPos());

        }

        //enemy.indicator.material.color = Color.green; *indicator*
        //enemy.navMeshAgent.destination = enemy.waypoints[nextWaypoint].position; *waypoint*
        enemy.navMeshAgent.isStopped = false;

        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            //nextWaypoint = (nextWaypoint + 1) % enemy.waypoints.Length; *waypoint*
            timeSec += Time.deltaTime;
            if (timeSec > 3f)
            {
                nextPoint = Vector3.zero;
                timeSec = 0;
                return;
            }
        }
    }

    private Vector3 SetNextWalkPos()
    {
        //Randomisoidaan Z ja X-arvot
        float randomZ = Random.Range(-enemy.walkPointRange, enemy.walkPointRange);
        float randomX = Random.Range(-enemy.walkPointRange, enemy.walkPointRange);

        //Luodaan uusi kohde
        enemy.walkPoint = new Vector3(enemy.transform.position.x + randomX, enemy.gameObject.transform.position.y, enemy.transform.position.z + randomZ);

        //Palautetaan uusi kohde
        nextPoint = enemy.walkPoint;
        return enemy.walkPoint;
    }




}
