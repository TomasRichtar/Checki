using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Richi;
using TMPro;
using UnityEngine.UI;

public class CreateQuestTwoWindow : BaseWindow
{
    [Header("Inputs")]
    [SerializeField] private int _childrenId;
    [SerializeField] private List<SelectableButton> _repeatableChoiseButtons;
    [SerializeField] private TMP_InputField _credit;
    [SerializeField] private TMP_InputField _complitionTime;
    [SerializeField] private List<SelectableButton> _selectedDaysButtons;

    [SerializeField] private TextMeshProUGUI _familyText;
    [SerializeField] private Image _familyImage;
    [SerializeField] private TextMeshProUGUI _interestsText;
    [SerializeField] private Image _interestsImage;


    [Header("Buttons")]
    public Button Next;
    public Button Previous;

    public void ChangeHeaderColorNext()
    {
        _familyImage.color = new Color(1.0f, 0.0f, 0.549f);
        _familyText.color = new Color(1.0f, 0.0f, 0.549f);
        _interestsImage.color = new Color(0.631f, 0.631f, 0.631f);
        _interestsText.color = new Color(0.631f, 0.631f, 0.631f);
    }

    public void ChangeHeaderColorPrevious()
    {
        _familyImage.color = new Color(1.0f, 0.0f, 0.549f);
        _familyText.color = new Color(1.0f, 0.0f, 0.549f);
        _interestsImage.color = new Color(0.631f, 0.631f, 0.631f);
        _interestsText.color = new Color(0.631f, 0.631f, 0.631f);
    }

    private void OnEnable()
    {
        Next.onClick.AddListener(() => _entryDirection = Direction.NONE);
        Next.onClick.AddListener(() => _exitDirection = Direction.NONE);
        Next.onClick.AddListener(() => _entryMode = PageEntryMode.NONE);
        Next.onClick.AddListener(() => _exitMode = PageEntryMode.NONE);

        Next.onClick.AddListener(() => ChangeHeaderColorNext());
        Previous.onClick.AddListener(() => ChangeHeaderColorPrevious());

        Previous.onClick.AddListener(() => _entryDirection = Direction.LEFT);
        Previous.onClick.AddListener(() => _exitDirection = Direction.RIGHT);

        Next.onClick.AddListener(CheckAndSendData);
        Previous.onClick.AddListener(WindowController.Instance.PushWindow<CreateQuestOneWindow>);

        //Next.onClick.AddListener(() => ProfileManager.Instance.AccountDataHobies(
        //   _traveling.IsSelected,
        //    _cooking.IsSelected,
        //    _music.IsSelected,
        //    _sport.IsSelected,
        //    _games.IsSelected,
        //    _relax.IsSelected,
        //    _art.IsSelected,
        //    _culture.IsSelected));
    }
    private void OnDisable()
    {
        Next.onClick.RemoveListener(() => _entryDirection = Direction.RIGHT);
        Next.onClick.RemoveListener(() => _exitDirection = Direction.LEFT);
        Next.onClick.RemoveListener(() => _entryMode = PageEntryMode.NONE);
        Next.onClick.RemoveListener(() => _exitMode = PageEntryMode.NONE);
        Previous.onClick.RemoveListener(() => _entryDirection = Direction.LEFT);
        Previous.onClick.RemoveListener(() => _exitDirection = Direction.RIGHT);

        Next.onClick.RemoveListener(CheckAndSendData);
        Previous.onClick.RemoveListener(WindowController.Instance.PushWindow<CreateQuestOneWindow>);

        //Next.onClick.RemoveListener(() => ProfileManager.Instance.AccountDataHobies(
        //   _traveling.IsSelected,
        //    _cooking.IsSelected,
        //    _music.IsSelected,
        //    _sport.IsSelected,
        //    _games.IsSelected,
        //    _relax.IsSelected,
        //    _art.IsSelected,
        //    _culture.IsSelected));
    }
    private void CheckAndSendData()
    {
        if (string.IsNullOrEmpty(_credit.text) ||
            string.IsNullOrEmpty(_complitionTime.text) ||
            _credit.text == "...")
        {
            PopUp();
            return;
        }

        SendData();
        QuestManager.Instance.AddQuest();
    }
    private void PopUp()
    {
        WindowController.Instance.PushPopUpWindow(
               "WrongDataTitle",
               "WrongData",
               "Continue",
               null);
    }

    private void SendData()
    {
        QuestManager.Instance.ChildrenId = ProfileManager.Instance.ChildrenData.Id;
        QuestManager.Instance.ProfileId = ProfileManager.Instance.ProfileData.Id;
        QuestManager.Instance.Credit = int.Parse(_credit.text);
        QuestManager.Instance.ComplitionTime = _complitionTime.text;
        QuestManager.Instance.QuestStatus = QuestStatusEnum.InProgress.ToString();

        QuestManager.Instance.Days = "";
        foreach (var item in _selectedDaysButtons)
        {
            if (item.IsSelected == 1)
            {
                QuestManager.Instance.Days += item.Data + ";";
            }
        }
        foreach (var item in _repeatableChoiseButtons)
        {
            if (item.IsSelected == 1)
            {
                QuestManager.Instance.Repeatable = 1;
                return;
            }
        }
    }
}
