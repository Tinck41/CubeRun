using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _tile;
    [SerializeField] private GameObject _obstacle;

    private GameObject[,] _grid;

    private Vector2Int  _size;

    private List<Coord> _allTileCoords;
    private Queue<Coord> _shuffledTileCoords;

    private Coord _playerSpawnPosition;

    void Update()
    {
        
    }

    public void GenerateGrid(Vector2Int size, int seed, float obstaclePercent)
    {
        _size = size;
        _allTileCoords = new List<Coord>();
        _playerSpawnPosition = new Coord((int)(size.x / 2), 0);

        var holderName = "GridHolder";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        var gridHolder = new GameObject(holderName);
        gridHolder.transform.SetParent(transform);

        if (_tile)
        {
            _grid = new GameObject[_size.x, _size.y];

            // First layer
            for (int y = 0; y < size.y; y++)
            {
                var tileWidth = transform.rotation.y > 0 ? _tile.transform.localScale.x * Mathf.Sqrt(2) : 1;
                var offSet = transform.rotation.y > 0 ? (y % 2 == 0 ? 0 : tileWidth / 2) : 0;
                for (int x = 0; x < size.x - Mathf.Min(1, y % 2); x++)
                {
                    if (y > 0 && y < _size.y - 1 && x > 0 && x < _size.x - 1)
                    {
                        if (Random.Range(0f, 1f) < 0.9f)
                        {
                            _grid[x, y] = Instantiate(_tile, new Vector3(x * tileWidth + offSet, 0, y * (tileWidth / 2)), transform.rotation);
                            _grid[x, y].transform.SetParent(gridHolder.transform);
                        }
                    }
                    else
                    {
                        _grid[x, y] = Instantiate(_tile, new Vector3(x * tileWidth + offSet, 0, y * (tileWidth / 2)), transform.rotation);
                        _grid[x, y].transform.SetParent(gridHolder.transform);
                    }
                    _allTileCoords.Add(new Coord(x, y));
                }
            }

            _shuffledTileCoords = new Queue<Coord>(Utility.ShuffleArray(_allTileCoords.ToArray(), seed));

            bool[,] obstacleMap = new bool[_size.x, _size.y];

            var obstacleCount = (int)(_size.x * _size.y * obstaclePercent);
            var currentObstacleCount = 0;
            for (int i = 0; i < obstacleCount; i++)
            {
                var randomCoord = GetRandomCoord();
                obstacleMap[randomCoord.x, randomCoord.y] = true;
                currentObstacleCount++;

                if (randomCoord.y > 2 && MapIsFullyAccessible(obstacleMap, currentObstacleCount)) {
                    var obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
                    var obstacle = Instantiate(_obstacle, obstaclePosition + Vector3.up * 1, transform.rotation);

                    obstacle.transform.SetParent(gridHolder.transform);
                }
                else
                {
                    obstacleMap[randomCoord.x, randomCoord.y] = false;
                    currentObstacleCount--;
                }
            }
        }
        else
        {
            Debug.LogError("No tile prefab was set");
            throw new UnassignedReferenceException();
        }
    }

    bool MapIsFullyAccessible(bool[,] obstacleMap, int currentObstacleCount)
    {
        bool[,] mapFlags = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(_playerSpawnPosition);
        mapFlags[_playerSpawnPosition.x, _playerSpawnPosition.y] = true;

        var accessibleTielCount = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    var neighbourX = tile.x + x;
                    var neighbourY = tile.y + y;

                    if (x == 0 || y == 0)
                    {
                        if (neighbourX >= 0 && neighbourX < obstacleMap.GetLength(0) &&
                            neighbourY >= 0 && neighbourY < obstacleMap.GetLength(1))
                        {
                            if (!mapFlags[neighbourX, neighbourY] && !obstacleMap[neighbourX, neighbourY])
                            {
                                mapFlags[neighbourX, neighbourY] = true;
                                queue.Enqueue(new Coord(neighbourX, neighbourY));
                                accessibleTielCount++;
                            }
                        }
                    }
                }
            }
        }

        var targetAccessibleTileCount = _size.x * _size.y - currentObstacleCount;

        return targetAccessibleTileCount == accessibleTielCount;
    }

    public Vector3 CoordToPosition(int x, int y)
    {
        var tileWidth = transform.rotation.y > 0 ? _tile.transform.localScale.x * Mathf.Sqrt(2) : 1;
        var offSet = transform.rotation.y > 0 ? (y % 2 == 0 ? 0 : tileWidth / 2) : 0;

        return new Vector3(x * tileWidth + offSet, 0, y * (tileWidth / 2));
    }

    public Coord GetRandomCoord()
    {
        Coord randomCoord = _shuffledTileCoords.Dequeue();
        _shuffledTileCoords.Enqueue(randomCoord);

        return randomCoord; 
    }

    public void AlignGrid()
    {
        transform.position = new Vector3(-_size.x / 2 * Mathf.Sqrt(2), transform.position.y, transform.position.z);
    }

    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
}
