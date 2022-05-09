using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Player>();
            if (player)
            {
                player.SetDead(true, AnalyticsHelper.DeadReason.Obstacle_collide);
            }
        }
    }
}
