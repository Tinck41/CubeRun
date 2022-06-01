using System;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    North = 0,
    NorthEast,
    East,
    SouthEast,
    South,
    SouthWest,
    West,
    NorthWest
}

public class ChunkGenerator : MonoBehaviour
{
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

    private enum CoordMark
    {
        NONE = 0,
        PATH,
        OBSTACLE,
        EDGE
    }

    [SerializeField] private GameObject _tile;
    [SerializeField] private GameObject _coin;
    [SerializeField] private GameObject[] _obstacles;

    [SerializeField] private Vector2Int  _size;

    [Range(0f, 1f)]
    [SerializeField] private float _obstaclePercent;

    [SerializeField] private bool _drawPath = false;

    private GameObject[,] _grid;

    private CoordMark[,] _coordMarks;

    private List<Coord> _allTileCoords;
    private List<Coord> _pathTileCoords;
    private List<Coord> _pathStartCoord;
    private List<Coord> _pathEndCoord;

    private Queue<Coord> _shuffledTileCoords;

    private Coord _playerSpawnPosition;

    private GameObject _gridHolder;

    private float _tileWidth;

    public bool firstChunk { get; set; }

    public Vector2Int GetSize()
    {
        return _size;
    }

    public void Intialize()
    {
        _allTileCoords = new List<Coord>();
        _pathTileCoords = new List<Coord>();
        _pathStartCoord = new List<Coord>();
        _pathEndCoord = new List<Coord>();

        _playerSpawnPosition = new Coord(6, 0);

        _tileWidth = _tile.transform.localScale.x * Mathf.Sqrt(2);

        _gridHolder = new GameObject("GridHolder");
        _gridHolder.transform.SetParent(transform);
    }

    public void GenerateChunk()
    {
        GenerateGround();
        SetInitialCoordMark();
        CreatePath();
        GenerateObstacles();
        GenerateCoins();
    }

    private void GenerateGround()
    {
        _grid = new GameObject[_size.y, _size.x + _size.x];

        var tileHalfSize = _tileWidth / 2;
        for (int y = 0; y < _grid.GetLength(0); y++)
        {
            for (int x = 0; x < _grid.GetLength(1) - 1; x++)
            {
                var posOffSet = x % 2 == 0 ? 0 : tileHalfSize;

                _grid[y, x] = Instantiate(_tile, new Vector3(x * tileHalfSize, 0, y * _tileWidth + posOffSet), transform.rotation);
                _grid[y, x].transform.SetParent(_gridHolder.transform);

                _allTileCoords.Add(new Coord(x, y));
            }
        }
    }

    private void SetInitialCoordMark()
    {
        _coordMarks = new CoordMark[_size.y, _size.x + _size.x];

        for (int y = 0; y < _coordMarks.GetLength(0); y++)
        {
            _coordMarks[y, 0] = CoordMark.EDGE;
            _coordMarks[y, _coordMarks.GetLength(1) - 2] = CoordMark.EDGE;
        }

        if (firstChunk)
        {
            for (int x = 0; x < _coordMarks.GetLength(1) - 1; x += 2)
            {
                _coordMarks[0, x] = CoordMark.EDGE;
            }
        }
    }

    private void CreatePath()
    {
        foreach (var start in _pathStartCoord)
        {
            var nextPos = start;
            for (int y = start.y; y < _size.y * 2; y++)
            {
                _pathTileCoords.Add(nextPos);

                _coordMarks[nextPos.y, nextPos.x] = CoordMark.PATH;

                var random = new System.Random(Guid.NewGuid().GetHashCode());
                var direction = 0;
                if (random.NextDouble() > 0.5f)
                {
                    direction = 2;
                }

                if (nextPos.x == 0) direction = 2;
                if (nextPos.x == _size.x * 2 - 2) direction = 0;

                nextPos = GetNeighbour(nextPos, direction);
            }

            nextPos.y = 0;
            _pathEndCoord.Add(nextPos);
        }

        if (_drawPath)
        {
            var tileHalfSize = _tileWidth / 2;
            foreach (var coord in _pathTileCoords)
            {
                var posOffSet = coord.x % 2 == 0 ? 0 : tileHalfSize;

                var tile = Instantiate(_obstacles[2], new Vector3(coord.x * tileHalfSize, 1, coord.y * _tileWidth + posOffSet), transform.rotation);
                tile.transform.SetParent(_gridHolder.transform);
            }
        }
    }

