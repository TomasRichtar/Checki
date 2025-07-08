using System;
using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class ChildrenResponse
{
    public bool success;
    public List<Children> data;
}
public class ChildrenDatabase : SingletonMonoBehaviour<ChildrenDatabase>
{
    public List<Children> databaseItems = new List<Children>();

    public void GetChildrenData(int id, Action<List<Children>> onSuccess)
    {
        StartCoroutine(GetChildrenCoroutine(id, onSuccess));
    }

    IEnumerator GetChildrenCoroutine(int id, Action<List<Children>> onSuccess)
    {
        UnityWebRequest www = UnityWebRequest.Get("https://checkiapp.com/get_children.php?id=" + id);
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

            foreach(Children quest in databaseItems)
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

    public void CreateChildren(Children Data, Action<bool> onSuccess)
    {
        StartCoroutine(SendDataCoroutine(Data, onSuccess));
    }

    public IEnumerator SendDataCoroutine(Children Data, Action<bool> onSuccess)
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

        form.AddField("UnlockedMonsters", Data.UnlockedMonsters);
        form.AddField("UnlockedEquipment", Data.UnlockedEquipment);
        form.AddField("SelectedMonster", Data.SelectedMonster);
        form.AddField("SelectedEquipment", Data.SelectedEquipment);

        form.AddField("Credit", Data.Credit);
        form.AddField("Money", Data.Money);
        form.AddField("ImageId", Data.ImageId);


        UnityWebRequest www = UnityWebRequest.Post("https://checkiapp.com/create_children.php", form);
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

    public void UpdateData(Children Data, Action<bool> onSuccess)
    {
        StartCoroutine(UpdateDataCoroutine(Data, onSuccess));
    }

    IEnumerator UpdateDataCoroutine(Children Data, Action<bool> onSuccess)
    {
        WWWForm form = new WWWForm();
        form.AddField("Id", Data.Id);
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

        form.AddField("UnlockedMonsters", Data.UnlockedMonsters);
        form.AddField("UnlockedEquipment", Data.UnlockedEquipment);
        form.AddField("SelectedMonster", Data.SelectedMonster);
        form.AddField("SelectedEquipment", Data.SelectedEquipment);

        form.AddField("Credit", Data.Credit);
        form.AddField("Money", Data.Money);
        form.AddField("ImageId", Data.ImageId);

        UnityWebRequest www = UnityWebRequest.Post("https://checkiapp.com/update_children.php", form);
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
}
