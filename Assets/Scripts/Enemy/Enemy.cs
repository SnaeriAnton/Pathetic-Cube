using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private EnemyDie _die;

    private int _valueBar = 10;
    private bool _isZone = false;

    private bool Death => _healthBar.Health <= 0;


    private void Update()
    {
        if (Death == true)
            _die.Die();
    }

    public void TakeDamage()
    {
        _healthBar.OnValyeChanged(-_valueBar);
    }

    public void Cure()
    {
        _healthBar.OnValyeChanged(_valueBar);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            _die.Die();
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DamageZone>(out DamageZone damageZone) && _isZone == false)
        {
            _isZone = true;
            _healthBar.ExplosionDamage();
        }

    }
}
