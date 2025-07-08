using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TastyCore.Utils;
using TMPro;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using UnityEngine.UI;

public class MyMonster : SingletonMonoBehaviour<MyMonster>
{
    public Monster SelectedMonster;
    public Equipment SelectedEquipment;

    [SerializeField] private List<Image> SelectedEquipmentHat = new List<Image>();
    [SerializeField] private List<Image> SelectedEquipmentHead = new List<Image>();
    [SerializeField] private List<Image> SelectedEquipmentShoulders = new List<Image>();
    [SerializeField] private List<Image> SelectedEquipmentNeck = new List<Image>();
    [SerializeField] private List<Image> SelectedEquipmentGlasses = new List<Image>();
    [SerializeField] private List<Image> SelectedEquipmentBeard = new List<Image>();
    [SerializeField] private List<Image> SelectedEquipmentGlovesRight = new List<Image>();
    [SerializeField] private List<Image> SelectedEquipmentGlovesLeft = new List<Image>();
    [SerializeField] private List<Image> SelectedEquipmentShoesRight = new List<Image>();
    [SerializeField] private List<Image> SelectedEquipmentShoesLeft = new List<Image>();
    [SerializeField] private List<Image> SelectedEquipmentHand = new List<Image>();
    [SerializeField] private List<Image> SelectedEquipmentWaist = new List<Image>();

    private Dictionary<EquipmentSlot, List<Image>> slotToUI;

    [SerializeField] private List<Image> SelectedMonsterImages = new List<Image>();
    [SerializeField] private List<TextMeshProUGUI> SelectedMonsterTextNames = new List<TextMeshProUGUI>();

    private void Start()
    {
        slotToUI = new Dictionary<EquipmentSlot, List<Image>>
        {
            { EquipmentSlot.Hat, SelectedEquipmentHat },
            { EquipmentSlot.Head, SelectedEquipmentHead },
            { EquipmentSlot.Shoulders, SelectedEquipmentShoulders },
            { EquipmentSlot.Neck, SelectedEquipmentNeck },
            { EquipmentSlot.Glasses, SelectedEquipmentGlasses },
            { EquipmentSlot.Beard, SelectedEquipmentBeard },
            { EquipmentSlot.GlovesRight, SelectedEquipmentGlovesRight },
            { EquipmentSlot.GlovesLeft, SelectedEquipmentGlovesLeft },
            { EquipmentSlot.ShoesRight, SelectedEquipmentShoesRight },
            { EquipmentSlot.ShoesLeft, SelectedEquipmentShoesLeft },
            { EquipmentSlot.Hand, SelectedEquipmentHand },
            { EquipmentSlot.Waist, SelectedEquipmentWaist },
        };

        Children child = MyGameManager.Instance.ChildrenList.FirstOrDefault(x => x.Id == MyGameManager.Instance.ChildrenId);

        SetSelectedEquipment(MyGameManager.Instance.AllEquipmentList.FirstOrDefault(x => x.Id == int.Parse(child.SelectedEquipment)));
        SetSelectedMonster(MyGameManager.Instance.AllMonsterList.FirstOrDefault(x => x.Id == int.Parse(child.SelectedMonster)));

        //SetSelectedEquipment(MyGameManager.Instance.AllEquipmentList[int.Parse(child.SelectedEquipment)]);
        //SetSelectedMonster(MyGameManager.Instance.AllMonsterList[int.Parse(child.SelectedMonster)]);
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

        switch (SelectedEquipment.EquipmentSlot)
        {
            case EquipmentSlot.Head:
                foreach (var image in SelectedEquipmentHead)
                {
                    image.gameObject.SetActive(true);
                    image.sprite = SelectedEquipment.Sprite;
                }
                break;
            case EquipmentSlot.Neck:
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
