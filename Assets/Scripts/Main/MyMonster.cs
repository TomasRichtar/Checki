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

    [SerializeField] private List<Image> SelectedEquipmentHat = new List<Image>();
    [SerializeField] private List<Image> SelectedEquipmentHead = new List<Image>();
    [SerializeField] private List<Image> SelectedEquipmentShoulders = new List<Image>();
    [SerializeField] private List<Image> SelectedEquipmentNeck = new List<Image>();
    [SerializeField] private List<Image> SelectedEquipmentGlasses = new List<Image>();
    [SerializeField] private List<Image> SelectedEquipmentBeard = new List<Image>();
    [SerializeField] private List<Image> SelectedEquipmentGloves = new List<Image>();
    [SerializeField] private List<Image> SelectedEquipmentShoes = new List<Image>();
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
            { EquipmentSlot.Gloves, SelectedEquipmentGloves },
            { EquipmentSlot.Shoes, SelectedEquipmentShoes },
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
        foreach (var entry in slotToUI)
        {
            foreach (var image in entry.Value)
            {
                image.gameObject.SetActive(false);
            }
        }

        if (SelectedEquipment == null || SelectedEquipment.Name == "None")
            return;

        var slot = SelectedEquipment.EquipmentSlot;

        if (!slotToUI.TryGetValue(slot, out var images)) return;

        if (slot == EquipmentSlot.Gloves || slot == EquipmentSlot.Shoes)
        {
            for (int i = 0; i < images.Count; i++)
            {
                var image = images[i];
                image.gameObject.SetActive(true);

                // Nastav správný sprite podle indexu
                image.sprite = (i % 2 == 0) ? SelectedEquipment.Sprite : SelectedEquipment.OffSprite;

                // Nastav pozici, velikost a rotaci podle pravé/levé
                var rt = image.rectTransform;
                if (slot == EquipmentSlot.Gloves)
                {
                    if (i % 2 == 0)
                    {
                        rt.localPosition = SelectedMonster.GlovesRightOffset;
                        rt.sizeDelta = new Vector2(SelectedMonster.GlovesRightSize.x, SelectedMonster.GlovesRightSize.y);
                        rt.localEulerAngles = SelectedMonster.GlovesRightRotation;
                    }
                    else
                    {
                        rt.localPosition = SelectedMonster.GlovesLeftOffset;
                        rt.sizeDelta = new Vector2(SelectedMonster.GlovesLeftSize.x, SelectedMonster.GlovesLeftSize.y);
                        rt.localEulerAngles = SelectedMonster.GlovesLeftRotation;
                    }
                }
                else if (slot == EquipmentSlot.Shoes)
                {
                    if (i % 2 == 0)
                    {
                        rt.localPosition = SelectedMonster.ShoesRightOffset;
                        rt.sizeDelta = new Vector2(SelectedMonster.ShoesRightSize.x, SelectedMonster.ShoesRightSize.y);
                        rt.localEulerAngles = SelectedMonster.ShoesRightRotation;
                    }
                    else
                    {
                        rt.localPosition = SelectedMonster.ShoesLeftOffset;
                        rt.sizeDelta = new Vector2(SelectedMonster.ShoesLeftSize.x, SelectedMonster.ShoesLeftSize.y);
                        rt.localEulerAngles = SelectedMonster.ShoesLeftRotation;
                    }
                }
            }
        }
        else
        {
            GetTransformDataForSlot(slot, out var offset, out var size, out var rotation);
            foreach (var image in images)
            {
                image.gameObject.SetActive(true);
                image.sprite = SelectedEquipment.Sprite;

                var rt = image.rectTransform;
                rt.localPosition = offset;
                rt.sizeDelta = new Vector2(size.x, size.y);
                rt.localEulerAngles = rotation;
            }
        }
    }

    private void GetTransformDataForSlot(EquipmentSlot slot, out Vector3 offset, out Vector3 size, out Vector3 rotation)
    {
        offset = Vector3.zero;
        size = Vector3.one;
        rotation = Vector3.zero;

        if (SelectedMonster == null) return;

        switch (slot)
        {
            case EquipmentSlot.Hat:
                offset = SelectedMonster.HatOffset;
                size = SelectedMonster.HatSize;
                rotation = SelectedMonster.HatRotation;
                break;
            case EquipmentSlot.Head:
                offset = SelectedMonster.HeadOffset;
                size = SelectedMonster.HeadSize;
                rotation = SelectedMonster.HeadRotation;
                break;
            case EquipmentSlot.Shoulders:
                offset = SelectedMonster.ShouldersOffset;
                size = SelectedMonster.ShouldersSize;
                rotation = SelectedMonster.ShouldersRotation;
                break;
            case EquipmentSlot.Neck:
                offset = SelectedMonster.NeckOffset;
                size = SelectedMonster.NeckSize;
                rotation = SelectedMonster.NeckRotation;
                break;
            case EquipmentSlot.Glasses:
                offset = SelectedMonster.GlassesOffset;
                size = SelectedMonster.GlassesSize;
                rotation = SelectedMonster.GlassesRotation;
                break;
            case EquipmentSlot.Beard:
                offset = SelectedMonster.BeardOffset;
                size = SelectedMonster.BeardSize;
                rotation = SelectedMonster.BeardRotation;
                break;
            case EquipmentSlot.Gloves:
                break;
            case EquipmentSlot.Shoes:
                break;
            case EquipmentSlot.Hand:
                offset = SelectedMonster.HandOffset;
                size = SelectedMonster.HandSize;
                rotation = SelectedMonster.HandRotation;
                break;
            case EquipmentSlot.Waist:
                offset = SelectedMonster.WaistOffset;
                size = SelectedMonster.WaistSize;
                rotation = SelectedMonster.WaistRotation;
                break;
        }
    }


    public void SetSelectedMonster(Monster monster)
    {
        SelectedMonster = monster;
        UpdateSelectedMonsterUI();
        if (SelectedEquipment)
        {
            SetSelectedEquipment(SelectedEquipment);
        }
        else
        {
            Children child = MyGameManager.Instance.ChildrenList.FirstOrDefault(x => x.Id == MyGameManager.Instance.ChildrenId);
            SetSelectedEquipment(MyGameManager.Instance.AllEquipmentList.FirstOrDefault(x => x.Id == int.Parse(child.SelectedEquipment)));
        }
    }

    public void SetSelectedEquipment(Equipment equipment)
    {
        SelectedEquipment = equipment;
        UpdateSelectedEquipmentUI();
    }
}
