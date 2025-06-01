using System;
using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using UnityEngine;

public class EquipmentCollection : SingletonMonoBehaviour<EquipmentCollection>
{
    public List<Equipment> AllEquipments = new List<Equipment>();
    public List<Equipment> MyEquipments = new List<Equipment>();
    public List<Equipment> LockedEquipments = new List<Equipment>();

    [SerializeField] private Transform _equipmentLayout;
    [SerializeField] private EquipmentCollectionButton _EquipmentCollectionButton;

    public event Action OnEquipmentLoaded;
    public event Action OnNewEquipmentUnlocked;

    private void OnEnable()
    {
        OnEquipmentLoaded += LoadEquipmentColletion;
    }
    private void OnDisable()
    {
        OnNewEquipmentUnlocked -= LoadEquipmentColletion;
    }


    private void Start()
    {
        UpdateEquipmentData();
    }
    public void UpdateEquipmentData()
    {
        SetAllEquipments();
        LoadEquipmentColletion();
    }

    public void SetAllEquipments()
    {
        MyEquipments.Clear();
        LockedEquipments.Clear();

        var unlockedSet = new HashSet<string>(MyGameManager.Instance.UnlockedEquipmentList);

        foreach (var Equipment in AllEquipments)
        {
            if (unlockedSet.Contains(Equipment.Name))
            {
                MyEquipments.Add(Equipment);
            }
            else
            {
                LockedEquipments.Add(Equipment);
            }
        }

        foreach (var equipment in MyEquipments)
        {
            if (equipment.Name == MyGameManager.Instance.SelectedEquipmnet)
            {
                MyMonster.Instance.SetSelectedEquipment(equipment);
            }
            else
            {
                Debug.Log("This Monster is not unlocked: " + MyGameManager.Instance.SelectedMonster);
            }
        }

        OnEquipmentLoaded?.Invoke();
    }

    public void LoadEquipmentColletion()
    {
        foreach (Transform item in _equipmentLayout)
        {
            Destroy(item.gameObject);
        }

        float collectionWidth = 0;

        foreach (var Equipment in MyEquipments)
        {
            EquipmentCollectionButton EquipmentButton = Instantiate(_EquipmentCollectionButton, Vector3.zero, Quaternion.identity, _equipmentLayout);
            EquipmentButton.CreateButton(Equipment, false);
            collectionWidth += 200;
        }

        foreach (var Equipment in LockedEquipments)
        {
            EquipmentCollectionButton EquipmentButton = Instantiate(_EquipmentCollectionButton, Vector3.zero, Quaternion.identity, _equipmentLayout);
            EquipmentButton.CreateButton(Equipment, true);
            collectionWidth += 200;
        }
        float gapWidth = (MyEquipments.Count + LockedEquipments.Count - 1) * 50;
        collectionWidth += gapWidth;

        RectTransform rt = _equipmentLayout.GetComponent<RectTransform>();
        Vector2 size = rt.sizeDelta;
        size.x = collectionWidth;
        rt.sizeDelta = size;
    }
    public bool CheckIfUnLocked(Equipment equipment)
    {
        if (MyEquipments.Contains(equipment))
        {
            return true;
        }
        return false;
    }

    public void UnlockNewMonster(Equipment equipment)
    {
        if (CheckIfUnLocked(equipment)) return;

        LockedEquipments.Remove(equipment);
        MyEquipments.Add(equipment);

        MyGameManager.Instance.UnlockedMonstersList.Add(equipment.Name);
        OnNewEquipmentUnlocked?.Invoke();
    }
}
