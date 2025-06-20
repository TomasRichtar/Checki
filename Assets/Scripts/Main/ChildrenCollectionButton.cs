using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChildrenCollectionButton : MonoBehaviour
{
    [Header("Button UI Elements")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _image;

    private Children _children;

    public void CreateButton(Children children)
    {
        _children = children;

        _nameText.text = children.Name;
        _image.sprite = SpriteManager.Instance.ProfileSprites[children.ImageId];
    }

    public void SelectThis()
    {
        WindowController.Instance.PushWindow<ProfileWindow>();
        ProfileManager.Instance.SelectChild(_children);
    }
}
