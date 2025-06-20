using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TastyCore.Utils;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CallendarCollection : SingletonMonoBehaviour<CallendarCollection>
{
    public List<CallendarDay> MyData = new List<CallendarDay>();

    [SerializeField] private CallendarCollectionButton _collectionButton;

    [Header("Custom")]
    [SerializeField] private Transform _layout;
    [SerializeField] private Transform _viewportContent;

    public event Action OnDataLoaded;
    public event Action OnDataUpdate;


    private void OnEnable()
    {
        OnDataUpdate += LoadColletionLayout;
    }
    private void OnDisable()
    {
        OnDataUpdate -= LoadColletionLayout;
    }

    public void UpdateData()
    {
        SetAllData();
        LoadColletionLayout();
    }
    public void SetAllData()
    {
        MyData.Clear();
        StartCoroutine(LoadLocalizedDataCoroutine());
    }

    private IEnumerator LoadLocalizedDataCoroutine()
    {
        var task = LoadLocalizedDataAsync();
        yield return new WaitUntil(() => task.IsCompleted);
    }

    private async Task LoadLocalizedDataAsync()
    {
        for (int i = 0; i <= 6; i++)
        {
            DateTime day = DateTime.Today.AddDays(i);
            Days myDays = (Days)(int)day.DayOfWeek;
            Months myMonth = (Months)(day.Month - 1);
            
            string dayKey = $"{myDays}";     // e.g., "Monday"
            string monthKey = $"{myMonth}";  // e.g., "January"

            var dayRequest = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("sport", dayKey);
            var monthRequest = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("sport", monthKey);

            await Task.WhenAll(dayRequest.Task, monthRequest.Task);

            if (dayRequest.Status != AsyncOperationStatus.Succeeded || monthRequest.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"Localization failed for: {dayKey} or {monthKey}");
                continue;
            }

            string localizedDay = dayRequest.Result;
            string localizedMonth = monthRequest.Result;

            CallendarDay callendarDay = new CallendarDay
            {
                Day = localizedDay,
                DayKey = dayKey,
                Month = localizedMonth,
                Year = day.Year.ToString(),
                DayNumber = day.Day,
                DayEnum = myDays
            };

            MyData.Add(callendarDay);
        }

        OnDataUpdate?.Invoke();
    }

    public void LoadColletionLayout()
    {
        float viewportContentHeigh = 0;
        float gapHeight = 0;

        foreach (Transform item in _layout)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in MyData)
        {
            CallendarCollectionButton buttonCustom = Instantiate(_collectionButton, Vector3.zero, Quaternion.identity, _layout);
            buttonCustom.CreateButton(item);
            viewportContentHeigh += 182;
            gapHeight += 34;
        }

        viewportContentHeigh += gapHeight;

        RectTransform rt = _viewportContent.GetComponent<RectTransform>();
        Vector2 size = rt.sizeDelta;
        size.y = viewportContentHeigh;
        rt.sizeDelta = size;
    }

    public bool CheckIfExists()
    {
        return true;
    }
}
