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
        _month.text = _callendarDay.Month + " " + _callendarDay.Year;
        _dayNumber.text = _callendarDay.DayNumber.ToString();
        _questsNumber.text = _callendarDay.Quests.Count.ToString();
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
