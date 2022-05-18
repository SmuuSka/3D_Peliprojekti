using UnityEngine;
using UnityEngine.AI;

public class DogScript : MonoBehaviour
{
    public Transform player;
    float distanceToPlayer;
    bool followingPlayer, panelIsActivated;
    [SerializeField] GameObject dogPanel;
    NavMeshAgent agent;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
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


                //   agent.SetDestination(new Vector3(player.position.x-1 ,player.position.y, player.position.z-1));
            }
            
        }
    }


}
