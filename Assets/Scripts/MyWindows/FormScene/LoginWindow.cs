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
        LogIn.onClick.AddListener(() => ProfileManager.Instance.LogIn(_name.text, _password.text));
        PasswordReset.onClick.AddListener(() => Debug.Log("Password Reseted"));
        Register.onClick.AddListener(WindowController.Instance.PushWindow<RegistrationWindow>);
        Gmail.onClick.AddListener(() => Debug.Log("Gmail Login"));
        KidVersion.onClick.AddListener(() => SceneController.Instance.SwitchScene("GamePartScene"));
    }
    private void OnDisable()
    {
        try
        {
            LogIn.onClick.RemoveListener(() => ProfileManager.Instance.LogIn(_name.text, _password.text));
            PasswordReset.onClick.RemoveListener(() => Debug.Log("Password Reseted"));
            Register.onClick.RemoveListener(WindowController.Instance.PushWindow<RegistrationWindow>);
            Gmail.onClick.RemoveListener(() => Debug.Log("Gmail Login"));
            KidVersion.onClick.RemoveListener(() => SceneController.Instance.SwitchScene("GamePartScene"));
        }
        catch (System.Exception)
        {
            return;
        }
    }
}
