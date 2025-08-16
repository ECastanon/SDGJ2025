using UnityEngine;

public class DumbEnemy : MonoBehaviour
{
    public int maxHP;
    public int hp;

    public float x;

    private void Start()
    {
        hp = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(x, 0, 0);
        if(transform.position.x > 15)
        {
            x = -x;
        }
        if (transform.position.x < -15)
        {
            x = -x;
        }

        if(hp <= 0)
        {
            Destroy(gameObject);
        }

    }
}
