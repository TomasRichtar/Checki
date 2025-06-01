using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TastyCore.Utils;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterCollection : SingletonMonoBehaviour<MonsterCollection>
{
    public List<Monster> AllMonsters = new List<Monster>();
    public List<Monster> MyMonsters = new List<Monster>();
    public List<Monster> LockedMonsters = new List<Monster>();

    [SerializeField] private Transform _monsterLayout;
    [SerializeField] private MonsterCollectionButton _monsterCollectionButton;

    public event Action OnMonstersLoaded;
    public event Action OnNewMonsterUnlocked;


    private void OnEnable()
    {
        OnNewMonsterUnlocked += LoadMonsterColletion;
    }
    private void OnDisable()
    {
        OnNewMonsterUnlocked -= LoadMonsterColletion;
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
        MyMonsters.Clear();
        LockedMonsters.Clear();

        var unlockedSet = new HashSet<string>(MyGameManager.Instance.UnlockedMonstersList);

        foreach (var monster in AllMonsters)
        {
            if (unlockedSet.Contains(monster.Name))
            {
                MyMonsters.Add(monster);
            }
            else
            {
                LockedMonsters.Add(monster);
            }
        }

        foreach (var monster in MyMonsters)
        {
            if (monster.Name == MyGameManager.Instance.SelectedMonster)
            {
                MyMonster.Instance.SetSelectedMonster(monster);
            }
            else
            {
                Debug.Log("This Monster is not unlocked: " + MyGameManager.Instance.SelectedMonster);
            }
        }

        
        OnMonstersLoaded?.Invoke();
    }
   
    public void LoadMonsterColletion()
    {
        foreach (Transform item in _monsterLayout)
        {
            Destroy(item.gameObject);
        }

        foreach (var monster in MyMonsters)
        {
            MonsterCollectionButton monsterButton = Instantiate(_monsterCollectionButton, Vector3.zero, Quaternion.identity, _monsterLayout);
            monsterButton.CreateButton(monster, false);
        }

        foreach (var monster in LockedMonsters)
        {
            MonsterCollectionButton monsterButton = Instantiate(_monsterCollectionButton, Vector3.zero, Quaternion.identity, _monsterLayout);
            monsterButton.CreateButton(monster, true);
        }
    }

    public bool CheckIfUnLocked(Monster monster)
    {
        if (MyMonsters.Contains(monster))
        {
            return true;
        }
        return false;
    }

    public void UnlockNewMonster(Monster monster)
    {
        if (CheckIfUnLocked(monster)) return;

        LockedMonsters.Remove(monster);
        MyMonsters.Add(monster);

        MyGameManager.Instance.UnlockedMonstersList.Add(monster.Name);
        OnNewMonsterUnlocked?.Invoke();
    }
}
