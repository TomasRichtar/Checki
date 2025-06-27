using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreditExchangeWindow : BaseWindow
{
    [SerializeField] private List<int> _exchangeValueCredit;
    [SerializeField] private List<int> _exchangeValueMoney;
    [Header("Buttons")]
    public Button ExchangeButton1;
    public Button ExchangeButton2;

    public Button DoExchangeButton;


    private int exchangeSelected = 0;

    private void OnEnable()
    {
        ExchangeButton1.onClick.AddListener(Exchange1);
        ExchangeButton2.onClick.AddListener(Exchange2);
        DoExchangeButton.onClick.AddListener(DoExchange);
    }
    private void OnDisable()
    {
        ExchangeButton1.onClick.RemoveListener(Exchange1);
        ExchangeButton2.onClick.RemoveListener(Exchange2);
        DoExchangeButton.onClick.RemoveListener(DoExchange);
    }
    public void DoExchange()
    {
        Children child = MyGameManager.Instance.ChildrenList.FirstOrDefault(x => x.Id == MyGameManager.Instance.ChildrenId);
       
        child.Credit -= _exchangeValueCredit[exchangeSelected];
        child.Money += _exchangeValueMoney[exchangeSelected];

        ChildrenDatabase.Instance.UpdateData(child, (response) =>
        {
            TextManager.Instance.UpdateMoney();
            TextManager.Instance.UpdateCredit();
        });
    }
    public void Exchange1()
    {
        exchangeSelected = 1;
        ExchangeButton1.GetComponent<Image>().color = Color.white;
        ExchangeButton2.GetComponent<Image>().color = new Color32(138, 61, 151, 255);

        ExchangeButton2.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        ExchangeButton1.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(138, 61, 151, 255);
    }
    public void Exchange2()
    {
        exchangeSelected = 2;
        ExchangeButton2.GetComponent<Image>().color = Color.white;
        ExchangeButton1.GetComponent<Image>().color = new Color32(138, 61, 151, 255);

        ExchangeButton1.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        ExchangeButton2.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(138, 61, 151, 255);
    }
}
