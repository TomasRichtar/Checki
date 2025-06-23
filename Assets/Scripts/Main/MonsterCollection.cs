using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TastyCore.Utils;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization;

public class MonsterCollection : SingletonMonoBehaviour<MonsterCollection>
{
    //public List<Monster> AllMonsters = new List<Monster>();
    //public List<Monster> MyMonsters = new List<Monster>();
    //public List<Monster> LockedMonsters = new List<Monster>();

    [SerializeField] private Transform _monsterLayout;
    [SerializeField] private MonsterCollectionButton _monsterCollectionButton;


    private float _collected = 0;
    private float _locked = 0;
    [SerializeField] private LocalizedString _collectedLocalization;
    [SerializeField] private TextMeshProUGUI _collectedText;

    public event Action OnMonstersLoaded;
    public event Action OnNewMonsterUnlocked;

    private void OnEnable()
    {
        OnNewMonsterUnlocked += LoadMonsterColletion;
        _collectedLocalization.Arguments = new object[] { _collected, _locked };
        _collectedLocalization.StringChanged += UpdateCompleteText;
    }
    private void OnDisable()
    {
        OnNewMonsterUnlocked -= LoadMonsterColletion;
        _collectedLocalization.StringChanged -= UpdateCompleteText;
    }

    public void UpdateCompleteText(string value)
    {
        _collectedText.text = value;
    }

    private void Start()
    {
        UpdateMonsterData();
    }

    public void UpdateMonsterData()
    {
        SetAllMonsters();
        LoadMonsterColletion();
    }

    public void SetAllMonsters()
    {
        OnMonstersLoaded?.Invoke();
    }
   
    public void LoadMonsterColletion()
    {
        _collected = 0;
        _locked = 0;

        foreach (Transform item in _monsterLayout)
        {
            Destroy(item.gameObject);
        }

        foreach (var monster in MyGameManager.Instance.MonsterList)
        {
            MonsterCollectionButton monsterButton = Instantiate(_monsterCollectionButton, Vector3.zero, Quaternion.identity, _monsterLayout);
            monsterButton.CreateButton(monster, false);
            _collected++;
        }

        foreach (var monster in MyGameManager.Instance.AllMonsterList)
        {
            if (!MyGameManager.Instance.MonsterList.Contains(monster))
            {
                MonsterCollectionButton monsterButton = Instantiate(_monsterCollectionButton, Vector3.zero, Quaternion.identity, _monsterLayout);
                monsterButton.CreateButton(monster, true);
                _locked++;
            }
        }

        _collectedLocalization.Arguments[0] = _collected.ToString();
        _collectedLocalization.Arguments[1] = _locked.ToString();
        _collectedLocalization.RefreshString();
    }

    public bool CheckIfUnLocked(Monster monster)
    {
        if (MyGameManager.Instance.MonsterList.Contains(monster))
        {
            return true;
        }
        return false;
    }

    public void UnlockNewMonster(Monster monster)
    {
        if (CheckIfUnLocked(monster)) return;

        Children child = MyGameManager.Instance.ChildrenList.FirstOrDefault(x => x.Id == MyGameManager.Instance.ChildrenId);
        child.UnlockedMonsters += monster.Id + ";";

        ChildrenDatabase.Instance.UpdateData(child, (response) =>
        {
            //LockedMonsters.Remove(monster);
            //MyMonsters.Add(monster);
            MyGameManager.Instance.MonsterList.Add(monster);
            OnNewMonsterUnlocked?.Invoke();
        });
    }
}
