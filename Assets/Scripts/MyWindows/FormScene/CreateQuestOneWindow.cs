using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Richi;

public class CreateQuestOneWindow : BaseWindow
{
    [Header("Inputs")]
    [SerializeField] private TMP_InputField _title;

    [SerializeField] private List<SelectableButton> _selectablebuttons = new List<SelectableButton>();

    [SerializeField] private TextMeshProUGUI _familyText;
    [SerializeField] private Image _familyImage;
    [SerializeField] private TextMeshProUGUI _interestsText;
    [SerializeField] private Image _interestsImage;


    [Header("Buttons")]
    public Button Next;
    public Button Previous;

    public void ChangeHeaderColorNext()
    {
        _familyImage.color = new Color(0.631f, 0.631f, 0.631f);
        _familyText.color = new Color(0.631f, 0.631f, 0.631f);
        _interestsImage.color = new Color(1.0f, 0.0f, 0.549f);
        _interestsText.color = new Color(1.0f, 0.0f, 0.549f);
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
        Next.onClick.AddListener(() => _entryDirection = Direction.RIGHT);
        Next.onClick.AddListener(() => _exitDirection = Direction.LEFT);
        Next.onClick.AddListener(() => _entryMode = PageEntryMode.SLIDE);
        Next.onClick.AddListener(() => _exitMode = PageEntryMode.SLIDE);

        Next.onClick.AddListener(() => ChangeHeaderColorNext());
        Previous.onClick.AddListener(() => ChangeHeaderColorPrevious());

        Previous.onClick.AddListener(WindowController.Instance.ForceExit<CreateQuestMainWindow>);
        Previous.onClick.AddListener(WindowController.Instance.PushWindow<QuestSettingsWindow>);
        Next.onClick.AddListener(WindowController.Instance.PushWindow<CreateQuestTwoWindow>);
        Next.onClick.AddListener(SendData);


        //Next.onClick.AddListener(() => ProfileManager.Instance.AccountChildRegister(
        //    _name.text,
        //    _nickName.text,
        //    _age.options[_age.value].text));
    }
    private void OnDisable()
    {
        Next.onClick.RemoveListener(() => _entryDirection = Direction.RIGHT);
        Next.onClick.RemoveListener(() => _exitDirection = Direction.LEFT);
        Next.onClick.RemoveListener(() => _entryMode = PageEntryMode.SLIDE);
        Next.onClick.RemoveListener(() => _exitMode = PageEntryMode.SLIDE);

        Previous.onClick.RemoveListener(WindowController.Instance.ForceExit<CreateQuestMainWindow>);
        Previous.onClick.RemoveListener(WindowController.Instance.PushWindow<QuestSettingsWindow>);
        Next.onClick.RemoveListener(WindowController.Instance.PushWindow<CreateQuestTwoWindow>);
        Next.onClick.RemoveListener(SendData);

        //Next.onClick.RemoveListener(() => ProfileManager.Instance.AccountChildRegister(
        //    _name.text,
        //    _nickName.text,
        //    _age.options[_age.value].text));
    }

    private void SendData()
    {
        QuestManager.Instance.Title = _title.text;
        foreach (var item in _selectablebuttons)
        {
            if (item.IsSelected == 1)
            {
                QuestManager.Instance.ImageId = item.ImageId;
                return;
            }
        }
    }
}
