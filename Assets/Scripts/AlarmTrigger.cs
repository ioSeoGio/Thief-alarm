using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Collider))]
public class AlarmTrigger : MonoBehaviour, ICollisionDetection
{
    public event Action CollisionEnter;
    public event Action CollisionExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Mover>(out Mover mover))
        {
            CollisionEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Mover>(out Mover mover))
        {
            CollisionExit?.Invoke();
        }
    }
}
