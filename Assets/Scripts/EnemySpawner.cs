using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject target;
    public float timeSinceLastSpawn;
    public float spawnTimer;

    void Start()
    {
        timeSinceLastSpawn = 0;
    }

    void Update()
    {
         timeSinceLastSpawn += Time.deltaTime;
         if (timeSinceLastSpawn >= spawnTimer)
         {
             GameObject newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
             newEnemy.GetComponent<MoveTo>().goal = target.transform;
             timeSinceLastSpawn = 0;
         }
    }

}