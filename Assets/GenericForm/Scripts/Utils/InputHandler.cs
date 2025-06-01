using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
    [SerializeField] private GameObject _parentLayout;

    private InputField _inputField;
    private static InputField[] _allInputFields;
    private float _lastTouchTime;

    public Action OnInputActivation;

    private void Awake()
    {
        _inputField = GetComponent<InputField>();
    }

    private void Start()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
            _inputField.shouldHideMobileInput = true;

        _allInputFields = _parentLayout.GetComponentsInChildren<InputField>();
        ActivateAllFields(false);
    }

    public static void ActivateAllFields(bool b)
    {
        foreach(InputField input in _allInputFields)
        {
            input.enabled = b;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!_inputField.isFocused)
        {
            ActivateAllFields(false);
            _lastTouchTime = Time.time;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(Time.time < _lastTouchTime + 0.2f)
        {
            ActivateAllFields(false);
            _inputField.enabled = true;
            _inputField.Select();
            _inputField.ActivateInputField();
            TouchScreenKeyboard.Open("");
            OnInputActivation?.Invoke();
        }
    }
}
