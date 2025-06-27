using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Richi;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class CreateRewardOneWindow : BaseWindow
{
    [Header("Inputs")]
    [SerializeField] private int _childrenId;
    [SerializeField] private TMP_InputField _title;
    [SerializeField] private TMP_InputField _price;
    [SerializeField] private List<SelectableButton> _selectedImageButtons;

    //[SerializeField] private TextMeshProUGUI _familyText;
    //[SerializeField] private Image _familyImage;
    //[SerializeField] private TextMeshProUGUI _interestsText;
    //[SerializeField] private Image _interestsImage;

    [Header("Buttons")]
    public Button Next;
    public Button Previous;

    public void ChangeHeaderColorNext()
    {
        //_familyImage.color = new Color(1.0f, 0.0f, 0.549f);
        //_familyText.color = new Color(1.0f, 0.0f, 0.549f);
        //_interestsImage.color = new Color(0.631f, 0.631f, 0.631f);
        //_interestsText.color = new Color(0.631f, 0.631f, 0.631f);
    }

    public void ChangeHeaderColorPrevious()
    {
        //_familyImage.color = new Color(1.0f, 0.0f, 0.549f);
        //_familyText.color = new Color(1.0f, 0.0f, 0.549f);
        //_interestsImage.color = new Color(0.631f, 0.631f, 0.631f);
        //_interestsText.color = new Color(0.631f, 0.631f, 0.631f);
    }

    private void OnEnable()
    {
        Next.onClick.AddListener(() => _entryDirection = Direction.NONE);
        Next.onClick.AddListener(() => _exitDirection = Direction.NONE);
        Next.onClick.AddListener(() => _entryMode = PageEntryMode.NONE);
        Next.onClick.AddListener(() => _exitMode = PageEntryMode.NONE);

        Previous.onClick.AddListener(() => ChangeHeaderColorPrevious());

        Previous.onClick.AddListener(() => _entryDirection = Direction.LEFT);
        Previous.onClick.AddListener(() => _exitDirection = Direction.RIGHT);

        Next.onClick.AddListener(CheckAndSendData);
        Previous.onClick.AddListener(WindowController.Instance.PushWindow<RewardsSettingsWindow>);
        Previous.onClick.AddListener(WindowController.Instance.ForceExit<CreateRewardMainWindow>);
    }
    private void OnDisable()
    {
        Next.onClick.RemoveListener(() => _entryDirection = Direction.RIGHT);
        Next.onClick.RemoveListener(() => _exitDirection = Direction.LEFT);
        Next.onClick.RemoveListener(() => _entryMode = PageEntryMode.NONE);
        Next.onClick.RemoveListener(() => _exitMode = PageEntryMode.NONE);
        Previous.onClick.RemoveListener(() => _entryDirection = Direction.LEFT);
        Previous.onClick.RemoveListener(() => _exitDirection = Direction.RIGHT);

        Next.onClick.RemoveListener(() => ChangeHeaderColorNext());
        Previous.onClick.RemoveListener(() => ChangeHeaderColorPrevious());

        Next.onClick.RemoveListener(CheckAndSendData);
        Previous.onClick.RemoveListener(WindowController.Instance.PushWindow<RewardsSettingsWindow>);
        Previous.onClick.RemoveListener(WindowController.Instance.ForceExit<CreateRewardMainWindow>);
    }

    private void CheckAndSendData()
    {
        if (string.IsNullOrEmpty(_title.text) ||
            string.IsNullOrEmpty(_price.text) ||
            _title.text == "..." ||
            _price.text == "...")
        {
            PopUp();
            return;
        }

        SendData();
        ChangeHeaderColorNext();
        RewardManager.Instance.AddReward();
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
        RewardManager.Instance.ChildrenId = ProfileManager.Instance.ChildrenData.Id;
        RewardManager.Instance.ProfileId = ProfileManager.Instance.ProfileData.Id;
        RewardManager.Instance.Price = int.Parse(_price.text);
        RewardManager.Instance.Title = _title.text;

        foreach (var item in _selectedImageButtons)
        {
            if (item.IsSelected == 1)
            {
                RewardManager.Instance.ImageId += item.ImageId;
                return;
            }
        }
    }
}
