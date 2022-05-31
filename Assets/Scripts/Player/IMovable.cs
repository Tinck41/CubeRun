using UnityEngine;

public interface IMovable
{
    public void StartMove();
    public void ChangeDirection(Vector3 direction);
    public void StopMove();
}
