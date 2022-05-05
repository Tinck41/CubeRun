using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGridScript : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private int seed;

    private GameObject[,] grid;

    void Start()
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
                for (int x = 0; x < gridSize.x; x++)
                {
                    grid[x, y] = Instantiate(tile, new Vector3(x, 0, y), Quaternion.identity);
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
}
