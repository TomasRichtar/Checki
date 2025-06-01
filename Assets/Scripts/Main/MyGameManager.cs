using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Richi;
using TastyCore.Utils;
using System;

public class MyGameManager : SingletonMonoBehaviour<MyGameManager>
{
    [Header("Data from databaze")]
    //TMP, this will be in the database
    public List<string> UnlockedMonstersList = new List<string>();
    public string SelectedMonster;

    public List<string> UnlockedEquipmentList = new List<string>();
    public string SelectedEquipmnet;

    public List<string> RewardsList = new List<string>();

    public List<string> QuestList = new List<string>();

    public List<string> ChildrenList = new List<string>();

    public string UserName;
    public int UserCredit;

    public event Action OnNewSelectedMonster;
    public event Action OnNewSelectedEquipment;
    public event Action OnDataLoaded;

    private void Start()
    {
        DontDestroyOnLoad();
        LoadAllData();
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

    public void LoadAllData()
    {
        OnDataLoaded?.Invoke();
    }
}
