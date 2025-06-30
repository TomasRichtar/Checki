using System;
using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Quest
{
    public int Id;
    public string Title;
    public string ComplitionTime;
    public string Days;
    public int Repeatable;
    public int Credit;
    public string QuestStatus;
    public int ProfileId;
    public int ChildrenId;
    public int ImageId;
}

[System.Serializable]
public class QuestResponse
{
    public bool success;
    public List<Quest> data;
}

public class CreateQuestDatabase : SingletonMonoBehaviour<CreateQuestDatabase>
{
    public List<Quest> databaseQuests = new List<Quest>();
   // public List<Quest> quests = new List<Quest>();

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void GetQuestData(int id, Action<List<Quest>> onSuccess)
    {
        StartCoroutine(GetQuestsCoroutine(id, onSuccess));
    }

    IEnumerator GetQuestsCoroutine(int id, Action<List<Quest>> onSuccess)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://checkiapp.com/get_quests.php?id=" + id);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Chyba: " + www.error);
            onSuccess?.Invoke(null);
        }
        else
        {
            Debug.Log("Odpoved: " + www.downloadHandler.text);

            QuestResponse response = JsonUtility.FromJson<QuestResponse>(FixJson(www.downloadHandler.text));
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

    public void CreateQuest(Quest Data, Action<bool> onSuccess)
    {
        StartCoroutine(SendDataCoroutine(Data, onSuccess));
    }
    public IEnumerator SendDataCoroutine(Quest Data, Action<bool> onSuccess)
    {
        WWWForm form = new WWWForm();
        form.AddField("Title", Data.Title);
        form.AddField("ComplitionTime", Data.ComplitionTime);
        form.AddField("Days", Data.Days);
        form.AddField("Repeatable", Data.Repeatable);

        form.AddField("Credit", Data.Credit);
        form.AddField("QuestStatus", Data.QuestStatus);
        form.AddField("ProfileId", Data.ProfileId);

        form.AddField("ChildrenId", Data.ChildrenId);
        form.AddField("ImageId", Data.ImageId);

        UnityWebRequest www = UnityWebRequest.Post("http://checkiapp.com/create_quest.php", form);
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
    public void Delete(int dataId, Action<bool> onSuccess)
    {
        StartCoroutine(DeleteDataCoroutine(dataId, onSuccess));
    }

    IEnumerator DeleteDataCoroutine(int id, Action<bool> onSuccess)
    {
        WWWForm form = new WWWForm();
        form.AddField("Id", id);

        using (UnityWebRequest www = UnityWebRequest.Post("http://checkiapp.com/delete_quest.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Delete error: " + www.error);
                onSuccess?.Invoke(false);
            }
            else
            {
                Debug.Log("request return: " + www.downloadHandler.text);
                onSuccess?.Invoke(true);
            }
        }
    }
    public void UpdateData(Quest data, Action<bool> onSuccess)
    {
        StartCoroutine(UpdateDataCoroutine(data, onSuccess));
    }

    IEnumerator UpdateDataCoroutine(Quest data, Action<bool> onSuccess)
    {
        WWWForm form = new WWWForm();
        form.AddField("Id", data.Id);
        form.AddField("Title", data.Title);
        form.AddField("ComplitionTime", data.ComplitionTime);
        form.AddField("Days", data.Days);
        form.AddField("Repeatable", data.Repeatable);

        form.AddField("Credit", data.Credit);
        form.AddField("QuestStatus", data.QuestStatus);
        form.AddField("ProfileId", data.ProfileId);

        form.AddField("ChildrenId", data.ChildrenId);
        form.AddField("ImageId", data.ImageId);

        UnityWebRequest www = UnityWebRequest.Post("http://checkiapp.com/update_quest.php", form);
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
