using Richi;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddChildrenPartTwoWindow : BaseWindow
{
    [Header("Inputs")]
    [SerializeField] private SelectableButton _traveling;
    [SerializeField] private SelectableButton _cooking;
    [SerializeField] private SelectableButton _music;
    [SerializeField] private SelectableButton _sport;
    [SerializeField] private SelectableButton _games;
    [SerializeField] private SelectableButton _relax;
    [SerializeField] private SelectableButton _art;
    [SerializeField] private SelectableButton _culture;

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

        Next.onClick.AddListener(ProfileManager.Instance.AddChildren);
        Previous.onClick.AddListener(WindowController.Instance.PushWindow<AddChildrenPartOneWindow>);

        Next.onClick.AddListener(() => ProfileManager.Instance.AccountDataHobies(
           _traveling.IsSelected,
            _cooking.IsSelected,
            _music.IsSelected,
            _sport.IsSelected,
            _games.IsSelected,
            _relax.IsSelected,
            _art.IsSelected,
            _culture.IsSelected));
    }
    private void OnDisable()
    {
        Next.onClick.RemoveListener(() => _entryDirection = Direction.RIGHT);
        Next.onClick.RemoveListener(() => _exitDirection = Direction.LEFT);
        Next.onClick.RemoveListener(() => _entryMode = PageEntryMode.NONE);
        Next.onClick.RemoveListener(() => _exitMode = PageEntryMode.NONE);
        Previous.onClick.RemoveListener(() => _entryDirection = Direction.LEFT);
        Previous.onClick.RemoveListener(() => _exitDirection = Direction.RIGHT);

        Next.onClick.RemoveListener(ProfileManager.Instance.AddChildren);
        Previous.onClick.RemoveListener(WindowController.Instance.PushWindow<AddChildrenPartOneWindow>);

        Next.onClick.RemoveListener(() => ProfileManager.Instance.AccountDataHobies(
           _traveling.IsSelected,
            _cooking.IsSelected,
            _music.IsSelected,
            _sport.IsSelected,
            _games.IsSelected,
            _relax.IsSelected,
            _art.IsSelected,
            _culture.IsSelected));
    }
}
