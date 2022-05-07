using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] private GameObject Chunk;
    [SerializeField] private GameObject Player;

    [SerializeField] private int PoolRange;
    [SerializeField] private int ChunkLength;

    [SerializeField] private float CheckOffSet;

    private Queue<GameObject> _pool;

    void Start()
    {
        _pool = new Queue<GameObject>(PoolRange);

        for (int i = 0; i < PoolRange; i++)
        {
            var newChunk = GenerateChunk();
            if (newChunk)
            {
                _pool.Enqueue(newChunk);
            }
        }
    }

    void Update()
    {
        if (Player.transform.position.z > _pool.Peek().transform.position.z + ChunkLength + CheckOffSet)
        {
            Destroy(_pool.Dequeue());

            var newChunk = GenerateChunk();
            if (newChunk)
            {
                _pool.Enqueue(newChunk);
            }
        }
    }

    GameObject GenerateChunk()
    {
        if (Chunk)
        {
            var chunkPosition = _pool.FirstOrDefault() == null ? Vector3.zero : _pool.Last().transform.position + new Vector3(0, 0, ChunkLength / 2 * Mathf.Sqrt(2));
            var newChunk = Instantiate(Chunk, Vector3.zero, Chunk.GetComponent<Transform>().rotation);
            newChunk.transform.position = chunkPosition;
            newChunk.GetComponent<TileGridScript>().AlignGrid();
            newChunk.transform.SetParent(transform);
            return newChunk.gameObject;
        }

        Debug.LogError("No chunk prefab was set");
        return null;
    }
}
