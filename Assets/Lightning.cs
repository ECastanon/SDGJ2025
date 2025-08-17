using UnityEngine;

public class Lightning : MonoBehaviour
{
    public float speed;
    public float rotSpeed;
    public int damage;
    public GameObject target;

    public void InitializeLightning(int turretDamage, GameObject turretTarget)
    {
        damage = turretDamage;
        target = turretTarget;
    }

    private void Update()
    {
        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.position, target.transform.position, rotSpeed * Time.deltaTime, 0.0f);
        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);

        transform.position = Vector3.Slerp(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void FindNextClosest()
    {
        GameObject closest = null;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log(hitCollider.gameObject);

            if (hitCollider.gameObject.tag.Contains("Enemy"))
            {
                float currentClosestEnemy = Vector3.Distance(transform.position, target.transform.position);
                float distanceTo = Vector3.Distance(transform.position, hitCollider.transform.position);

                if (hitCollider.gameObject == target) distanceTo = 10000;

                if (distanceTo > currentClosestEnemy)
                {
                    Debug.Log("Finding NEXT!");
                    target = hitCollider.gameObject;
                    closest = target;
                }
            }
        }
        
        if (closest == null)
        {
            Debug.Log("NOTHING NEARBY");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if the collider of the other GameObject involved in the collision is tagged "Enemy"
        if (other.gameObject == target)
        {
            target.GetComponent<DumbEnemy>().hp -= damage;
            FindNextClosest();
        }
    }
}