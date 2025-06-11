using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Richi;
using TastyCore.Utils;
using System;

public class MyGameManager : SingletonMonoBehaviour<MyGameManager>
{
    public int ProfileId;
    public int ChildrenId;
    [Header("Data from real databaze")]
    public List<Children> ChildrenList = new List<Children>();
    public List<Quest> QuestList = new List<Quest>();
    public List<Reward> RewardList = new List<Reward>();

    [Header("Data from game")]
    //TMP, this will be in the database
    public List<string> UnlockedMonstersList = new List<string>();
    public string SelectedMonster;

    public List<string> UnlockedEquipmentList = new List<string>();
    public string SelectedEquipmnet;


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

    public void SelectNewMonster(string name)
    {
        SelectedMonster = name;
        OnNewSelectedMonster?.Invoke();
    }
    public void SelectNewEquipment(string name)
    {
        SelectedEquipmnet = name;
        OnNewSelectedEquipment?.Invoke();
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
}
