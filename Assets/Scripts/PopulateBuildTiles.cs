using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PopulateBuildTiles : MonoBehaviour
{
    public GameObject buildTile;
    private int rows = 11;
    private int columns = 11;
    private float tileSpacing = 5;

    private List<GameObject> tiles = new List<GameObject>();

    void Start()
    {
        PopulateTiles();
    }
    private void PopulateTiles()
    {
        // Find Centermost point for the grid
        float xOffset = ((rows - 1) * tileSpacing) / 2f;
        float zOffset = ((columns - 1) * tileSpacing) / 2f;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                //Place Tiles to be centered around the origin
                Vector3 position = new Vector3(i * tileSpacing - xOffset, 0.1f, j * tileSpacing - zOffset);
                GameObject bt = Instantiate(buildTile, position, Quaternion.identity);
                bt.transform.SetParent(transform);
                tiles.Add(bt);
            }
        }

        int middle = ((rows * columns) - 1) / 2;
        GameObject centerTile = tiles[middle];
        Destroy(centerTile);
        tiles.Remove(centerTile);
    }
}