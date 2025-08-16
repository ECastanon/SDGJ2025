using System.Net;
using UnityEngine;

public class BuildTile : MonoBehaviour
{
    public GameObject towerOnTile;

    public void SetTowerOnTile(GameObject towerToPlace)
    {
        //Can Add Other Rescoure Checks Here
        if (towerOnTile == null)
        {
            GameObject tower = Instantiate(towerToPlace, transform.position, Quaternion.identity);
            towerOnTile = tower;
        }
        else    
        {
            Debug.LogWarning("Cannot Add Another Tower!");
        }
    }
}
