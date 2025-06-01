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
        _openButton.onClick.AddListener(Open);
        _closeButton.onClick.AddListener(Close);
    }
    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(Open);
        _closeButton.onClick.RemoveListener(Close);
    }
 
    public void Open()
    {
        _animator.enabled = true;
        _animator.SetTrigger("OnOpen");
        _openButton.interactable = false;
        _closeButton.interactable = true;
        _mask.enabled = false;
    }

    public void Close()
    {
        _animator.enabled = true;
        _animator.SetTrigger("OnClose");
        _openButton.interactable = true;
        _closeButton.interactable = false;
        _mask.enabled = true;
    }

    public void StopIdleRepeat()
    {
        _animator.enabled= false;
    }
}
