using System;
using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class ProfileDatabase
{
    public int Id;

    public string Name;
    public string Nickname;
    public string Email;
    public string Password;
    public string TelNumber;

    public int Traveling;
    public int Cooking;
    public int Music;
    public int Sport;
    public int Games;
    public int Relax;
    public int Art;
    public int Culture;

    public int Dogs;
    public int Cats;
    public int Fish;
    public int Other;

    public string Quests;
    public string Rewards;
    public string ChildrenIds;
    public string GmailId;

    public string FamilyCount;
    public string ChildrenCount;
}

[System.Serializable]
public class ProfileResponse
{
    public bool success;
    public ProfileDatabase data;
}
public class CreateProfileDatabase : SingletonMonoBehaviour<CreateProfileDatabase>
{
    public ProfileDatabase databaseQuests = new ProfileDatabase();

    public void GetProfileData(string email, Action<ProfileDatabase> onSuccess)
    {
        StartCoroutine(GetProfileCoroutine(email, onSuccess));
    }

    IEnumerator GetProfileCoroutine(string email, Action<ProfileDatabase> onSuccess)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://checkiapp.com/get_profile.php?email=" + email);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Chyba: " + www.error);
            onSuccess?.Invoke(null);
        }
        else
        {
            Debug.Log("Odpoved: " + www.downloadHandler.text);

            ProfileResponse response = JsonUtility.FromJson<ProfileResponse>(FixJson(www.downloadHandler.text));
            databaseQuests = response.data;

            onSuccess?.Invoke(databaseQuests);
        }
    }

    string FixJson(string value)
    {
        if (!value.TrimStart().StartsWith("{"))
            return "{\"data\":" + value + "}";
        return value;
    }
    public void CreateProfile(ProfileDatabase profileData, Action<bool> onSuccess)
    {
        StartCoroutine(SendDataCoroutine(profileData, onSuccess));
    }
    public IEnumerator SendDataCoroutine(ProfileDatabase profileData, Action<bool> onSuccess)
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", profileData.Name);
        form.AddField("Nickname", profileData.Nickname);
        form.AddField("Email", profileData.Email);
        form.AddField("Password", profileData.Password);
        form.AddField("TelNumber", profileData.TelNumber);

        form.AddField("Traveling", profileData.Traveling);
        form.AddField("Cooking", profileData.Cooking);
        form.AddField("Music", profileData.Music);
        form.AddField("Sport", profileData.Sport);
        form.AddField("Games", profileData.Games);
        form.AddField("Relax", profileData.Relax);
        form.AddField("Art", profileData.Art);
        form.AddField("Culture", profileData.Culture);

        form.AddField("Dogs", profileData.Dogs);
        form.AddField("Cats", profileData.Cats);
        form.AddField("Fish", profileData.Fish);
        form.AddField("Other", profileData.Other);

        form.AddField("Quests", profileData.Quests);
        form.AddField("Rewards", profileData.Rewards);
        form.AddField("ChildrenIds", profileData.ChildrenIds);
        form.AddField("GmailId", profileData.GmailId);

        form.AddField("FamilyCount", profileData.FamilyCount);
        form.AddField("ChildrenCount", profileData.ChildrenCount);

        UnityWebRequest www = UnityWebRequest.Post("http://checkiapp.com/create_profile.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Chyba: " + www.error);
            onSuccess?.Invoke(false);
        }
        else
        {
            Debug.Log("Odpoved: " + www.downloadHandler.text);
            onSuccess?.Invoke(true);
        }
    }
    public void UpdateData(ProfileDatabase profileData, Action<bool> onSuccess)
    {
        StartCoroutine(UpdateDataCoroutine(profileData, onSuccess));
    }

    IEnumerator UpdateDataCoroutine(ProfileDatabase profileData, Action<bool> onSuccess)
    {
        WWWForm form = new WWWForm();
        form.AddField("Id", profileData.Id);
        form.AddField("Name", profileData.Name);
        form.AddField("Nickname", profileData.Nickname);
        form.AddField("Email", profileData.Email);
        form.AddField("TelNumber", profileData.TelNumber);

        form.AddField("Traveling", profileData.Traveling);
        form.AddField("Cooking", profileData.Cooking);
        form.AddField("Music", profileData.Music);
        form.AddField("Sport", profileData.Sport);
        form.AddField("Games", profileData.Games);
        form.AddField("Relax", profileData.Relax);
        form.AddField("Art", profileData.Art);
        form.AddField("Culture", profileData.Culture);

        form.AddField("Dogs", profileData.Dogs);
        form.AddField("Cats", profileData.Cats);
        form.AddField("Fish", profileData.Fish);
        form.AddField("Other", profileData.Other);

        form.AddField("Quests", profileData.Quests);
        form.AddField("Rewards", profileData.Rewards);
        form.AddField("ChildrenIds", profileData.ChildrenIds);
        form.AddField("GmailId", profileData.GmailId);

        form.AddField("FamilyCount", profileData.FamilyCount);
        form.AddField("ChildrenCount", profileData.ChildrenCount);

        UnityWebRequest www = UnityWebRequest.Post("http://checkiapp.com/update_profile.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + www.error);
            onSuccess?.Invoke(false);
        }
        else
        {
            Debug.Log("Response: " + www.downloadHandler.text);
            onSuccess?.Invoke(www.downloadHandler.text == "SUCCESS");
        }
    }

    public void UpdatePassword(int userId, string oldPassword, string newPassword, Action<bool> onSuccess)
    {
        StartCoroutine(UpdatePasswordCoroutine(userId, oldPassword, newPassword, onSuccess));
    }

    IEnumerator UpdatePasswordCoroutine(int userId, string oldPassword, string newPassword, Action<bool> onSuccess)
    {
        WWWForm form = new WWWForm();
        form.AddField("Id", userId);
        form.AddField("OldPassword", oldPassword);
        form.AddField("NewPassword", newPassword);

        UnityWebRequest www = UnityWebRequest.Post("http://checkiapp.com/change_password.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error updating password: " + www.error);
            onSuccess?.Invoke(false);
        }
        else
        {
            Debug.Log("Password update response: " + www.downloadHandler.text);
            onSuccess?.Invoke(www.downloadHandler.text == "SUCCESS");
        }
    }

    public void LoginUser(string email, string password, Action<ProfileDatabase> onSuccess)
    {
        StartCoroutine(LoginUserCoroutine(email, password, onSuccess));
    }

    IEnumerator LoginUserCoroutine(string email, string password, Action<ProfileDatabase> onSuccess)
    {
        WWWForm form = new WWWForm();
        form.AddField("Email", email);
        form.AddField("Password", password);

        UnityWebRequest www = UnityWebRequest.Post("http://checkiapp.com/login_profile.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Login error: " + www.error);
            onSuccess?.Invoke(null);
        }
        else
        {
            Debug.Log("Login response: " + www.downloadHandler.text);

            string json = www.downloadHandler.text;

            if (json.Contains("\"success\":true"))
            {
                ProfileResponse response = JsonUtility.FromJson<ProfileResponse>(FixJson(www.downloadHandler.text));
                databaseQuests = response.data;

                onSuccess?.Invoke(databaseQuests);
            }
            else
            {
                onSuccess?.Invoke(null);
            }
        }
    }
}
