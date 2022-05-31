using UnityEngine;
using System;

public class Hummer : MonoBehaviour
{
    void Start()
    {
        var random = new System.Random(Guid.NewGuid().GetHashCode());

        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + random.Next(0, 3) * 90, transform.rotation.eulerAngles.z));
    }
}
