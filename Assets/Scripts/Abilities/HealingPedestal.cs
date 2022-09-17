using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPedestal : MonoBehaviour 
{
    [SerializeField] private Transform _center;
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private GameObject _gameObject;

    private float _radiusCollider = 4.329885f;
    private List<Enemy> _enemies = new List<Enemy>();
    private float _dilay = 15f;
    private Coroutine _work;

    private void Start()
    {
        _sphereCollider.enabled = true;
        _sphereCollider.radius = _radiusCollider;
        _work = StartCoroutine(Work());
    }

    private void Update()
    {
        if (_enemies.Count > 0)
            Accept();
    }

    private void Accept()
    {
        foreach (var enemy in _enemies)
            enemy.Accept(_center.position, true);
    }

    private void OnRemove(Enemy enemy)
    {
        _enemies.Remove(enemy);
        enemy.Died += OnRemove;
    }

    private IEnumerator Work()
    {
        var dilay = new WaitForSeconds(_dilay);
        yield return dilay;
        if (_work != null)
            StopCoroutine(_work);
        Destroy(_gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _enemies.Add(enemy);
            enemy.Died += OnRemove;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
            OnRemove(enemy);
    }
}
