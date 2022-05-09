using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
    [SerializeField] private GameObject _chunkPrefab;
    [SerializeField] private GameObject _player;

    [SerializeField] private Vector2Int _chunkSize;

    [SerializeField] private int _poolRange;

    [SerializeField] private float _checkOffSet;

    [SerializeField] private int _seed;
    [Range(0, 1)]
    [SerializeField] private float _obstaclePercent;

    private Queue<GameObject> _pool;


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
        if (_player.transform.position.z > _pool.Peek().transform.position.z + _chunkSize.y + _checkOffSet)
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

        var chunkPosition = _pool.FirstOrDefault() == null ? Vector3.zero : _pool.Last().transform.position + new Vector3(0, 0, _chunkSize.y / 2 * Mathf.Sqrt(2));
        var newChunk = Instantiate(_chunkPrefab, Vector3.zero, _chunkPrefab.GetComponent<Transform>().rotation);
        if (newChunk)
        {
            newChunk.GetComponent<ChunkGenerator>().GenerateGrid(_chunkSize, _seed, _obstaclePercent);
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

        Load();
    }
}
