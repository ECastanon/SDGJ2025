using UnityEngine;

public class GatlingTower : MonoBehaviour
{
    public GameObject bullet;
    public float range;
    public float fireRate;
    private float attacktimer;
    public Transform firePoint;
    public int damage;

    public GameObject enemy;

    public Transform objectToRotate;

    void Start()
    {
        GetComponent<SphereCollider>().radius = range;
        //objectToRotate = transform.GetChild(1);
        attacktimer = fireRate;
    }

    void Update()
    {
        if (enemy != null)
        {
            Attack();
            RotateTowardsEnemy();
        }
    }

    private void Attack()
    {
        if(attacktimer >= fireRate)
        {
            attacktimer = 0;
            GameObject proj = Instantiate(bullet, firePoint.position, Quaternion.identity);
            proj.GetComponent<Bullet>().InitializeBullet(damage, enemy);
        }
        attacktimer += Time.deltaTime;
    }

    private void RotateTowardsEnemy()
    {
        Vector3 enemydir = new Vector3(enemy.transform.position.x, objectToRotate.position.y, enemy.transform.position.z);
        objectToRotate.LookAt(enemydir);
    }

    private void OnTriggerStay(Collider other)
    {
        float distance = Vector3.Distance(transform.position, other.transform.position);
        float distance_to_enemy = Vector3.Distance(transform.position, enemy.transform.position);
        //Check if the collider of the other GameObject involved in the collision is tagged "Enemy"
        if (other.tag == "Enemy" && (distance < distance_to_enemy)) // target closest
        {
            enemy = other.gameObject;
        }
    }
}