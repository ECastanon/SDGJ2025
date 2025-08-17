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

    public Vector2 scales;

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

                //Set Enemy Scale
                float newScale = Random.Range(scales.x, scales.y);
                newEnemy.transform.localScale = new Vector3(newScale, newScale, newScale);

                //Scale HP based on size
                newEnemy.GetComponent<DumbEnemy>().maxHP += (int)newScale * 3;
                newEnemy.GetComponent<DumbEnemy>().EnableEnemy();
            }
            if (currWave < waveSizes.Count - 1){
                currWave++;
                scales += new Vector2(1, 5);
            }
            timeSinceLastSpawn = 0;
         }
    }

    private void OnDrawGizmos()
    {
        // Draw wire sphere outline
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}