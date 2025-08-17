using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem; 

public class SimpleMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    public Animator anim;

    public LayerMask btLayer;
    private bool isHittingBuildTile;
    private BuildTile bt;

    public List<GameObject> Towers = new List<GameObject>();

    public Transform endPoint;

    public CartMagnet cm;
    public List<Recipe> Recipes = new List<Recipe>();

    //0 = Gatling, 1 = Cannon, 2 = Fire, 3 = Lightning
    public int CurrrentTowerIndex = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        PerformRaycast();
        BuildTower();
        Move();
    }
    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        if(movement == Vector3.zero)
        {
            anim.SetBool("Walking", false);
        }
        else
        {
            anim.SetBool("Walking", true);
        }

        movement = transform.TransformDirection(movement) * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    void BuildTower()
    {
        if (Input.GetKeyDown("1"))
        {
            CurrrentTowerIndex = 0;
            Debug.Log("GATLING SELECTED");
        }
        if (Input.GetKeyDown("2"))
        {
            CurrrentTowerIndex = 1;
            Debug.Log("CANNON SELECTED");
        }
        if (Input.GetKeyDown("3"))
        {
            CurrrentTowerIndex = 2;
            Debug.Log("FIRE SELECTED");
        }
        if (Input.GetKeyDown("4"))
        {
            CurrrentTowerIndex = 3;
            Debug.Log("LIGHTNING SELECTED");
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (IfCanAfford())
            {
                bt.SetTowerOnTile(Towers[CurrrentTowerIndex]);
            }
        }
    }

    private bool IfCanAfford()
    {
        if (cm.oreCounts[0] >= Recipes[CurrrentTowerIndex].coal &&
            cm.oreCounts[1] >= Recipes[CurrrentTowerIndex].steel &&
            cm.oreCounts[2] >= Recipes[CurrrentTowerIndex].diamond &&
            cm.oreCounts[3] >= Recipes[CurrrentTowerIndex].gold)
        {
            //SubtractCost
            cm.oreCounts[0] -= Recipes[CurrrentTowerIndex].coal;
            cm.oreCounts[1] -= Recipes[CurrrentTowerIndex].steel;
            cm.oreCounts[2] -= Recipes[CurrrentTowerIndex].diamond;
            cm.oreCounts[3] -= Recipes[CurrrentTowerIndex].gold;

            return true;
        }
        else
        {
            Debug.Log("YOU'RE TOO POOR");
        }
        return false;
    }

    public void PerformRaycast()
    {
        RaycastHit hit;
        Vector3 direction = endPoint.position - transform.position;
        if (Physics.Raycast(transform.position, direction.normalized, out hit, direction.magnitude, btLayer))
        {
            //Debug.Log("Hit: " + hit.collider.name);
            isHittingBuildTile = true;
            bt = hit.collider.GetComponent<BuildTile>();
        }
        else
        {
            isHittingBuildTile = false;
            bt = null;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = isHittingBuildTile ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, endPoint.position);
    }


}

[System.Serializable]
public class Recipe
{
    public int coal;
    public int steel;
    public int diamond;
    public int gold;
}