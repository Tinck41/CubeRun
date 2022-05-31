using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    [SerializeField] protected GameObject UI;

    public virtual void EnterState() { }

    public virtual void UpdateState() { }

    public virtual void LeaveState() { }
}
