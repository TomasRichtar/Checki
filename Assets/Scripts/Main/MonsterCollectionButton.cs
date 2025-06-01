using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCollectionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _monsterImage;
    [SerializeField] private GameObject _lock;
    [SerializeField] private Image _glow;

    private Monster _monster;

    public void CreateButton(Monster monster, bool isLocked)
    {
        _monster = monster;

        _nameText.text = monster.Name;
        _monsterImage.sprite = monster.Sprite;

        _lock.SetActive(isLocked);
        _monsterImage.gameObject.SetActive(!isLocked);
        _nameText.gameObject.SetActive(!isLocked);
        _glow.color = monster.GlowColor;
    }

    public void SelectThisMonster()
    {
        if (MonsterCollection.Instance.CheckIfUnLocked(_monster))
        {
            MyGameManager.Instance.SelectNewMonster(_monster.Name);
            MyMonster.Instance.SetSelectedMonster(_monster);
        }
    }
}
