using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGridScript : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private int seed;

    private GameObject[,] grid;

    void Awake()
    {
        grid = new GameObject[gridSize.x, gridSize.y];

        Random.InitState(seed);
        
        generateGrid();
    }

    void Update()
    {
        
    }

    void generateGrid()
    {
        if (tile)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int x = 0; x < gridSize.x - Mathf.Min(1, y % 2); x++)
                {
                    grid[x, y] = Instantiate(tile, new Vector3(x * Mathf.Sqrt(2) + (y % 2 == 0 ? 0 : Mathf.Sqrt(2) / 2), 0, y * (Mathf.Sqrt(2) / 2)), transform.rotation);
                    grid[x, y].transform.parent = transform;
                    grid[x, y].GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                }
            }
        }
        else
        {
            Debug.LogError("No tile prefab was set");
        }
    }

    public void AlignGrid()
    {
        transform.position = new Vector3(-gridSize.x / 2 * Mathf.Sqrt(2), transform.position.y, transform.position.z);
    }
}
