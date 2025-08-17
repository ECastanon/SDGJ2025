using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LightningTower : MonoBehaviour
{
    public float range;
    public float lightingChainRange;

    public float fireRate;
    private float attacktimer;
    public Transform firePoint;

    public int damage;

    public GameObject enemy;
    public GameObject enemy2;
    public GameObject enemy3;

    [Header("Use Laser")]
    public LineRenderer lineRenderer;
    public LineRenderer lineRenderer2;
    public LineRenderer lineRenderer3;
    //public ParticleSystem impactEffect;
    //public Light impactLight;

    void Start()
    {
        GetComponent<SphereCollider>().radius = range;
        attacktimer = fireRate;
    }

    void Update()
    {
            Attack();
            FindEnemies();
            Laser();
    }

    private void Attack()
    {
        if (attacktimer >= fireRate)
        {
            attacktimer = 0;
            //Damage enemies in chain then make a new chain
            if(enemy) enemy.GetComponent<DumbEnemy>().hp -= damage;
            if(enemy2) enemy2.GetComponent<DumbEnemy>().hp -= damage;
            if(enemy3) enemy3.GetComponent<DumbEnemy>().hp -= damage;
            FindEnemies();
        }
        attacktimer += Time.deltaTime;
    }

    private void FindEnemies()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(allEnemies.Length > 0)
        {
            enemy = allEnemies[0];

            foreach (var e in allEnemies)
            {
                float distance = Vector3.Distance(transform.position, e.transform.position);
                float distance2 = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < distance2)
                {
                    enemy = e;
                }
            }

            if (enemy)
            {
                Collider[] hitColliders = Physics.OverlapSphere(enemy.transform.position, lightingChainRange);
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.gameObject.tag.Contains("Enemy") && hitCollider.gameObject != enemy)
                    {
                        float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                        float distance2 = Vector3.Distance(transform.position, enemy.transform.position);
                        if (distance > distance2)
                        {
                            enemy2 = hitCollider.gameObject;
                        }
                    }
                }
            }

            if (enemy2)
            {
                Collider[] hitColliderss = Physics.OverlapSphere(enemy2.transform.position, lightingChainRange);
                foreach (var hitCollider in hitColliderss)
                {
                    if (hitCollider.gameObject.tag.Contains("Enemy") && hitCollider.gameObject != enemy2)
                    {
                        float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                        float distance2 = Vector3.Distance(transform.position, enemy.transform.position);
                        if (distance > distance2)
                        {
                            enemy3 = hitCollider.gameObject;
                        }
                    }
                }
            }
        }
        else
        {
            enemy = null;
            enemy2 = null;
            enemy3 = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if the collider of the other GameObject involved in the collision is tagged "Enemy"
        if (other.tag == "Enemy")
        {
            enemy = other.gameObject;
        }
    }

    void Laser()
    {
        if (enemy)
        {
            lineRenderer.SetPosition(1, firePoint.position + Vector3.up * 3.75f);
            lineRenderer.SetPosition(0, enemy.transform.position);
        }
        else
        {
            lineRenderer.SetPosition(1, firePoint.position + Vector3.up * 3.75f);
            lineRenderer.SetPosition(0, firePoint.position + Vector3.up * 3.75f);
        }


        if (enemy2 && enemy)
        {
            lineRenderer2.SetPosition(1, enemy.transform.position);
            lineRenderer2.SetPosition(0, enemy2.transform.position);
        }
        else
        {
            lineRenderer2.SetPosition(1, firePoint.position + Vector3.up * 3.75f);
            lineRenderer2.SetPosition(0, firePoint.position + Vector3.up * 3.75f);
        }

        if (enemy3 && enemy2)
        {
            lineRenderer3.SetPosition(1, enemy2.transform.position);
            lineRenderer3.SetPosition(0, enemy3.transform.position);
        }
        else
        {
            lineRenderer3.SetPosition(1, firePoint.position + Vector3.up * 3.75f);
            lineRenderer3.SetPosition(0, firePoint.position + Vector3.up * 3.75f);
        }

        //Laser Effects
        //Vector3 dir = firePoint.position - enemy.transform.position;
        //impactEffect.transform.position = enemy.transform.position + dir.normalized * .5f;
        //impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }
}
