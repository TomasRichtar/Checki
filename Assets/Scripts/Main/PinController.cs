using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PinController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pinDisplay;
    private string correctPin = "12345";
    private string currentPin = "";

    private void Start()
    {
        pinDisplay.text = "";
        correctPin = MyGameManager.Instance.ChildrenList.FirstOrDefault(x => x.Id == MyGameManager.Instance.ChildrenId).Password.ToString();
    }
    public void MyInput(string x)
    {
        if (currentPin.Length < 5)
        {
            currentPin += x;
            pinDisplay.text += "*";

            if (currentPin.Length == 5)
            {
                CheckPin();
            }
        }
    }

    private void CheckPin()
    {
        if (currentPin == correctPin)
        {
            Login();
        }
        else
        {
            ResetPin();
        }
    }

    private void Login()
    {
        ResetPin();
        WindowController.Instance.PushWindow<MainWindow>();
    }

    private void ResetPin()
    {
        currentPin = "";
        pinDisplay.text = currentPin;
    }
}
