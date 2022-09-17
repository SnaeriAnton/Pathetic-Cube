using UnityEngine;

public class DamageZone : MonoBehaviour 
{
    [SerializeField] private SphereCollider _sphereCollider;

    private float _radisu = 4f;

    public void SetRadiusSpher()
    {
        _sphereCollider.radius = _radisu;
    }
}
