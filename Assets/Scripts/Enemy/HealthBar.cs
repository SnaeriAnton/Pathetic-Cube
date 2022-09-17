using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private int _maxValue = 100;

    private void Start()
    {
        _slider.maxValue = _maxValue;
        _slider.value = _maxValue;
    }
    public void OnValyeChanged(int value)
    {
        _slider.value += value;

        if (true)
        {

        }
    }
}
