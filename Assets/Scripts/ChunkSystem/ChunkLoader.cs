using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
    [SerializeField] private GameObject _chunkPrefab;
    [SerializeField] private GameObject _player;

    [SerializeField] private int _poolRange;

    [SerializeField] private float _checkOffSet;

    private Queue<GameObject> _pool;

    private Vector2Int _chunkSize;

    void Start()
    {
        Reload();
    }

    private void Load()
    {
        _pool = new Queue<GameObject>(_poolRange);

        for (int i = 0; i < _poolRange; i++)
        {
            var newChunk = LoadChunk();
            if (newChunk)
            {
                _pool.Enqueue(newChunk);
            }
        }
    }

    void Update()
    {
        if (_player.transform.position.z > _pool.Peek().transform.position.z + _chunkSize.y * 2 + _checkOffSet)
        {
            Destroy(_pool.Dequeue());

            var newChunk = LoadChunk();
            if (newChunk)
            {
                _pool.Enqueue(newChunk);
            }
        }

    }

    GameObject LoadChunk()
    {
        if (!_chunkPrefab)
        {
            Debug.LogError("No chunk prefab was set");
            throw new UnassignedReferenceException();
        }

        var chunkPosition = _pool.FirstOrDefault() == null ? Vector3.zero : _pool.Last().transform.position + new Vector3(0, 0, _chunkSize.y * Mathf.Sqrt(2));
        var newChunk = Instantiate(_chunkPrefab, Vector3.zero, _chunkPrefab.GetComponent<Transform>().rotation);
        if (newChunk)
        {
            var chunkGenerator = newChunk.GetComponent<ChunkGenerator>();
            chunkGenerator.firstChunk = _pool.Count < 1;
            chunkGenerator.Intialize();
            chunkGenerator.SetPathStartCorrd(_pool.Count > 0 ? _pool.Last().GetComponent<ChunkGenerator>().GetPathEndCoord() : new List<ChunkGenerator.Coord>());
            chunkGenerator.GenerateChunk();
            newChunk.transform.position = chunkPosition;
            newChunk.GetComponent<ChunkGenerator>().AlignGrid();
            newChunk.transform.SetParent(transform);
            return newChunk.gameObject;
        }
        else
        {
            Debug.LogError("No chunk prefab was created");
            throw new MissingReferenceException();
        }
    }

    public void ReloadPlatformForRevive()
    {
        GameObject chunkWithPlayer = new GameObject();
        foreach (var chunk in _pool)
        {
            if (_player.transform.position.z >= chunk.transform.position.z - 0.05f)
            {
                chunkWithPlayer = chunk;
            }
        }

        chunkWithPlayer.GetComponent<ChunkGenerator>().ClearObstacles(_player);
    }

    public void Reload()
    {
        if (_pool != null)
        {
            _pool.Clear();
        }

        var childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        var chunkGenerator = _chunkPrefab.GetComponent<ChunkGenerator>();
        if (chunkGenerator != null)
        {
            _chunkSize = chunkGenerator.GetSize();
        }

        Load();
    }
}
