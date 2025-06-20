using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Metadata;

public class ChildrenQuestCollectionButton : MonoBehaviour
{
    [Header("Button UI Elements")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _image;

    private Children _children;

    public void CreateButton(Children children)
    {
        _children = children;

        _nameText.text = children.Name;
    }

    public void SelectThis()
    {
        ProfileManager.Instance.SelectChild(_children);
    }
}
