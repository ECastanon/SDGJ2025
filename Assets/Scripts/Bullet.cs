using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public GameObject target;

    private float lifeTime = 0.75f;
    private float timer;

    public void InitializeBullet(int turretDamage, GameObject turretTarget)
    {
        damage = turretDamage;
        target = turretTarget;
    }

    private void Update()
    {
        if(timer > lifeTime)
        {
            Destroy(gameObject);
        }
        timer += Time.deltaTime;

        transform.position = Vector3.Slerp(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if the collider of the other GameObject involved in the collision is tagged "Enemy"
        if (other.gameObject == target)
        {
            target.GetComponent<DumbEnemy>().hp -= damage;
            Destroy(gameObject);
        }
    }
}