using UnityEngine;
using System.Collections.Generic;

public class CartMagnet : MonoBehaviour
{   
    public float gravityStrength = 10f;
    public List<GameObject> ores = new List<GameObject>();
    public List<int> oreCounts = new List<int>();
    
    private void OnTriggerEnter (Collider col)
    {
        if (col.CompareTag("Ore"))
        {
            Debug.Log("Added ore to ore list");
            if (!ores.Contains(col.gameObject))
            {
                Debug.Log("Ore already exists in the list, skipping addition.");
                ores.Add(col.gameObject);

            }
        }
        if (col.CompareTag("BuildZone"))
        {
            Debug.Log("Emptying cart");
            for (int index = 0; index < ores.Count; index++)
            {
                GameObject ore = ores[index];
                int oreId = ore.GetComponent<OreProperties>().oreID;
                oreCounts[oreId]++;
                Destroy(ore);
            }
            ores = new List<GameObject>();
        }
    }

    private void OnTriggerStay(Collider col)
    { 
        if (col.CompareTag("Ore"))
        {            
            Rigidbody erb = col.gameObject.GetComponent<Rigidbody>();
            Vector3 directionToCart = (transform.position - erb.transform.position).normalized;
            Vector3 force = directionToCart * gravityStrength * Time.deltaTime;
            erb.AddForce(force, ForceMode.Force);
        }
    }
}