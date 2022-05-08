using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _tile;

    private GameObject[,] _grid;

    private Vector2Int  _size;

    void Update()
    {
        
    }

    public void GenerateGrid(Vector2Int size)
    {
        _size = size;

        if (_tile)
        {
            _grid = new GameObject[_size.x, _size.y];

            for (int y = 0; y < size.y; y++)
            {
                var tileWidth = _tile.transform.localScale.x * Mathf.Sqrt(2);
                var offSet = (y % 2 == 0 ? 0 : tileWidth / 2);
                for (int x = 0; x < size.x - Mathf.Min(1, y % 2); x++)
                {
                    if (y > 0 && y < _size.y - 1 && x > 0 && x <  _size.x - 1)
                    {
                        if (Random.Range(0f, 1f) < 0.98f)
                        {
                            _grid[x, y] = Instantiate(_tile, new Vector3(x * tileWidth + offSet, 0, y * (tileWidth / 2)), transform.rotation);
                            _grid[x, y].transform.parent = transform;
                        }
                    }
                    else
                    {
                        _grid[x, y] = Instantiate(_tile, new Vector3(x * tileWidth + offSet, 0, y * (tileWidth / 2)), transform.rotation);
                        _grid[x, y].transform.parent = transform;
                    }
                }
            }
        }
        else
        {
            Debug.LogError("No tile prefab was set");
            throw new UnassignedReferenceException();
        }
    }

    public void AlignGrid()
    {
        transform.position = new Vector3(-_size.x / 2 * Mathf.Sqrt(2), transform.position.y, transform.position.z);
    }
}
