using UnityEngine;

public class Mover : MonoBehaviour
{
    private const string VERTICAL = "Vertical";

    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis(VERTICAL) * _speed);
    }
}
