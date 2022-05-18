using UnityEngine;
using UnityEngine.AI;

public class DogScript : MonoBehaviour
{
    [SerializeField] private Transform[] dogSpawnPoints = new Transform[0];
    [SerializeField] GameObject dogPanel;
    [SerializeField] AudioSource dogAudioSource;

    public Transform player;
    float distanceToPlayer;
    bool followingPlayer, panelIsActivated;
    
    NavMeshAgent agent;
    
    private void Awake()
    {
        


    }

    private void Start()
    {
        int spawnPoint = Random.Range(0, dogSpawnPoints.Length);

        Debug.Log("SpawnPoint: " + spawnPoint);

        transform.position = dogSpawnPoints[spawnPoint].gameObject.transform.position;
        gameObject.AddComponent<NavMeshAgent>(); 
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 1.5f;
        agent.radius = 0.3f;
        agent.height = 0.5f;
        agent.baseOffset = 0.03f;

    }


    private void Update()
    {
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if(distanceToPlayer < 2) 
            {
                followingPlayer = true;
                if (dogPanel.activeInHierarchy == false && !panelIsActivated) 
                {
                    dogPanel.SetActive(true);
                    panelIsActivated = true;
                }
                
            }
            if(followingPlayer)
            {
                agent.destination = player.position;
                StatePatternEnemy.withDog = true;
                dogAudioSource.volume = 0.1f;
                dogAudioSource.maxDistance = 4;
            }
            else
            {
                dogAudioSource.volume = 1;
                dogAudioSource.maxDistance = 4;
            }
            
        }
    }


}
