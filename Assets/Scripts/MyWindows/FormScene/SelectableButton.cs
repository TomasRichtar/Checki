using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectableButton : MonoBehaviour
{
    private Button _button;
    private Image _image;
    private TextMeshProUGUI _text;
    public bool IsSelected = false;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Selected);
    }
    private void OnDisable()
    {
        _button.onClick.RemoveListener(Selected);
    }

    private void Selected()
    {
        IsSelected = !IsSelected;

        _image.color = IsSelected ? new Color32(255, 0, 140, 255) : Color.white;
        _text.color = IsSelected ? Color.white : new Color32(255, 0, 140, 255);
    }
}
