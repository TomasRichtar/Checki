using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormManager : MonoBehaviour
{
    [SerializeField] private List<string> _usersNames = new List<string>();
    [SerializeField] private List<string> _usersPasswords = new List<string>();

    [SerializeField] private string userName;
    [SerializeField] private string userLogin;


    public void LogIn(string userName, string password)
    {
        
    }

    bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            Debug.LogError("Email " + email + " is NOT valid");
            return false;
        }
    }
}
