using UnityEngine;

public class BaseData : MonoBehaviour
{
    public int baseMaxHP;
    public int HP;

    private float timer;

    private int EnemiesCount()
    {
        int enemiesInRange = 0;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag.Contains("Enemy"))
            {
                enemiesInRange += 1;
            }
        }
        return enemiesInRange;
    }

    private void Update()
    {
        if(timer > 3)
        {
            HP -= EnemiesCount();
            timer = 0;

            if(HP <= 0)
            {
                Debug.Log("YOU LOSE");
            }
        }
        timer += Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        // Draw wire sphere outline
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3);
    }
}
