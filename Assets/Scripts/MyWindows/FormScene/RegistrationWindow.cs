using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegistrationWindow : BaseWindow
{
    [Header("Inputs")]
    [SerializeField] private TMP_InputField _name;
    [SerializeField] private TMP_InputField _email;
    [SerializeField] private TMP_InputField _mobile;
    [SerializeField] private TMP_InputField _password;

    [Header("Buttons")]
    public Button Register;
    public Button LogIn;
    public Button Gmail;

    private void OnEnable()
    {
        Register.onClick.AddListener(() => ProfileManager.Instance.Register(_name.text, _email.text, _mobile.text, _password.text));
        LogIn.onClick.AddListener(WindowController.Instance.PushWindow<LoginWindow>);
        Gmail.onClick.AddListener(() => Debug.Log("Gmail Login"));
    }
    private void OnDisable()
    {
        Register.onClick.RemoveListener(() => ProfileManager.Instance.Register(_name.text, _email.text, _mobile.text, _password.text));
        LogIn.onClick.RemoveListener(WindowController.Instance.PushWindow<LoginWindow>);
        Gmail.onClick.RemoveListener(() => Debug.Log("Gmail Login"));
    }
}
