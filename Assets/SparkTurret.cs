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
        RemoveNullEnemies();
        if (enemy == null)
        {   
            FireAttack.gameObject.SetActive(false);
            if(enemies.Count > 0)
            {
                enemy = enemies[0];
                FireAttack.gameObject.SetActive(true);
            }
        }
        if (enemy != null)
        {
            Attack();
            RotateTowardsEnemy();
        }
    }

    private void RotateTowardsEnemy()
    {
        Vector3 enemydir = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
        objectToRotate.LookAt(enemydir);
    }

    void RemoveNullEnemies()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }
    }

    private void Attack()
    {

        if (attacktimer >= fireRate && enemies.Count > 0)
        {
            attacktimer = 0;
            for (int i = enemies.Count - 1; i >= 0; i--)
            {   
                GameObject e = enemies[i];
                e.GetComponent<DumbEnemy>().hp -= damage;
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
