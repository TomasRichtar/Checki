using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestCollectionButton : MonoBehaviour
{
    [Header("Button UI Elements")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _dateText;
    [SerializeField] private Image _image;
    [Header("Status Variants")]
    [SerializeField] private GameObject _inProgress;
    [SerializeField] private GameObject _completed;
    [SerializeField] private GameObject _pending;
    [SerializeField] private GameObject _failed;

    private Quest _quest;

    public void CreateButton(Quest quest)
    {
        _quest = quest;

        _image.sprite = SpriteManager.Instance.QuestSprites[quest.ImageId];
        _nameText.text = quest.Title;
        _dateText.text = quest.ComplitionTime;
        QuestStatusEnum questStatus;
        if (!Enum.TryParse(quest.QuestStatus, true, out questStatus))
        {
            questStatus = QuestStatusEnum.None;
        }

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
        if (QuestCollection.Instance.CheckIfExists(_quest))
        {
            QuestManager.Instance.CompleteQuest(_quest);
        }
    }
}
