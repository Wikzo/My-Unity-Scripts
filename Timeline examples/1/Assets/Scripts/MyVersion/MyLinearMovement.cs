using UnityEngine;
using System.Collections;

public class MyLinearMovement : MonoBehaviour, ITimeUpdate
{

    public float Speed = 1;
    public Vector3 Direction = Vector3.up;

    public void AddTime(float dt)
    {
        transform.position += Speed*Direction.normalized*dt;
    }
}
