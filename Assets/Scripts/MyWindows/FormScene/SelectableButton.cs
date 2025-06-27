using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectableButton : MonoBehaviour
{
    [SerializeField] private List<SelectableButton> buttons = new List<SelectableButton>();
    [SerializeField] private bool changeSprite;
    [SerializeField] private Sprite spriteSelected;
    [SerializeField] private Sprite spriteNotSelected;

    private Button _button;
    private Image _image;
    private TextMeshProUGUI _text;
    public int IsSelected = 0;
    public int ImageId = 0;
    public string Data = "";

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

    public void Selected()
    {
        foreach (SelectableButton button in buttons)
        {
            if (button.IsSelected == 1)
            {
                button.ResetTheButton();
            }
        }

        IsSelected = IsSelected == 0 ? 1 : 0;

        if (changeSprite)
        {
           _image.sprite = IsSelected == 1 ? spriteSelected : spriteNotSelected;
        }
        else
        {
            _image.color = IsSelected == 1 ? new Color32(255, 0, 140, 255) : new Color32(229, 229, 229, 255);
        }
        if (_text)
        {
            _text.color = IsSelected == 1 ? new Color32(229, 229, 229, 255) : new Color32(255, 0, 140, 255);
        }
    }
    private void ResetTheButton()
    {
        IsSelected = 0;

        if (changeSprite)
        {
            _image.sprite = spriteNotSelected;
        }
        else
        {
            _image.color = Color.white;
        }
        if (_text)
        {
            _text.color = new Color32(255, 0, 140, 255);
        }
    }
}
