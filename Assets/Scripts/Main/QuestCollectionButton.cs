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

        _image.sprite = quest.Image;
        _nameText.text = quest.Name;
        _dateText.text = quest.ComplitionTime;
        switch (quest.QuestStatus)
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
        if (QuestCollection.Instance.CheckIfExists(_quest))
        {
            
        }
    }
}
