using System.Collections;
using UnityEngine;

public class EnemyEjector : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _transform;

    private Vector3 _positionExplosion;
    private Vector3 _forceDirection;
    private float _dilay = 0.5f;
    private bool _isForce = false;
    private Coroutine _detain;
    private float _offseZ = 0;
    private float _offseY = 3;
    private float _offseX = 0;
    private float _force = 15;


    private void FixedUpdate()
    {
        if (_isForce == true)
            AddForce();
    }

    public void Discard(Vector3 direction)
    {
        _isForce = true;
        _detain = StartCoroutine(Detain());
        _positionExplosion = direction;
    }

    private void AddForce()
    {
        if (_transform.position.z > _positionExplosion.z)
            _offseZ = _force;
        else
            _offseZ = -_force;

        if (_transform.position.x > _positionExplosion.x)
            _offseX = _force;
        else
            _offseX = -_force;

        _forceDirection = new Vector3(_offseX, _offseY, _offseZ);
        _rigidbody.AddForce(_forceDirection);
    }

    private IEnumerator Detain()
    {
        var dilay = new WaitForSeconds(_dilay);
        yield return dilay;
        _isForce = false;
        if (_detain != null)
            StopCoroutine(_detain);
    }
}
