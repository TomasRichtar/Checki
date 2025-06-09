using System;
using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEditor.AddressableAssets.Settings.AddressableAssetProfileSettings;

[System.Serializable]
public class ChildrenDatabase
{
    public int Id;

    public string Name;
    public int Password;
    public string Nickname;
    public int Age;

    public string QuestIds;
    public string RewardIds;
    public int ProfileId;

    public int Traveling;
    public int Cooking;
    public int Music;
    public int Sport;
    public int Games;
    public int Relax;
    public int Art;
    public int Culture;
}

[System.Serializable]
public class ChildrenResponse
{
    public bool success;
    public List<ChildrenDatabase> data;
}
public class CreateChildrenDatabase : SingletonMonoBehaviour<CreateChildrenDatabase>
{
    public List<ChildrenDatabase> databaseItems = new List<ChildrenDatabase>();

    public void GetChildrenData(Action<List<ChildrenDatabase>> onSuccess)
    {
        StartCoroutine(GetChildrenCoroutine(onSuccess));
    }

    IEnumerator GetChildrenCoroutine(Action<List<ChildrenDatabase>> onSuccess)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/get_children.php");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Chyba: " + www.error);
            onSuccess?.Invoke(null);
        }
        else
        {
            Debug.Log("Odpoved: " + www.downloadHandler.text);

            ChildrenResponse response = JsonUtility.FromJson<ChildrenResponse>(FixJson(www.downloadHandler.text));
            databaseItems = response.data;

            foreach(ChildrenDatabase quest in databaseItems)
            {
                Debug.Log($"ID: {quest.Id}, Title: {quest.Name}, Profile: {quest.ProfileId}");
            }
            onSuccess?.Invoke(databaseItems);
        }
    }

    string FixJson(string value)
    {
        if (!value.TrimStart().StartsWith("{"))
            return "{\"data\":" + value + "}";
        return value;
    }
    public void CreateChildren(ChildrenDatabase Data)
    {
        StartCoroutine(SendDataCoroutine(Data));
    }
    public IEnumerator SendDataCoroutine(ChildrenDatabase Data)
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", Data.Name);
        form.AddField("Nickname", Data.Nickname);
        form.AddField("Password", Data.Password);
        form.AddField("Age", Data.Age);

        form.AddField("QuestIds", Data.QuestIds);
        form.AddField("RewardIds", Data.RewardIds);
        form.AddField("ProfileId", Data.ProfileId);

        form.AddField("Traveling", Data.Traveling);
        form.AddField("Cooking", Data.Cooking);
        form.AddField("Music", Data.Music);
        form.AddField("Sport", Data.Sport);
        form.AddField("Games", Data.Games);
        form.AddField("Relax", Data.Relax);
        form.AddField("Art", Data.Art);
        form.AddField("Culture", Data.Culture);


        UnityWebRequest www = UnityWebRequest.Post("http://localhost/create_children.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Chyba: " + www.error);
        }
        else
        {
            Debug.Log("Odpoved: " + www.downloadHandler.text);
        }
    }
}
