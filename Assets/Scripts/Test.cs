using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            _healthBar.OnValyeChanged(-10);

        if (Input.GetKeyDown(KeyCode.Space))
            _healthBar.OnValyeChanged(10);
    }
}
