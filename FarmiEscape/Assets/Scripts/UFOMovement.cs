
using UnityEngine;

public class UFOMovement : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Light enemyChaseLight;
    public Transform[] waypoints;
    public float minSpeed;
    public float maxSpeed;
    private StatePatternEnemy enemy;

    private int currentWaypointIndex = 1;//menee alussa t�t� kohti
    private float speed;
    public int hoverHeight = 15;

    private void Start()
    {
        speed = minSpeed;
        enemy = GameObject.FindObjectOfType<StatePatternEnemy>();
    }

    private void Update()
    {
        if (enemy.playerRefe.enemyIsChaseMode == true)
        {
            HoverOverPlayer();
        }
        else
        {
            Patrol();
        }
    }


    public void Patrol()
    {
        LightOff();
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

    public void LightOn()
    {
        enemyChaseLight.gameObject.SetActive(true);
    }
    public void LightOff()
    {

        enemyChaseLight.gameObject.SetActive(false);

    }



    public void HoverOverPlayer()
    {
        var _speed = 25;
        LightOn();
        Vector3 playerPos = new Vector3(playerTransform.position.x, playerTransform.position.y + hoverHeight, playerTransform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, playerPos, _speed * Time.deltaTime);

        //transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + hoverHeight, playerTransform.position.z);
    }


}