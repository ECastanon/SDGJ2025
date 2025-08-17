using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject target;
    public float spawnRadius;
    public float timeSinceLastSpawn;
    public float spawnTimer;
    public List<int> waveSizes;
    public int currWave;

    void Start()
    {
        currWave = 0;
    }

    void Update()
    {
         timeSinceLastSpawn += Time.deltaTime;
         if (timeSinceLastSpawn >= spawnTimer)
         {
            for (int i = 0; i < waveSizes[currWave]; i++)
            {

                Vector2 SpawnPoint2d = Random.insideUnitCircle.normalized * spawnRadius;
                Vector3 SpawnPoint = new Vector3(SpawnPoint2d.x, transform.position.y, SpawnPoint2d.y);
                GameObject newEnemy = Instantiate(enemy, SpawnPoint, Quaternion.identity);
                newEnemy.GetComponent<MoveTo>().goal = target.transform;
            }
            if (currWave < waveSizes.Count - 1){
                currWave++;
            }
            timeSinceLastSpawn = 0;
         }
    }

}