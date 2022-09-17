using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private DamageZone _damageZone;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private BoxCollider _boxCollider;

    private float _dilayDie = 2f;
    private bool _death = false;

    public void Die()
    {
        if (_death == false)
        {
            Destroy(_enemy);
            _death = true;
            _boxCollider.enabled = false;
            _damageZone.SetRadiusSpher();
            _particleSystem.Play();
            _mesh.enabled = false;
            _canvas.SetActive(false);
            Destroy(_gameObject, _dilayDie);
        }
    }
}
