using UnityEngine;

public class DetectInteractables : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Ore"))
        {
            Debug.Log("Collided with: " + col.name + "!");
        }
    }
}
