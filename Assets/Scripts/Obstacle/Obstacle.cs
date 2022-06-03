using UnityEngine;
using System.Collections.Generic;

public enum ObstacleType
{
    UNDEFINED = 0,
    STATIC,
    FLOATING,
    HUMMNER,
    DOUBLE_HUMMER
}

[RequireComponent(typeof(SpawnChance))]
public class Obstacle : MonoBehaviour
{
    public ObstacleType type;

    public List<Direction> occupiedDir;
}
