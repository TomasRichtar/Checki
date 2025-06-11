using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordWindow : BaseWindow
{
    [Header("Compoments")]
    public GameObject Header;

    [Header("Buttons")]
   // public Button Code;
    public Button Login;

    private void OnEnable()
    {
        //Code.onClick.AddListener(WindowController.Instance.PushWindow<MainWindow>);
        Login.onClick.AddListener(() => SceneController.Instance.SwitchScene("FormScene"));
    }
    private void OnDisable()
    {
        try
        {
            //Code.onClick.RemoveListener(WindowController.Instance.PushWindow<MainWindow>);
            Login.onClick.RemoveListener(() => SceneController.Instance.SwitchScene("FormScene"));
        }
        catch (System.Exception)
        {
            return;
        }
    }
    protected override void PostPush()
    {
        base.PostPush();
        Header.SetActive(false);
    }
    protected override void PostPop()
    {
        base.PostPop();
        Header.SetActive(true);
    }
}
