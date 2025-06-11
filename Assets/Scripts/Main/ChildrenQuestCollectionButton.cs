using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class ChildrenQuestCollectionButton : MonoBehaviour
{
    [Header("Button UI Elements")]
    [SerializeField] private TextMeshProUGUI _nameText;

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
