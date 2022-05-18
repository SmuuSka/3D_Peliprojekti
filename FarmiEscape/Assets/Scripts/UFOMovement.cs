
using UnityEngine;

public class UFOMovement : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    public Transform[] waypoints;
    public float minSpeed;
    public float maxSpeed;
    private StatePatternEnemy enemy;

    private int currentWaypointIndex = 1;//menee alussa t�t� kohti
    private float speed;

    private void Start()
    {
        speed = minSpeed;
    }
    private void Patrol()
    {
        Transform wp = waypoints[currentWaypointIndex];
        //jos et�isyys pienempi kuin 0.01f, niin asetetaan uusi waypoint
        if (Vector3.Distance(transform.position, wp.position) < 0.01f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            currentWaypointIndex = (Random.Range(0, waypoints.Length)) % waypoints.Length;
            speed = Random.Range(minSpeed, maxSpeed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                wp.position,
                speed * Time.deltaTime);
        }
    }

    private void Update()
    
    {
        if(enemy.currentState == enemy.chaseState)
        {
            transform.position = playerTransform.position;

        }
        else
        {
            Patrol();
        }
        
    }


}