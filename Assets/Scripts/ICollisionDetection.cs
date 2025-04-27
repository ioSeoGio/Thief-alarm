using System;
using UnityEngine;

public interface ICollisionDetection
{
    public event Action CollisionEnter;
    public event Action CollisionExit;
}
