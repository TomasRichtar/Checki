using System;
using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class QuestAdminCollection : SingletonMonoBehaviour<QuestAdminCollection>
{
    //public List<Quest> AllData = new List<Quest>();
    //public List<Quest> MyData = new List<Quest>();

    [SerializeField] private QuestAdminCollectionButton _collectionButton;

    [SerializeField] private Transform _layout;
    [SerializeField] private Transform _viewportContent;

    [Header("Profile")] 
    private float _completed = 0;
    private float _inCompleted = 0;
    [SerializeField] private LocalizedString _completeLocalization;
    [SerializeField] private LocalizedString _inCompleteLocalization;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _percentage;
    [SerializeField] private TextMeshProUGUI _complete;
    [SerializeField] private TextMeshProUGUI _inComplete;

    public event Action OnDataLoaded;
    public event Action OnDataUpdate;

    private void OnEnable()
    {
        OnDataUpdate += LoadColletionLayout;
        _completeLocalization.Arguments = new object[] { _complete };
        _completeLocalization.StringChanged += UpdateCompleteText;
        _inCompleteLocalization.Arguments = new object[] { _inComplete };
        _inCompleteLocalization.StringChanged += UpdateInCompleteText;
    }
    private void OnDisable()
    {
        OnDataUpdate -= LoadColletionLayout;
        _completeLocalization.StringChanged -= UpdateCompleteText;
        _inCompleteLocalization.StringChanged -= UpdateInCompleteText;
    }
    public void UpdateCompleteText(string value)
    {
        _complete.text = value;
    }
    public void UpdateInCompleteText(string value)
    {
        _inComplete.text = value;
    }

    public void UpdateData()
    {
        SetAllData();
        LoadColletionLayout();
        LoadCollectionSlider();
    }

    public void SetAllData()
    {
        
        OnDataLoaded?.Invoke();
    }

    public void LoadCollectionSlider()
    {
        _completed = 0;
        _inCompleted = 0;
        float totalValue = MyGameManager.Instance.QuestList.Count;

        foreach (var item in MyGameManager.Instance.QuestList)
        {
            if (item.QuestStatus == QuestStatusEnum.Completed.ToString())
            {
                _completed++;
            }
            else
            {
                _inCompleted++;
            }
        }
        _slider.value = _completed;
        _completeLocalization.Arguments[0] = _completed.ToString();
        _completeLocalization.RefreshString();
        _inCompleteLocalization.Arguments[0] = _inCompleted.ToString();
        _inCompleteLocalization.RefreshString();
        if (totalValue != 0)
        {
            _slider.maxValue = totalValue;
            _percentage.text = (_completed / totalValue)  * 100 + "%";
        }
    }

    public void LoadColletionLayout()
    {
        float viewportContentHeigh = 0;
        float gapHeight = 0;

        foreach (Transform item in _layout)
        {
            Destroy(item.gameObject);
        }

        if (MyGameManager.Instance.QuestList.Count > 0)
        {
            foreach (var item in MyGameManager.Instance.QuestList)
            {
                QuestAdminCollectionButton buttonCustom = Instantiate(_collectionButton, Vector3.zero, Quaternion.identity, _layout);
                buttonCustom.CreateButton(item);
                viewportContentHeigh += 142;
                gapHeight += 32;
            }
        }

        viewportContentHeigh += gapHeight;

        RectTransform rt = _viewportContent.GetComponent<RectTransform>();
        Vector2 size = rt.sizeDelta;
        size.y = viewportContentHeigh;
        rt.sizeDelta = size;
    }

    public bool CheckIfExists(Quest data)
    {
        if (MyGameManager.Instance.QuestList.Contains(data))
        {
            return true;
        }
        return false;
    }

    public void AddNewQuest(int profileId)
    {
        CreateQuestDatabase.Instance.GetQuestData(profileId, (Quest) =>
        {
            MyGameManager.Instance.QuestList = Quest;
            WindowController.Instance.ForceExit<CreateQuestMainWindow>();
            WindowController.Instance.PushWindow<QuestSettingsWindow>();
            OnDataUpdate?.Invoke();
        });
    }

    public void DeleteQuest(Quest quest)
    {
        MyGameManager.Instance.QuestList.Remove(quest);
        OnDataUpdate?.Invoke();
    }
}
