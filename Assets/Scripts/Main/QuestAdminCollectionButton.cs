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
        CloseValidation();
    }
    public void CreateButton(Quest quest)
    {
        _quest = quest;

        _nameText.text = quest.Title;
        _dateText.text = quest.ComplitionTime;
        QuestStatusEnum questStatus;
        if (!Enum.TryParse(quest.QuestStatus, true, out questStatus))
        {
            questStatus = QuestStatusEnum.None;
        }

        SwitchState(questStatus);
    }

    public void SwitchState(QuestStatusEnum questStatus)
    {
        _inProgress.SetActive(false);
        _completed.SetActive(false);
        _failed.SetActive(false);
        _pending.SetActive(false);

        switch (questStatus)
        {
            case QuestStatusEnum.None:
                _inProgress.SetActive(true);
                break;
            case QuestStatusEnum.InProgress:
                _inProgress.SetActive(true);
                break;
            case QuestStatusEnum.Completed:
                _completed.SetActive(true);
                break;
            case QuestStatusEnum.Pending:
                _pending.SetActive(true);
                break;
            case QuestStatusEnum.Failed:
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
            QuestManager.Instance.DeleteQuest(_quest);
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
            SwitchState(QuestStatusEnum.Completed);
            QuestManager.Instance.ValidateQuest(_quest, QuestStatusEnum.Completed);
            if (_quest.Repeatable == 1)
            {
                SwitchState(QuestStatusEnum.InProgress);
                QuestManager.Instance.ValidateQuest(_quest, QuestStatusEnum.InProgress);
            }
            _validation.SetActive(false);
        }
    }
    public void Decline()
    {
        if (QuestAdminCollection.Instance.CheckIfExists(_quest))
        {
            Debug.Log("Declined");
            SwitchState(QuestStatusEnum.Failed);
            QuestManager.Instance.ValidateQuest(_quest, QuestStatusEnum.Failed);
            _validation.SetActive(false);
        }
    }
    public void CloseValidation()
    {
        _validation.SetActive(false);
    }
}
