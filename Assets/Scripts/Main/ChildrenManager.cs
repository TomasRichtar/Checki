using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using TMPro;
using UnityEngine;

public class ChildrenManager : SingletonMonoBehaviour<ChildrenManager>
{
    [SerializeField] private List<TextMeshProUGUI> TextNames = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> TextNicknames = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> TextAge = new List<TextMeshProUGUI>();

    public void UpdateChildUI(Children children)
    {
        foreach (var text in TextNames)
        {
            text.text = children.Name;
        }
        foreach (var text in TextNicknames)
        {
            text.text = children.Nickname;
        }
        foreach (var text in TextAge)
        {
            text.text = children.Age.ToString();
        }
    }
}
