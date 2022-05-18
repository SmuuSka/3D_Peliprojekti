using UnityEngine.SceneManagement;
using UnityEngine;

public class ChaseState : IEnemyState
{
    private StatePatternEnemy enemy;
    

    public ChaseState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
       
        Chase();
        Look();
    }

    public void OnTriggerEnter(Collider other)
    {

    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {

    }

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
    }

    void Look()
    {
        

        Vector3 enemyToTarget = enemy.chaseTarget.position - enemy.eye.position;

        Debug.DrawRay(enemy.eye.position, enemyToTarget, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(enemy.eye.position, enemyToTarget, out hit, enemy.sightRange * 1.5f) && hit.collider.CompareTag("Player"))
        {

           enemy.isChaseOn = true;
           enemy.chaseTarget = hit.transform;
        }
        else
        {

            enemy.isChaseOn = false;
            ToPatrolState();
        }

    }

    void Chase()
    {
        
        //enemy.indicator.material.color = Color.red; *indicator*
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.isStopped = false;

        if (Vector3.Distance(enemy.navMeshAgent.gameObject.transform.position, enemy.chaseTarget.position) < 1f)
        {
            enemy.navMeshAgent.isStopped = true;
            SceneManager.LoadScene(7);

        }
    }
}
