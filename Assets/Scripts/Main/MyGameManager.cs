using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Richi;
using TastyCore.Utils;
using System;
using System.Linq;

public class MyGameManager : SingletonMonoBehaviour<MyGameManager>
{
    public int ProfileId;
    public int ChildrenId;
    [Header("Data from real databaze")]
    public List<Children> ChildrenList = new List<Children>();
    public List<Quest> QuestList = new List<Quest>();
    public List<Reward> RewardList = new List<Reward>();
    public List<Monster> MonsterList = new List<Monster>();
    public List<Equipment> EquipmentList = new List<Equipment>();

    [Header("Data from game")]
    public List<Monster> AllMonsterList = new List<Monster>();
    public List<Equipment> AllEquipmentList = new List<Equipment>();

    public string UserName;
    public int UserCredit;


    public event Action OnNewSelectedMonster;
    public event Action OnNewSelectedEquipment;

    private void Start()
    {
        DontDestroyOnLoad();
        if (ProfileId == 0)
        {
            SceneController.Instance.SwitchScene("FormScene");
        }
        else
        {
            //LoadAllData();
        }
    }

    public void LoadAllData(Action onComplete)
    {
        int databaseLoaded = 0;
        int databaseCount = 3;

        void CheckCompleted()
        {
            databaseLoaded++;
            if (databaseCount >= databaseLoaded)
            {
                LoadMonsterData();
                onComplete?.Invoke();
            }
        }

        ChildrenDatabase.Instance.GetChildrenData(ProfileId, (Children) =>
        {
            ChildrenList = Children;
            CheckCompleted();
        });
        CreateQuestDatabase.Instance.GetQuestData(ChildrenId, (Quest) =>
        {
            QuestList = Quest;
            CheckCompleted();
        });
        CreateRewardDatabase.Instance.GetRewardData(ChildrenId, (Reward) =>
        {
            RewardList = Reward;
            CheckCompleted();
        });
    }

    public void LoadChildData(Action onComplete)
    {
        int databaseLoaded = 0;
        int databaseCount = 2;

        void CheckCompleted()
        {
            databaseLoaded++;
            if (databaseCount >= databaseLoaded)
            {
                onComplete?.Invoke();
            }
        }

        CreateQuestDatabase.Instance.GetQuestData(ChildrenId, (Quest) =>
        {
            QuestList = Quest;
            CheckCompleted();
        });
        CreateRewardDatabase.Instance.GetRewardData(ChildrenId, (Reward) =>
        {
            RewardList = Reward;
            CheckCompleted();
        });
    }

    public void LoadMonsterData()
    {
        Children selectedChild = ChildrenList.FirstOrDefault(c => c.Id == ChildrenId);
        if (selectedChild == null)
        {
            Debug.LogWarning("Children not found with ID: " + ChildrenId);
            return;
        }

        List<int> unlockedMonsterIds = selectedChild.UnlockedMonsters
            .Split(';', StringSplitOptions.RemoveEmptyEntries)
            .Select(id => int.Parse(id))
            .ToList();

        List<int> unlockedEquipmentIds = selectedChild.UnlockedEquipment
            .Split(';', StringSplitOptions.RemoveEmptyEntries)
            .Select(id => int.Parse(id))
            .ToList();

        MonsterList = AllMonsterList
            .Where(monster => unlockedMonsterIds.Contains(monster.Id))
            .ToList();

        EquipmentList = AllEquipmentList
            .Where(equipment => unlockedEquipmentIds.Contains(equipment.Id))
            .ToList();
    }
}