    private void GenerateObstacles()
    {
        _shuffledTileCoords = new Queue<Coord>(Utility.ShuffleArray(_allTileCoords.ToArray()));

        var obstacleCount = (int)(_shuffledTileCoords.Count * _obstaclePercent);
        var counter = 0;
        for (int i = 0; i < obstacleCount; i++)
        {
            if (counter == _shuffledTileCoords.Count)
            {
                break;
            }

            var randomCoord = GetRandomCoord();
            if (_coordMarks[randomCoord.y, randomCoord.x] == CoordMark.EDGE ||
                _coordMarks[randomCoord.y, randomCoord.x] == CoordMark.PATH ||
                _coordMarks[randomCoord.y, randomCoord.x] == CoordMark.OBSTACLE ||
                (firstChunk && randomCoord.y < 2))
            {
                i--;
                counter++;
                continue;
            }

            _coordMarks[randomCoord.y, randomCoord.x] = CoordMark.OBSTACLE;

            var obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
            var random = new System.Random(Guid.NewGuid().GetHashCode());

            if (random.NextDouble() > 0.05f)
            {
                var obstacleId = random.Next(0, _obstacles.Length);
                var occupiedDir = _obstacles[obstacleId].GetComponent<Obstacle>().occupiedDir;
                var spwanChance = _obstacles[obstacleId].GetComponent<SpawnChance>().value;
                if (random.NextDouble() <= spwanChance)
                {
                    var neigboursCoord = new List<Coord>();
                    foreach (int direction in occupiedDir)
                    {
                        Debug.Log(direction);
                        var neigbour = GetNeighbour(randomCoord, direction);
                        if (neigbour.x >= 0 && neigbour.x < _coordMarks.GetLength(1) &&
                        neigbour.y >= 0 && neigbour.y < _coordMarks.GetLength(0))
                        {
                            neigboursCoord.Add(neigbour);
                        }
                    }

                    // Check whether neigbour occupied or not
                    var occupied = false;
                    foreach (var coord in neigboursCoord)
                    {
                        if (_coordMarks[coord.y, coord.x] != CoordMark.NONE)
                        {
                            occupied = true;
                            break;
                        }
                    }

                    // Can't continue in case one of the neighbours is occupied
                    if (occupied)
                    {
                        i--;
                        counter++;
                        continue;
                    }

                    // Mark neigbours
                    foreach (var coord in neigboursCoord)
                    {
                        _coordMarks[coord.y, coord.x] = CoordMark.OBSTACLE;
                    }

                    // Spawning
                    var obstacle = Instantiate(_obstacles[obstacleId], _obstacles[obstacleId].transform.position + obstaclePosition + Vector3.up * 1, transform.rotation);
                    obstacle.transform.SetParent(_gridHolder.transform);
                }
            }
            else
            {
                DestroyImmediate(_grid[randomCoord.y, randomCoord.x]);
                _grid[randomCoord.y, randomCoord.x] = null;
            }
        }
    }

    private Coord GetNeighbour(Coord position, int direction)
    {
        switch (direction)
        {
            case 0: return new Coord(position.x - 1, position.y + Mathf.Abs(position.x % 2));
            case 2: return new Coord(position.x + 1, position.y + Mathf.Abs(position.x % 2));
            case 4: return new Coord(position.x + 1, position.y - Mathf.Abs((position.x + 1) % 2));
            case 6: return new Coord(position.x - 1, position.y - Mathf.Abs((position.x + 1) % 2));
            case 1: return new Coord(position.x, position.y + 1);
            case 3: return new Coord(position.x + 2, position.y);
            case 5: return new Coord(position.x, position.y - 1);
            case 7: return new Coord(position.x - 2, position.y);
        }

        return new Coord(0, 0);
    }

    public void SetPathStartCorrd(List<Coord> coords)
    {
        if (coords.Count > 0)
        {
            _pathStartCoord = coords;
        }
        else
        {
            _pathStartCoord.Add(_playerSpawnPosition);
            _pathStartCoord.Add(_playerSpawnPosition);
        }
    }

    public List<Coord> GetPathEndCoord()
    {
        return _pathEndCoord;
    }

    private void GenerateCoins()
    {
        var random = new System.Random(Guid.NewGuid().GetHashCode());
        var spawnChance = _coin.GetComponent<SpawnChance>().value;

        foreach (var coord in _pathTileCoords)
        {
            if (random.NextDouble() < spawnChance && coord.y > 5)
            {
                var position = CoordToPosition(coord.x, coord.y);
                var coin = Instantiate(_coin, position + Vector3.up * 1, transform.rotation);
                coin.transform.SetParent(transform.Find("GridHolder").transform);
            }
        }
    }

    public Vector3 CoordToPosition(int x, int y)
    {
        var tileHalfSize = _tileWidth / 2;
        var posOffSet = x % 2 == 0 ? 0 : tileHalfSize;

        return new Vector3(x * tileHalfSize, 0, y * _tileWidth + posOffSet);
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
}
