using System.Collections.Generic;
using UnityEngine;

public class SparkTurret : MonoBehaviour
{
    public float range;
    public float fireRate;
    private float attacktimer;
    public Transform firePoint;
    public int damage;

    public GameObject enemy;
    public List<GameObject> enemies = new List<GameObject>();

    public Transform objectToRotate;
    public GameObject FireAttack;

    void Start()
    {
        GetComponent<SphereCollider>().radius = range;
        //objectToRotate = transform.GetChild(1);
        attacktimer = fireRate;
    }

    void Update()
    {
        if (enemy == null)
        {
            FireAttack.gameObject.SetActive(false);
        }
        if (enemy != null)
        {
            Attack();
            RotateTowardsEnemy();
        }
    }

    private void RotateTowardsEnemy()
    {
        Vector3 enemydir = new Vector3(enemy.transform.position.x, objectToRotate.position.y, enemy.transform.position.z);
        objectToRotate.LookAt(enemydir);
    }

    private void Attack()
    {
        if (attacktimer >= fireRate && enemies.Count > 0)
        {
            attacktimer = 0;
            foreach (var e in enemies)
            {
                e.GetComponent<DumbEnemy>().hp -= damage;
                if(e.GetComponent<DumbEnemy>().hp <= 0 && enemies.Contains(e))
                {
                    enemies.Remove(e);
                    if(enemies.Count > 0)
                    {
                        enemy = enemies[0];
                        FireAttack.gameObject.SetActive(true);
                    }
                    else
                    {
                        enemy = null;
                    }
                }
            }
        }
        attacktimer += Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if the collider of the other GameObject involved in the collision is tagged "Enemy"
        if (other.tag == "Enemy")
        {
            enemies.Add(other.gameObject);
            enemy = enemies[0];
            FireAttack.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Check if the collider of the other GameObject involved in the collision is tagged "Enemy"
        if (other.tag == "Enemy")
        {
            enemies.Remove(other.gameObject);
        }
    }
}
