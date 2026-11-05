using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private IObjectPool<EnemyMovement> enemyPool;

    public void SetPool(IObjectPool<EnemyMovement> pool)
    {
        enemyPool = pool;
    }

    [System.Obsolete]
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = Object.FindObjectOfType<PlayerCamera>().transform;
    }

    void Update()
    {
        GameObject go = new GameObject("Target");
        Vector3 sourcePostion = new Vector3(0, 0, 0);//The position you want to place your agent
        NavMeshHit closestHit;
        if (NavMesh.SamplePosition(sourcePostion, out closestHit, 500, 1))
        {
            go.transform.position = closestHit.position;
            go.AddComponent<NavMeshAgent>();
        }
        else
        {
            Debug.Log("...");
        }
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
