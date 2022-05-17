using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyState
{
    private StatePatternEnemy enemy;
    float searchTimer;

    public AlertState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Search();
        Look();
    }

    public void OnTriggerEnter(Collider other)
    {

    }

    public void ToAlertState()
    {

    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }

    public void ToPatrolState()
    {
        searchTimer = 0;
        enemy.currentState = enemy.patrolState;
    }

    void Look()
    {
        Debug.DrawRay(enemy.eye.position, enemy.eye.forward * enemy.sightRange, Color.yellow);


        //RaycastHit hit;

        //if (Physics.Raycast(enemy.eye.position, enemy.eye.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        //{
        //    ToChaseState();
        //    //enemy.chaseTarget = hit.transform;


        //}
        enemy.chaseList = Physics.OverlapSphere(enemy.eye.position, enemy.sightRange * 1.5f, enemy.playerLayer);
        if (enemy.chaseList.Length > 0)
        {
            enemy.chaseTarget = enemy.chaseList[0].gameObject.transform;
            ToChaseState();
        }
        else
        {
            ToPatrolState();
            return;
        }

    }

    void Search()
    {
        //enemy.indicator.material.color = Color.yellow; *indicator*
        enemy.navMeshAgent.isStopped = true;
        enemy.transform.Rotate(0, enemy.searchTurnSpeed * Time.deltaTime, 0);

        searchTimer += Time.deltaTime;

        if (searchTimer >= enemy.searchDuration)
        {
            ToPatrolState();
        }

    }
}
