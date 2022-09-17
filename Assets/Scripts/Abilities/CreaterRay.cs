using UnityEngine;
using UnityEngine.Events;

public class CreaterRay : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _healingPedestal;
    [SerializeField] private Transform _arena;

    private bool _isHealingRay = false;
    private bool _isHealingPedestal = false;

    public event UnityAction<Enemy, bool> FiredRay;

    private void Update()
    {
        if (Input.GetMouseButton(0))
            CreateRay();
    }

    private void CreateRay()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000000000000000000f, _layerMask))
        {
            if (_isHealingPedestal == true)
            {
                Instantiate(_healingPedestal,  new Vector3(hit.point.x, hit.point.y + 0.2f, hit.point.z), Quaternion.identity, _arena);
                _isHealingPedestal = false;
            }
            else
            {

                hit.collider.TryGetComponent<Enemy>(out Enemy enemy);
                if (enemy != null)
                    FiredRay?.Invoke(enemy, _isHealingRay);
            }
            
        }
    }

    public void ChoiñeRay(bool value)
    {
        _isHealingRay = value;
    }

    public void SelectePedestal(bool value)
    {
        Debug.Log(value);
        _isHealingPedestal = value;
    }
}

