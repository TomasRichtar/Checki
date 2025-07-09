using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomDropDown : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;

    [Header("Compoments")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Mask _mask;

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        _openButton.onClick.AddListener(Open);
        _closeButton.onClick.AddListener(Close);
    }
    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(Open);
        _closeButton.onClick.RemoveListener(Close);
    }
    private void Start()
    {
        Debug.Log("Start");
    }

    public void Open()
    {
        Debug.Log("Open1");
        _animator.enabled = true;
        _animator.SetTrigger("OnOpen");
        _openButton.interactable = false;
        _closeButton.interactable = true;
        _mask.enabled = false;
        Debug.Log("Open2");
    }

    public void Close()
    {
        Debug.Log("Close1");
        _animator.enabled = true;
        _animator.SetTrigger("OnClose");
        _openButton.interactable = true;
        _closeButton.interactable = false;
        _mask.enabled = true;
        Debug.Log("Close2");
    }

    public void ForceClose()
    {
        Debug.Log("Forced");
        _animator.enabled = true;
        _animator.SetTrigger("ForceClose");
        _openButton.interactable = true;
        _closeButton.interactable = false;
        _mask.enabled = true;
    }

    public void StopIdleRepeat()
    {
        _animator.enabled= false;
    }
}
