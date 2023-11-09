using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuantityCounterItem : MonoBehaviour
{
    public event Action Click;

    [SerializeField] private Button _buttonPlus;
    [SerializeField] private Button _buttonMinus;

    [SerializeField] private IntValueView _textValue;

    private int _currentValue = 0;
    private int _minValue = 1;

    public int Value => _currentValue;

    private void OnEnable()
    {
        _buttonPlus.onClick.AddListener(OnButtonPlusClick);
        _buttonMinus.onClick.AddListener(OnButtonMinusClick);
    }

    private void OnDisable()
    {
        _buttonPlus.onClick.RemoveListener(OnButtonPlusClick);
        _buttonMinus.onClick.RemoveListener(OnButtonMinusClick);
    }

    private void OnButtonPlusClick()
    {
        _currentValue++;
        _textValue.Calculate(_currentValue);

        Click?.Invoke();
    }

    private void OnButtonMinusClick()
    {
        if (_currentValue - 1 < _minValue)
            return;

        _currentValue--;
        _textValue.Calculate(_currentValue);

        Click?.Invoke();
    }

    public void Show()
    {
        gameObject.SetActive(true);

        _currentValue = _minValue;
        _textValue.Calculate(_currentValue);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
