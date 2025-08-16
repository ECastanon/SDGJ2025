using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float speed;
    public int damage;
    public Transform target;
    private Vector3 vect;
    public ParticleSystem ps;
    private bool hasExploded = false;

    public void InitializeCannon(int turretDamage, Transform turretTarget)
    {
        damage = turretDamage;
        target = turretTarget;
        vect = new Vector3(target.transform.position.x, 0, target.transform.position.z);
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, vect);
        if (distance <= 0.025f && !hasExploded)
        {
            HitGroundThenExplode();
            hasExploded = true;
            speed = 0;
        }
        else
        {
            transform.position = Vector3.Slerp(transform.position, vect, speed * Time.deltaTime);
        }

    }

    private void HitGroundThenExplode()
    {
        ps.Play();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag.Contains("Enemy"))
            {
                hitCollider.gameObject.GetComponent<DumbEnemy>().hp -= damage;

                Rigidbody erb = hitCollider.gameObject.GetComponent<Rigidbody>();
                //erb.AddForce(erb.linearVelocity * -1, ForceMode.Impulse);
                erb.AddForce(transform.up * 50000, ForceMode.Impulse);
            }
        }
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(0.225f);
        Destroy(gameObject);
    }
}
