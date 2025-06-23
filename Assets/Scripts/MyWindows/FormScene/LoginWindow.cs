using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginWindow : BaseWindow
{
    [Header("Inputs")]
    [SerializeField] private TMP_InputField _name;
    [SerializeField] private TMP_InputField _password;

    [Header("Buttons")]
    public Button LogIn;
    public Button KidVersion;
    public Button PasswordReset;
    public Button Register;
    public Button Gmail;

    private void OnEnable()
    {
        LogIn.onClick.RemoveAllListeners();
        PasswordReset.onClick.RemoveAllListeners();
        Register.onClick.RemoveAllListeners();
        Gmail.onClick.RemoveAllListeners();
        KidVersion.onClick.RemoveAllListeners();

        LogIn.onClick.AddListener(() => ProfileManager.Instance.LogIn(_name.text, _password.text));
        PasswordReset.onClick.AddListener(OpenPasswordReset);
        Register.onClick.AddListener(WindowController.Instance.PushWindow<RegistrationWindow>);
        Gmail.onClick.AddListener(LoginWithGoogle.Instance.Login);
        KidVersion.onClick.AddListener(() => SceneController.Instance.ChildScene(true));

    }
    private void OnDisable()
    {
        try
        {
            LogIn.onClick.RemoveListener(() => ProfileManager.Instance.LogIn(_name.text, _password.text));
            PasswordReset.onClick.RemoveListener(OpenPasswordReset);
            Register.onClick.RemoveListener(WindowController.Instance.PushWindow<RegistrationWindow>);
            Gmail.onClick.RemoveListener(LoginWithGoogle.Instance.Login);
            KidVersion.onClick.RemoveListener(() => SceneController.Instance.ChildScene(true));
        }
        catch (System.Exception)
        {
            return;
        }
    }

    private void OpenPasswordReset()
    {
        WindowController.Instance.PushPopUpWindow(
               "AccountReset",
               "ContactUs",
               "Continue",
               null);
        //WindowController.Instance.PushWindow<AboutFamilyPartOneWindow>();
        //WindowController.Instance.ForceEnter<AboutFamilyMainWindow>();
        //WindowController.Instance.GetWindow<AboutFamilyPartTreeWindow>().IsResetingPassword = true;
    }
}
