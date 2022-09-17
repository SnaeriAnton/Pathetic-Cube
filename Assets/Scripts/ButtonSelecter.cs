using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelecter : MonoBehaviour
{
    [SerializeField] private CreaterRay _createrRay;
    [SerializeField] private Button _buttonHealing;
    [SerializeField] private Button _buttonBeating;
    [SerializeField] private Button _buttonPedestal;
    [SerializeField] private GameObject _objectHealing;
    [SerializeField] private GameObject _objectBeating;
    [SerializeField] private GameObject _objectPedestal;


    private float _dilay = 0.5f;
    private float _dilayOedestal = 5f;
    private Coroutine _detainHealing;
    private Coroutine _detainBeating;
    private Coroutine _detainPedestail;

    private void OnEnable()
    {
        _buttonHealing.onClick.AddListener(OnSelectHealing);
        _buttonBeating.onClick.AddListener(OnSelectBeating);
        _buttonPedestal.onClick.AddListener(OnCreatePedestal);
    }

    private void OnDisable()
    {
        _buttonHealing.onClick.RemoveListener(OnSelectHealing);
        _buttonBeating.onClick.RemoveListener(OnSelectBeating);
        _buttonPedestal.onClick.RemoveListener(OnCreatePedestal);
    }

    private void OnSelectHealing()
    {
        _createrRay.SelectePedestal(false);
        _createrRay.ChoiñeRay(true);
        _objectHealing.SetActive(false);
        _detainHealing = StartCoroutine(DetainHealing());
    }

    private void OnSelectBeating()
    {
        _createrRay.SelectePedestal(false);
        _createrRay.ChoiñeRay(false);
        _objectBeating.SetActive(false);
        _detainBeating = StartCoroutine(DetainBeating());
    }

    private void OnCreatePedestal()
    {
        _createrRay.SelectePedestal(true);
        _objectPedestal.SetActive(false);
        _detainPedestail = StartCoroutine(DetainPedestail());
    }


    private IEnumerator DetainHealing()
    {
        var dilay = new WaitForSeconds(_dilay);
        yield return dilay;
        _objectHealing.SetActive(true);
        if (_detainHealing != null)
            StopCoroutine(_detainHealing);
    }

    private IEnumerator DetainBeating()
    {
        var dilay = new WaitForSeconds(_dilay);
        yield return dilay;
        _objectBeating.SetActive(true);
        if (_detainBeating != null)
            StopCoroutine(_detainBeating);
    }

    private IEnumerator DetainPedestail()
    {
        var dilay = new WaitForSeconds(_dilayOedestal);
        yield return dilay;
        _objectPedestal.SetActive(true);
        if (_detainPedestail != null)
            StopCoroutine(_detainPedestail);
    }

}
