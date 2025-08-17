using UnityEngine;

public class DumbEnemy : MonoBehaviour
{
    public int maxHP;
    public int hp;

    public void EnableEnemy()
    {
        hp = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
