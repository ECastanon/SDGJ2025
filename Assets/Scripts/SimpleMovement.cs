using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem; 

public class SimpleMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;

    public LayerMask btLayer;
    private bool isHittingBuildTile;
    private BuildTile bt;

    //Can Delete Later
    public MeshRenderer mr;

    public List<GameObject> Towers = new List<GameObject>();

    public Transform endPoint;
    InputAction interactAction;
    InputAction nextTower;
    InputAction prevTower;
    public int CurrrentTowerIndex = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        interactAction = InputSystem.actions.FindAction("Interact");
        nextTower = InputSystem.actions.FindAction("Previous");
        prevTower = InputSystem.actions.FindAction("Next");
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
        movement = transform.TransformDirection(movement) * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    void BuildTower()
    {
        if (nextTower.WasPressedThisFrame())
        {
            CurrrentTowerIndex++;
            if (CurrrentTowerIndex >= Towers.Count)
            {
                CurrrentTowerIndex = 0;
            }
        }
        if (prevTower.WasPressedThisFrame())
        {
            CurrrentTowerIndex--;
            if (CurrrentTowerIndex < 0)
            {
                CurrrentTowerIndex = Towers.Count - 1;
            }
        }
        if (isHittingBuildTile && interactAction.WasPressedThisFrame())
        {
            if (bt != null)
            {
                bt.SetTowerOnTile(Towers[CurrrentTowerIndex]);
            }
        }
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
            mr.material.color = Color.green;
        }
        else
        {
            isHittingBuildTile = false;
            bt = null;
            mr.material.color = Color.red;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = isHittingBuildTile ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, endPoint.position);
    }
}