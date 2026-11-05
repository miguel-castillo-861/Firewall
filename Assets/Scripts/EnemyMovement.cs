using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    [System.Obsolete]
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = Object.FindObjectOfType<PlayerCamera>().transform;
    }

    void Update()
    {
        agent.SetDestination(player.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerCamera player = other.GetComponent<PlayerCamera>();
            Destroy(other.gameObject);

        }
    }
}
