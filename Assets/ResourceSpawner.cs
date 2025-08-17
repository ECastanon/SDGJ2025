using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [Header("Ore Type 1")]
    public GameObject ore;
    public int oreToSpawn;
    public float oreinnerRadius;
    public float oreouterRadius;

    [Header("Ore Type 2")]
    public GameObject ore2;
    public int ore2ToSpawn;
    public float ore2innerRadius;
    public float ore2outerRadius;

    [Header("Ore Type 3")]
    public GameObject ore3;
    public int ore3ToSpawn;
    public float ore3innerRadius;
    public float ore3outerRadius;

    private void Start()
    {
        SpawnResource(ore, oreToSpawn, oreinnerRadius, oreouterRadius);
        SpawnResource(ore2, ore2ToSpawn, ore2innerRadius, ore2outerRadius);
        SpawnResource(ore3, ore3ToSpawn, ore3innerRadius, ore3outerRadius);
    }

    private void SpawnResource(GameObject resource, int number, float inner, float outer)
    {
        for (int i = 0; i < number; i++)
        {
            float angle = Random.Range(0f, Mathf.PI * 2);
            float radius = Random.Range(inner, outer);
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            Vector3 spawnPosition = new Vector3(x, 0.5f, z);
            Instantiate(resource, spawnPosition, Quaternion.identity);
        }
    }
}
