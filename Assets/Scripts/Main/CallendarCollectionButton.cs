using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CallendarCollectionButton : MonoBehaviour
{
    [Header("Button UI Elements")]
    [SerializeField] private TextMeshProUGUI _day;
    [SerializeField] private TextMeshProUGUI _month;
    [SerializeField] private TextMeshProUGUI _year;
    [SerializeField] private TextMeshProUGUI _dayNumber;
    [SerializeField] private TextMeshProUGUI _questsNumber;

    private CallendarDay _callendarDay;

    public void CreateButton(CallendarDay callendarDay)
    {
        _callendarDay = callendarDay;

        _day.text = _callendarDay.Day;
        _month.text = _callendarDay.Month;
        _year.text = _callendarDay.Year;
        _dayNumber.text = _callendarDay.DayNumber.ToString();
        int questCount = 0;
        foreach (var item in MyGameManager.Instance.QuestList)
        {
            if (item.Days.Contains(_callendarDay.DayKey)) 
            {
                questCount++;
            }
        }
        _questsNumber.text = questCount.ToString();
    }

    public void SelectThis()
    {
        OpenThisDay();
    }
    public void OpenThisDay()
    {
        Debug.Log("Open");
    }
}
