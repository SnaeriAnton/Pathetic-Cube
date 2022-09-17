using UnityEngine;

public class Ray : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _player;
    [SerializeField] private CreaterRay _createrRay;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private Material _healingMat;
    [SerializeField] private Material _beatingMat;

    private bool _isFire = false;
    private Enemy _enemy;
    private bool _isHealingRay;

    private void OnEnable()
    {
        _createrRay.FiredRay += OnFire;
        _playerMover.Moved += OnStop;
    }

    private void OnDisable()
    {
        _createrRay.FiredRay -= OnFire;
        _playerMover.Moved -= OnStop;
    }

    private void Update()
    {
        if (_isFire == true)
            FireBeam();
    }

    private void OnFire(Enemy enemy, bool isHealingRay)
    {
        _isHealingRay = isHealingRay;
        enemy.Died += OnStop;
        _enemy = enemy;
        _isFire = true;
    }

    private void FireBeam()
    {
        _lineRenderer.enabled = true;

        if (_isHealingRay == true)
        {
            _lineRenderer.material = _healingMat;
            _enemy.Accept(_player.position, _isHealingRay);
        }
        else
        {
            _lineRenderer.material = _beatingMat;
            _enemy.Accept(-_player.position, _isHealingRay);
        }


        _lineRenderer.SetPosition(0, _player.position);
        _lineRenderer.SetPosition(1, _enemy.Transform.position);
    }

    private void OnStop()
    {
        _enemy.StopAction();
        _isFire = false;
        _lineRenderer.enabled = false;
    }

    private void OnStop(Enemy enemy)
    {
        enemy.Died -= OnStop;
        OnStop();
    }
}
