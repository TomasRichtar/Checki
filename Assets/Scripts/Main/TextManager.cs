using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TastyCore.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class TextManager : SingletonMonoBehaviour<TextManager>
{
    [SerializeField] private List<TextMeshProUGUI> _childNames = new List<TextMeshProUGUI>();
    [SerializeField] private List<Image> _childImage = new List<Image>();

    [SerializeField] private List<TextMeshProUGUI> _creditText = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> _moneyText = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> _bonusesText = new List<TextMeshProUGUI>();

    private float _credit = 0;
    private float _money = 0;
    private string _bonus = "0%";
    [SerializeField] private LocalizedString _collectedLocalization;
    [SerializeField] private LocalizedString _moneyLocalization;
    [SerializeField] private LocalizedString _bonusLocalization;

    private Children _child;
    private void OnEnable()
    {
        _collectedLocalization.Arguments = new object[] { _credit };
        _collectedLocalization.StringChanged += UpdateCompleteText;

        _moneyLocalization.Arguments = new object[] { _money };
        _moneyLocalization.StringChanged += UpdateMoneyText;

        _bonusLocalization.Arguments = new object[] { _bonus };
        _bonusLocalization.StringChanged += UpdateBonusText;
    }
    private void OnDisable()
    {
        _collectedLocalization.StringChanged -= UpdateCompleteText;
        _moneyLocalization.StringChanged -= UpdateMoneyText;
        _bonusLocalization.StringChanged -= UpdateBonusText;
    }
    public void UpdateCompleteText(string value)
    {
        foreach (var item in _creditText)
        {
            item.text = value;
        }
    }
    public void UpdateMoneyText(string value)
    {
        foreach (var item in _moneyText)
        {
            item.text = value;
        }
    }

    public void UpdateBonusText(string value)
    {
        foreach (var item in _bonusesText)
        {
            item.text = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _child = MyGameManager.Instance.ChildrenList.FirstOrDefault(x => x.Id == MyGameManager.Instance.ChildrenId);
        foreach (var item in _childNames)
        {
            item.text = _child.Name;
        }
        foreach (var item in _childImage)
        {
            item.sprite = SpriteManager.Instance.ProfileSprites[_child.ImageId];
        }
        UpdateCredit();
        UpdateMoney();
        UpdateBonus();
    }

    public void UpdateCredit()
    {
        _credit = _child.Credit;
        _collectedLocalization.Arguments[0] = _credit.ToString();
        _collectedLocalization.RefreshString();
    }
    public void UpdateMoney()
    {
        _money = _child.Money;
        _moneyLocalization.Arguments[0] = _money.ToString();
        _moneyLocalization.RefreshString();
    }
    public void UpdateBonus()
    {
        //_bonus = _child.Money;
        _moneyLocalization.Arguments[0] = _bonus;
        _moneyLocalization.RefreshString();
    }
}
