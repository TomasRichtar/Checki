using Richi;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AboutFamilyPartTwoWindow : BaseWindow
{

    [Header("Inputs")]
    [SerializeField] private TMP_InputField _dogs;
    [SerializeField] private TMP_InputField _cats;
    [SerializeField] private TMP_InputField _fish;
    [SerializeField] private TMP_InputField _other;

    [SerializeField] private TextMeshProUGUI _familyText;
    [SerializeField] private Image _familyImage;
    [SerializeField] private TextMeshProUGUI _petsText;
    [SerializeField] private Image _petsImage;
    [SerializeField] private TextMeshProUGUI _interestsText;
    [SerializeField] private Image _interestsImage;


    [Header("Buttons")]
    public Button Next;
    public Button Previous;

    public void ChangeHeaderColorNext()
    {
        _familyImage.color = new Color(0.631f, 0.631f, 0.631f);
        _familyText.color = new Color(0.631f, 0.631f, 0.631f);
        _petsImage.color = new Color(0.631f, 0.631f, 0.631f);
        _petsText.color = new Color(0.631f, 0.631f, 0.631f);
        _interestsImage.color = new Color(1.0f, 0.0f, 0.549f);
        _interestsText.color = new Color(1.0f, 0.0f, 0.549f);
    }

    public void ChangeHeaderColorPrevious()
    {
        _familyImage.color = new Color(1.0f, 0.0f, 0.549f);
        _familyText.color = new Color(1.0f, 0.0f, 0.549f);
        _petsImage.color = new Color(0.631f, 0.631f, 0.631f);
        _petsText.color = new Color(0.631f, 0.631f, 0.631f);
        _interestsImage.color = new Color(0.631f, 0.631f, 0.631f);
        _interestsText.color = new Color(0.631f, 0.631f, 0.631f);
    }

    private void OnEnable()
    {
        Next.onClick.AddListener(() => _entryDirection = Direction.RIGHT);
        Next.onClick.AddListener(() => _exitDirection = Direction.LEFT);
        Previous.onClick.AddListener(() => _entryDirection = Direction.LEFT);
        Previous.onClick.AddListener(() => _exitDirection = Direction.RIGHT);

        Next.onClick.AddListener(() => ChangeHeaderColorNext());
        Previous.onClick.AddListener(() => ChangeHeaderColorPrevious());

        Next.onClick.AddListener(WindowController.Instance.PushWindow<AboutFamilyPartTreeWindow>);
        Next.onClick.AddListener(WindowController.Instance.GetWindow<AboutFamilyPartTreeWindow>().FillUpData);
        Previous.onClick.AddListener(WindowController.Instance.PushWindow<AboutFamilyPartOneWindow>);

        Next.onClick.AddListener(() => ProfileManager.Instance.AccountDataPets(
            _dogs.text,
            _cats.text,
            _fish.text,
            _other.text));
    }
    private void OnDisable()
    {
        Next.onClick.RemoveListener(() => _entryDirection = Direction.RIGHT);
        Next.onClick.RemoveListener(() => _exitDirection = Direction.LEFT);
        Previous.onClick.RemoveListener(() => _entryDirection = Direction.LEFT);
        Previous.onClick.RemoveListener(() => _exitDirection = Direction.RIGHT);

        Next.onClick.RemoveListener(WindowController.Instance.PushWindow<AboutFamilyPartTreeWindow>);
        Next.onClick.RemoveListener(WindowController.Instance.GetWindow<AboutFamilyPartTreeWindow>().FillUpData);
        Previous.onClick.RemoveListener(WindowController.Instance.PushWindow<AboutFamilyPartOneWindow>);

        Next.onClick.RemoveListener(() => ProfileManager.Instance.AccountDataPets(
            _dogs.text,
            _cats.text,
            _fish.text,
            _other.text));
    }
    public void FillUpData()
    {
        _dogs.text = ProfileManager.Instance.ProfileData.Dogs.ToString();
        _cats.text = ProfileManager.Instance.ProfileData.Cats.ToString();
        _fish.text = ProfileManager.Instance.ProfileData.Fish.ToString();
        _other.text = ProfileManager.Instance.ProfileData.Other.ToString();
    }
}
