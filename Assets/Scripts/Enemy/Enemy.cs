using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private EnemyDie _die;
    [SerializeField] private EnemyEjector _discard;
    [SerializeField] private Transform _transform;

    private int _valueBar = 10;
    private bool _isZone = false;
    private float _speed = 0.5f;
    private float _dilay = 1f;
    private Coroutine _detain;
    private float _offseZ = 0;
    private float _offseX = 0;
    private float _force = 1;

    private bool Death => _healthBar.Health <= 0;
    public Transform Transform => _transform;

    public event UnityAction<Enemy> Died;

    private void Update()
    {
        if (Death == true)
            Die();
    }

    public void Accept(Vector3 direction, bool isCure)
    {
        if (_detain == null)
        {
            if (isCure == true)
                _detain = StartCoroutine(Detain(_valueBar));
            else
                _detain = StartCoroutine(Detain(-_valueBar));
        }

        if (isCure == false)
        {
            if (_transform.position.z > direction.z)
                _offseZ = _force;
            else
                _offseZ = -_force;

            if (_transform.position.x > direction.x)
                _offseX = _force;
            else
                _offseX = -_force;

            direction = new Vector3(_transform.position.x + _offseX, _transform.position.y, _transform.position.z + _offseZ);
        }

        


        _transform.position = Vector3.MoveTowards(_transform.position, direction, _speed * Time.deltaTime);
    }

    public void StopAction()
    {
        if (_detain != null)
        {
            StopCoroutine(_detain);
            _detain = null;
        }
    }

    private void Die()
    {
        if (_detain != null)
            StopCoroutine(_detain);

        _die.Die();
        Died?.Invoke(this);
    }

    public void TakeDamage()
    {
        _healthBar.OnValyeChanged(-_valueBar);
    }

    public void Cure()
    {
        _healthBar.OnValyeChanged(_valueBar);
    }

    private IEnumerator Detain(int value)
    {
        var dilay = new WaitForSeconds(_dilay);
        while (true)
        {
            yield return dilay;
            _healthBar.OnValyeChanged(value);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DamageZone>(out DamageZone damageZone) && _isZone == false)
        {
            _isZone = true;
            _healthBar.ExplosionDamage();
            _discard.Discard(other.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<HealingPedestal>(out HealingPedestal healingPedestal))
            if (_detain != null)
                StopCoroutine(_detain);
    }
}
