using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private float timer = 0;

    [SerializeField] private EnemyMovement enemyPrefab;
    private IObjectPool<EnemyMovement> enemyPool;

    void Start()
    {
        enemyPool = new ObjectPool<EnemyMovement>(CreateEnemy, OnGet, OnRelease);
        NavMeshHit closestHit;

        if (NavMesh.SamplePosition(gameObject.transform.position, out closestHit, 500f, NavMesh.AllAreas))
            gameObject.transform.position = closestHit.position;
        else
            Debug.LogError("Could not find position on NavMesh!");
    }

    private void OnGet(EnemyMovement enemy)
    {
        enemy.gameObject.SetActive(true);
        Transform randomSpawnPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];

    }

    private void OnRelease(EnemyMovement enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private EnemyMovement CreateEnemy()
    {
        EnemyMovement enemy = Instantiate(enemyPrefab);
        enemy.SetPool(enemyPool);
        return enemy;
    }

    
    void Update()
    {
        SpawnTime();
    }

    public void SpawnTime()
    {
        timer += Time.deltaTime;
        if (timer > 4)
        {
            enemyPool.Get();
            timer = 0f;
        }
    }
}
