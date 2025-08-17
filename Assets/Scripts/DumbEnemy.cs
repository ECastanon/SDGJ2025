using UnityEngine;

public class DumbEnemy : MonoBehaviour
{
    public int maxHP;
    public int hp;

    private void Start()
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
