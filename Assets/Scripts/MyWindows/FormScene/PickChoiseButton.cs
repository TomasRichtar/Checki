using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickChoiseButton : MonoBehaviour
{
    private Button _button;
    private Image _image;
    private TextMeshProUGUI _text;
    public int IsSelected = 0;
    
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
        IsSelected = IsSelected == 0 ? 1 : 0;

        _image.color = IsSelected == 1 ? new Color32(255, 0, 140, 255) : Color.white;
        _text.color = IsSelected == 1 ? Color.white : new Color32(255, 0, 140, 255);
    }
}
