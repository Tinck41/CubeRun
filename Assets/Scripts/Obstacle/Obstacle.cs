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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Player>();
            if (player)
            {
                player.SetDead(AnalyticsHelper.DeadReason.Obstacle_collide);
            }
        }
    }
}
