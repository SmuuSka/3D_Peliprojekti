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
        agent = GetComponent<NavMeshAgent>();


    }

    private void Start()
    {
        int spawnPoint = Random.Range(0, dogSpawnPoints.Length);

        Debug.Log("SpawnPoint: " + spawnPoint);

        agent.transform.position = dogSpawnPoints[spawnPoint].gameObject.transform.position;
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
                dogAudioSource.maxDistance = 6;
            }
            
        }
    }


}
