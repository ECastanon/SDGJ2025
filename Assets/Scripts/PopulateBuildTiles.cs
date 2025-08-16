using UnityEngine;

public class PopulateBuildTiles : MonoBehaviour
{
    public GameObject buildTile;
    public int rows;
    public int columns;
    private float tileSpacing = 5;
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
            }
        }
    }
}
