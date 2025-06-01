using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Richi;
using TMPro;

public class AboutFamilyPartOneWindow : BaseWindow
{
    [Header("Inputs")]
    [SerializeField] private TMP_InputField _name;
    [SerializeField] private TMP_InputField _nickName;
    [SerializeField] private TMP_Dropdown _familyMemberNumber;
    [SerializeField] private TMP_Dropdown _childrenNumber;

    [SerializeField] private TextMeshProUGUI _familyText;
    [SerializeField] private Image _familyImage;
    [SerializeField] private TextMeshProUGUI _petsText;
    [SerializeField] private Image _petsImage;
    [SerializeField] private TextMeshProUGUI _interestsText;
    [SerializeField] private Image _interestsImage;

    [Header("Buttons")]
    public Button Next;

    public void ChangeHeaderColorNext()
    {
        _familyImage.color = new Color(0.631f, 0.631f, 0.631f);
        _familyText.color = new Color(0.631f, 0.631f, 0.631f);
        _petsImage.color = new Color(1.0f, 0.0f, 0.549f);
        _petsText.color = new Color(1.0f, 0.0f, 0.549f);
        _interestsImage.color = new Color(0.631f, 0.631f, 0.631f);
        _interestsText.color = new Color(0.631f, 0.631f, 0.631f);
    }

    private void OnEnable()
    {
        Next.onClick.AddListener(() => _entryDirection = Direction.RIGHT);
        Next.onClick.AddListener(() => _exitDirection = Direction.LEFT);
        Next.onClick.AddListener(() => _entryMode = PageEntryMode.SLIDE);
        Next.onClick.AddListener(() => _exitMode = PageEntryMode.SLIDE);

        Next.onClick.AddListener(WindowController.Instance.PushWindow<AboutFamilyPartTwoWindow>);
        Next.onClick.AddListener(() => ChangeHeaderColorNext());


        Next.onClick.AddListener(() => ProfileManager.Instance.AccountDataBase(
            _name.text,
            _nickName.text,
            _familyMemberNumber.options[_familyMemberNumber.value].text,
            _childrenNumber.options[_childrenNumber.value].text));
    }
    private void OnDisable()
    {
        Next.onClick.RemoveListener(() => _entryDirection = Direction.RIGHT);
        Next.onClick.RemoveListener(() => _exitDirection = Direction.LEFT);
        Next.onClick.RemoveListener(() => _entryMode = PageEntryMode.SLIDE);
        Next.onClick.RemoveListener(() => _exitMode = PageEntryMode.SLIDE);

        Next.onClick.RemoveListener(WindowController.Instance.PushWindow<AboutFamilyPartTwoWindow>);

        Next.onClick.RemoveListener(() => ProfileManager.Instance.Register(
            _name.text,
            _nickName.text,
            _familyMemberNumber.options[_familyMemberNumber.value].text,
            _childrenNumber.options[_childrenNumber.value].text));
    }

    
}
