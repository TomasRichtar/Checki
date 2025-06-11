using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiseButtons : MonoBehaviour
{
    [SerializeField] private List<Button> buttons = new List<Button>();

    private Image _image;
    private TextMeshProUGUI _text;
    public int IsSelected = 0;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }

}
