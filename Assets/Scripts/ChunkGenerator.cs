using System;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _commonTile;
    [SerializeField] private GameObject _OneLineTile;
    [SerializeField] private GameObject _TwoLineTile;
    [SerializeField] private GameObject _ThreeLineTile;
    [SerializeField] private GameObject _OneConnectionTile;
    [SerializeField] private GameObject _TwoCloseConnectionTile;
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

    public void GenerateGrid(Vector2Int size, int pathNum, float obstaclePercent, bool isFirstPlatform)
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

        if (_commonTile)
        {
            _grid = new GameObject[_size.x, _size.y];

            for (int y = 0; y < _size.y; y++)
            {
                var tileWidth = _commonTile.transform.localScale.x * Mathf.Sqrt(2);
                var offSet = y % 2 == 0 ? 0 : tileWidth / 2;
                for (int x = 0; x < _size.x - Mathf.Min(1, y % 2); x++)
                {
                    _grid[x, y] = Instantiate(_commonTile, new Vector3(x * tileWidth + offSet, 0, y * (tileWidth / 2)), transform.rotation);
                    _grid[x, y].transform.SetParent(gridHolder.transform);
                    _allTileCoords.Add(new Coord(x, y));
                }
            }

            for (int i = 0; i < pathNum; i++)
            {
                CreatePath();
            }

            _shuffledTileCoords = new Queue<Coord>(Utility.ShuffleArray(_allTileCoords.ToArray()));

            var obstacleCount = (int)(_shuffledTileCoords.Count * obstaclePercent);
            var width = transform.rotation.y > 0 ? _commonTile.transform.localScale.x * Mathf.Sqrt(2) : 1;
            for (int i = 0; i < obstacleCount; i++)
            {
                var randomCoord = GetRandomCoord();
                if (_pathTileCoords.Contains(randomCoord))
                {
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

                            //var globalShift = randomCoord.y % 2 == 0 ? 0 : 1;
                            //for (int y = -2; y <= 2; y++)
                            //{
                            //    var horizontalRule = Mathf.Abs(y) == 2 ? 0 : -1;
                            //    var localShift = Convert.ToInt32(y == 0) * (globalShift == 0 ? 1 : -1);
                            //    int x;
                            //    if (Mathf.Abs(y) == 2)
                            //    {
                            //        x = 0;
                            //    }
                            //    else
                            //    {
                            //        x = horizontalRule + globalShift + (globalShift != 0 ? localShift : 0);
                            //    }
                            //    for (x = x; x < 1 + globalShift + (globalShift == 0 ? localShift : 0); x++)
                            //    {
                            //        var neighbourX = randomCoord.x + x;
                            //        var neighbourY = randomCoord.y + y;

                            //        var offSet = neighbourY % 2 == 0 ? 0 : width / 2;
                            //        if (neighbourX >= 0 && neighbourX < _size.x &&
                            //            neighbourY >= 0 && neighbourY < size.y)
                            //        {
                            //            if (_grid[neighbourX, neighbourY] != null)
                            //            {
                            //                //ReplaceCommonTile(new Coord(neighbourX, neighbourY));
                            //                var obstacle = Instantiate(_obstacles[1], new Vector3(neighbourX * width + offSet, 0, neighbourY * (width / 2)) + Vector3.up * 1, transform.rotation);
                            //                obstacle.transform.SetParent(gridHolder.transform);
                            //            }
                            //        }
                            //    }
                            //}
                        }
                    }
                }
            }

            ApplyEdeges(isFirstPlatform);
        }
        else
        {
            Debug.LogError("No tile prefab was set");
            throw new UnassignedReferenceException();
        }
    }

    void ApplyEdeges(bool isFirstPlatform)
    {
        //if (_edgeTile)
        //{
        //    for (int y = 0; y < _size.y; y++)
        //    {
        //        var offset = y % 2 == 0 ? 0 : 1;
        //        for (int x = 0; x < _size.x; x++)
        //        {
        //            if (isFirstPlatform)
        //            {
        //                if (y == 0)
        //                {
        //                    ReplaceCommonTile(new Coord(x, y));
        //                }
        //            }
        //            if (((x == 0 || x == _size.x - 1) && offset != 1))
        //            {
        //                ReplaceCommonTile(new Coord(x, y));
        //            }
        //            if (_grid[x, y] == null)
        //            {

        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    Debug.LogError("No edge tile prefab was set");
        //}
    }

    void ReplaceCommonTile(Coord position)
    {
        var tileWidth = transform.rotation.y > 0 ? _commonTile.transform.localScale.x * Mathf.Sqrt(2) : 1;
        var posOffSet = transform.rotation.y > 0 ? (position.y % 2 == 0 ? 0 : tileWidth / 2) : 0;
        DestroyImmediate(_grid[position.x, position.y]);
        //_grid[position.x, position.y] = Instantiate(_edgeTile, new Vector3(position.x * tileWidth + posOffSet, 0, position.y * (tileWidth / 2)), transform.rotation);
        _grid[position.x, position.y].transform.SetParent((transform.Find("GridHolder").transform));
    }

    void CreatePath()
    {
        var currentPostion = _playerSpawnPosition;
        for (int y = currentPostion.y; y < _size.y; y++)
        {
            _pathTileCoords.Add(currentPostion);

            var tileWidth = _commonTile.transform.localScale.x * Mathf.Sqrt(2);
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

    public Vector3 CoordToPosition(int x, int y)
    {
        var tileWidth = transform.rotation.y > 0 ? _commonTile.transform.localScale.x * Mathf.Sqrt(2) : 1;
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
