using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChildrenCollectionAddButon : MonoBehaviour
{
    public void CreateButton()
    {
    }

    public void SelectThis()
    {
        WindowController.Instance.ForceEnter<AddChildrenMainWindow>();
        WindowController.Instance.PushWindow<AddChildrenPartOneWindow>();
    }
}
