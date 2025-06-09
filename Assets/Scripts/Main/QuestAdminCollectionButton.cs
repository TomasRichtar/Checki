using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestAdminCollectionButton : MonoBehaviour
{
    [Header("Button UI Elements")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _dateText;
    [SerializeField] private GameObject _validation;
    [Header("Status Variants")]
    [SerializeField] private GameObject _inProgress;
    [SerializeField] private GameObject _completed;
    [SerializeField] private GameObject _pending;
    [SerializeField] private GameObject _failed;

    private Quest _quest;
    private void OnEnable()
    {
        Debug.Log("Enable)");
        CloseValidation();
    }
    public void CreateButton(Quest quest)
    {
        _quest = quest;

        _nameText.text = quest.Title;
        _dateText.text = quest.ComplitionTime;
        QuestStatus questStatus;
        if (!Enum.TryParse(quest.QuestStatus, true, out questStatus))
        {
            questStatus = QuestStatus.None;
        }

        switch (questStatus)
        {
            case QuestStatus.None:
                _inProgress.SetActive(true);
                break;
            case QuestStatus.InProgress:
                _inProgress.SetActive(true);
                break;
            case QuestStatus.Completed:
                _completed.SetActive(true);
                break;
            case QuestStatus.Pending:
                _pending.SetActive(true);
                break;
            case QuestStatus.Failed:
                _failed.SetActive(true);
                break;
            default:
                _inProgress.SetActive(true);
                break;
        }
    }

    public void SelectThis()
    {
        if (QuestAdminCollection.Instance.CheckIfExists(_quest))
        {

        }
    }
    public void DeleteThis()
    {
        if (QuestAdminCollection.Instance.CheckIfExists(_quest))
        {
            Debug.Log("DELETE THIS");
        }
    }
    public void OpenValidation()
    {
        if (QuestAdminCollection.Instance.CheckIfExists(_quest))
        {
            Debug.Log("Validate THIS");
            _validation.SetActive(true);

        }
    }
    public void Accept()
    {
        if (QuestAdminCollection.Instance.CheckIfExists(_quest))
        {
            Debug.Log("Accepted");
            _validation.SetActive(false);
        }
    }
    public void Decline()
    {
        if (QuestAdminCollection.Instance.CheckIfExists(_quest))
        {
            Debug.Log("Declined");
            _validation.SetActive(false);
        }
    }
    public void CloseValidation()
    {
        _validation.SetActive(false);
    }
}
