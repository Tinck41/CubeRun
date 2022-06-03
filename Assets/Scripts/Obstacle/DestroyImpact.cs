using UnityEngine;

public class DestroyImpact : MonoBehaviour
{
    [SerializeField] private Obstacle _holder;

    private void Start()
    {
        if (_holder == null)
        {
            Debug.LogError("Holder for destroy impact wasn't set");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            if (player.isDead) return;

            player.skinManager.GetSelectedSkin().GetCommonSkin().SetActive(false);
            player.skinManager.GetSelectedSkin().CreateFracturedSkin();

            player.SetDead(AnalyticsHelper.DeadReason.Obstacle_collide);

            if (_holder != null)
            {
                AnalyticsHelper.OnObstacleCollide(_holder.type);
            }
        }
    }
}
