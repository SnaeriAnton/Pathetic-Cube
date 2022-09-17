using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _transform;


    private void Update()
    {
        _transform.LookAt(_player);
    }
}
