using UnityEngine;
using UnityEngine.Events;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _speedRotation = 1f;

    private float _rotateAngle = 2;

    public event UnityAction Moved;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
            Move();

        if (Input.GetKey(KeyCode.A))
            Rotate(-_rotateAngle);

        if (Input.GetKey(KeyCode.D))
            Rotate(_rotateAngle);
    }

    private void Move()
    {
        Moved?.Invoke();
        _transform.position = Vector3.MoveTowards(_transform.position, _transform.position + _transform.forward, _speed * Time.deltaTime);
    }

    private void Rotate(float side)
    {
        Moved?.Invoke();
        _transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.Euler(_transform.rotation.eulerAngles.x, _transform.rotation.eulerAngles.y + side, _transform.rotation.eulerAngles.z), _speedRotation + Time.deltaTime);
    }
}
