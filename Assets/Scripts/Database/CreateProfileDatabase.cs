using System;
using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEditor.AddressableAssets.Settings.AddressableAssetProfileSettings;

[System.Serializable]
public class ProfileDatabase
{
    public int Id;

    public string Name;
    public string Nickname;
    public string Email;
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
    public List<ProfileDatabase> data;
}
public class CreateProfileDatabase : SingletonMonoBehaviour<CreateProfileDatabase>
{
    public List<ProfileDatabase> databaseQuests = new List<ProfileDatabase>();

    public void GetProfileData(int id, Action<ProfileDatabase> onSuccess)
    {
        StartCoroutine(GetProfileCoroutine(id, onSuccess));
    }

    IEnumerator GetProfileCoroutine(int id, Action<ProfileDatabase> onSuccess)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/get_profile.php");
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

            foreach (ProfileDatabase quest in databaseQuests)
            {
                Debug.Log($"ID: {quest.Id}, Title: {quest.Name}, Body: {quest.Email}");
            }
            onSuccess?.Invoke(databaseQuests[0]);
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

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/create_profile.php", form);
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
}
