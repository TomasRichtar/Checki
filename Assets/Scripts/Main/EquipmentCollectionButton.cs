using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentCollectionButton : MonoBehaviour
{
    [SerializeField] private Image _equipmentImage;
    [SerializeField] private GameObject _lock;

    private Equipment _equipment;

    public void CreateButton(Equipment equipment, bool isLocked)
    {
        _equipment = equipment;
        _equipmentImage.sprite = equipment.Sprite;

        _lock.SetActive(isLocked);
        _equipmentImage.gameObject.SetActive(!isLocked);
    }

    public void SelectThisEquipment()
    {
        if (EquipmentCollection.Instance.CheckIfUnLocked(_equipment))
        {
            Children child = MyGameManager.Instance.ChildrenList[MyGameManager.Instance.ChildrenId];
            child.SelectedEquipment = _equipment.Id.ToString();

            ChildrenDatabase.Instance.UpdateData(child, (response) =>
            {
                MyMonster.Instance.SetSelectedEquipment(_equipment);
            });
        }
    }
}
