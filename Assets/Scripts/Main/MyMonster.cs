using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TastyCore.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyMonster : SingletonMonoBehaviour<MyMonster>
{
    public Monster SelectedMonster;
    public Equipment SelectedEquipment;

    [SerializeField] private List<Image> SelectedEquipmentHead = new List<Image>();
    [SerializeField] private List<Image> SelectedEquipmentNeck = new List<Image>();
    [SerializeField] private List<Image> SelectedEquipmentBeard = new List<Image>();
    [SerializeField] private List<Image> SelectedEquipmentGlasses = new List<Image>();

    [SerializeField] private List<Image> SelectedMonsterImages = new List<Image>();
    [SerializeField] private List<TextMeshProUGUI> SelectedMonsterTextNames = new List<TextMeshProUGUI>();

    private void Start()
    {
        Children child = MyGameManager.Instance.ChildrenList.FirstOrDefault(x => x.Id == MyGameManager.Instance.ChildrenId);

        SetSelectedEquipment(MyGameManager.Instance.AllEquipmentList[int.Parse(child.SelectedEquipment)]);
        SetSelectedMonster(MyGameManager.Instance.AllMonsterList[int.Parse(child.SelectedMonster)]);
    }
    public void UpdateSelectedMonsterUI()
    {
        foreach (var image in SelectedMonsterImages)
        {
            image.sprite = SelectedMonster.Sprite;
        }

        foreach (var text in SelectedMonsterTextNames)
        {
            text.text = SelectedMonster.Name;
        }
    }

    public void UpdateSelectedEquipmentUI()
    {
        foreach (var image in SelectedEquipmentHead)
        {
            image.gameObject.SetActive(false);
        }
        foreach (var image in SelectedEquipmentGlasses)
        {
            image.gameObject.SetActive(false);
        }
        foreach (var image in SelectedEquipmentNeck)
        {
            image.gameObject.SetActive(false);
        }
        foreach (var image in SelectedEquipmentBeard)
        {
            image.gameObject.SetActive(false);
        }

        if (SelectedEquipment.Name == "None")
        {
            return;
        }

        switch (SelectedEquipment.EquipmentSLot)
        {
            case EquipmentSlot.Head:
                foreach (var image in SelectedEquipmentHead)
                {
                    image.gameObject.SetActive(true);
                    image.sprite = SelectedEquipment.Sprite;
                }
                break;
            case EquipmentSlot.Necklaces:
                foreach (var image in SelectedEquipmentNeck)
                {
                    image.gameObject.SetActive(true);
                    image.sprite = SelectedEquipment.Sprite;
                }
                break;
            case EquipmentSlot.Glasses:
                foreach (var image in SelectedEquipmentGlasses)
                {
                    image.gameObject.SetActive(true);
                    image.sprite = SelectedEquipment.Sprite;
                }
                break;
            case EquipmentSlot.Beard:
                foreach (var image in SelectedEquipmentBeard)
                {
                    image.gameObject.SetActive(true);
                    image.sprite = SelectedEquipment.Sprite;
                }
                break;
            default:
                break;
        }
       

    }

    public void SetSelectedMonster(Monster monster)
    {
        SelectedMonster = monster;
        UpdateSelectedMonsterUI();
    }

    public void SetSelectedEquipment(Equipment equipment)
    {
        SelectedEquipment = equipment;
        UpdateSelectedEquipmentUI();
    }
}
