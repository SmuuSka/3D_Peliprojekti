using UnityEngine;
using UnityEngine.AI;

public class UfoController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;


    private Vector3 walkPoint;
    public float walkPointRange, sightRange, attackRange, timeSec;
    private bool playerInSightRange, playerInAttackRange, enemyWalkPointReach;
    public LayerMask whatIsPlayer, whatIsGround;
    private void Awake()
    {
        player = GameObject.Find("Player1").transform;
        agent = GetComponent<NavMeshAgent>();
    }


    // Start is called before the first frame update
    void Start()
    {
        //Ensimmäinen vihollisen liikkumispiste
        Patroling();
    }

    // Update is called once per frame
    void Update()
    {
        DistanceCheck();
    }
    private void Patroling()
    {
        //Haetaan random kohde
        var target = SetNewWalkPoint();
        //Lähetetään vihollinen kohteeseen
        agent.SetDestination(target);
    }

    private void DistanceCheck()
    {
        //Jos vihollinen on saapunut kohteeseen, odotetaan 3 sekuntia ja kutsutaan Patroling()-metodia.
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            timeSec += Time.deltaTime;
            if (timeSec > 3f)
            {
                Patroling();
                timeSec = 0;
                return;
            }

        }
        else
        {
            return;
        }

    }
    private Vector3 SetNewWalkPoint()
    {
        //Randomisoidaan Z ja X-arvot
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        //Luodaan uusi kohde
        walkPoint = new Vector3(transform.position.x + randomX, gameObject.transform.position.y, transform.position.z + randomZ);

        //Palautetaan uusi kohde
        return walkPoint;
    }
}

