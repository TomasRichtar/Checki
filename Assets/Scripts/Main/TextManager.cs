using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _childNames = new List<TextMeshProUGUI>();
    [SerializeField] private List<Image> _childImage = new List<Image>();

    private Children _child;
    // Start is called before the first frame update
    void Start()
    {
        _child = MyGameManager.Instance.ChildrenList.FirstOrDefault(x => x.Id == MyGameManager.Instance.ChildrenId);

        foreach (var item in _childNames)
        {
            item.text = _child.Name;
        }
        foreach (var item in _childImage)
        {
            item.sprite = SpriteManager.Instance.ProfileSprites[_child.ImageId];
        }
    }
}
