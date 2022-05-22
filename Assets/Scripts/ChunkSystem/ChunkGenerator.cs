using System;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _tile;
    [SerializeField] private GameObject _coin;
    [SerializeField] private GameObject[] _obstacles;

    private GameObject[,] _grid;

    private Vector2Int  _size;

    private List<Coord> _allTileCoords;
    private List<Coord> _pathTileCoords;
    private Queue<Coord> _shuffledTileCoords;

    private Coord _playerSpawnPosition;

    void Update()
    {
        
    }

    public void GenerateGrid(Vector2Int size, int pathNum, float obstaclePercent)
    {
        _size = size;
        _allTileCoords = new List<Coord>();
        _pathTileCoords = new List<Coord>();
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

            for (int y = 0; y < _size.y; y++)
            {
                var tileWidth = transform.rotation.y > 0 ? _tile.transform.localScale.x * Mathf.Sqrt(2) : 1;
                var offSet = transform.rotation.y > 0 ? (y % 2 == 0 ? 0 : tileWidth / 2) : 0;
                for (int x = 0; x < _size.x - Mathf.Min(1, y % 2); x++)
                {
                    _grid[x, y] = Instantiate(_tile, new Vector3(x * tileWidth + offSet, 0, y * (tileWidth / 2)), transform.rotation);
                    _grid[x, y].transform.SetParent(gridHolder.transform);
                    _allTileCoords.Add(new Coord(x, y));
                }
            }

            for (int i = 0; i < pathNum; i++)
            {
                CreatePath();
            }

            CreateCoins();

            _shuffledTileCoords = new Queue<Coord>(Utility.ShuffleArray(_allTileCoords.ToArray()));

            var obstacleCount = (int)(_shuffledTileCoords.Count * obstaclePercent);
            int counter = 0;
            for (int i = 0; i < obstacleCount; i++)
            {
                var randomCoord = GetRandomCoord();
                if (_pathTileCoords.Contains(randomCoord))
                {
                    counter++;
                    continue;
                }

                if (randomCoord.y > 2)
                {
                    var obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
                    var random = new System.Random(Guid.NewGuid().GetHashCode());

                    if (random.NextDouble() > 0.5f)
                    {
                        var obstacle = Instantiate(_obstacles[random.Next(0, _obstacles.Length)], obstaclePosition + Vector3.up * 1, transform.rotation);
                        obstacle.transform.SetParent(gridHolder.transform);
                    }
                    else
                    {
                        if ((randomCoord.x != 0 && randomCoord.x != _size.x - 1) && randomCoord.y != 0)
                        {
                            DestroyImmediate(_grid[randomCoord.x, randomCoord.y]);
                            _grid[randomCoord.x, randomCoord.y] = null;
                        }
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

    private void CreatePath()
    {
        var currentPostion = _playerSpawnPosition;
        for (int y = currentPostion.y; y < _size.y; y++)
        {
            _pathTileCoords.Add(currentPostion);

            var tileWidth = _tile.transform.localScale.x * Mathf.Sqrt(2);
            var offset = y % 2 == 0 ? tileWidth / 2 : 0;
            var shift = y % 2 == 0 ? 0 : 1;
            Coord nexPosition;
            if (shift == 0 && (currentPostion.x == 0 || currentPostion.x == _size.x - 1))
            {
                nexPosition = new Coord(currentPostion.x == 0 ? 0 : _size.x - 2 , currentPostion.y + 1);
            }
            else
            {
                var random = new System.Random(Guid.NewGuid().GetHashCode());
                var direction = 0;
                if (random.NextDouble() > 0.5f)
                {
                    direction = 1;
                }
                nexPosition = new Coord(currentPostion.x - direction + shift, currentPostion.y + 1);
            }

            currentPostion = nexPosition;

            //var instance = Instantiate(_obstacles[0], new Vector3(currentPostion.x * tileWidth + offset, 1, currentPostion.y * (tileWidth / 2)), transform.rotation);
            //instance.GetComponent<Renderer>().material.color = new Color(255f, 255f, 0f, 1f);
            //instance.transform.SetParent(transform.Find("GridHolder").transform);
        }
    }

    private void CreateCoins()
    {
        var random = new System.Random(Guid.NewGuid().GetHashCode());

        foreach (var coord in _pathTileCoords)
        {
            if (random.NextDouble() < 0.01f && coord.y > 5)
            {
                var position = CoordToPosition(coord.x, coord.y);
                var coin = Instantiate(_coin, position + Vector3.up * 1, transform.rotation);
                coin.transform.SetParent(transform.Find("GridHolder").transform);
            }
        }
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
