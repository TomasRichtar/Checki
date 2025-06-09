using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class QuestDatabase
{
    public int Id;
    public string Title;
    public string ComplitionTime;
    public string Days;
    public int Repeatable;
    public int Credit;
    public string QuestStatus;
    public int UserId;
    public int ChildrenId;
    public int Image;
}

[System.Serializable]
public class QuestResponse
{
    public bool success;
    public List<QuestDatabase> data;
}

public class CreateQuestDatabase : MonoBehaviour
{
    public List<QuestDatabase> databaseQuests = new List<QuestDatabase>();
   // public List<Quest> quests = new List<Quest>();

    void Start()
    {
        //StartCoroutine(SendQuestCoroutine());
        //StartCoroutine(GetQuestsCoroutine());
    }

    IEnumerator GetQuestsCoroutine()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/get_quests.php");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Chyba: " + www.error);
        }
        else
        {
            Debug.Log("Odpoved: " + www.downloadHandler.text);

            QuestResponse response = JsonUtility.FromJson<QuestResponse>(FixJson(www.downloadHandler.text));
            databaseQuests = response.data;

            foreach (QuestDatabase quest in databaseQuests)
            {
                Debug.Log($"ID: {quest.Id}, Název: {quest.Title}, Body: {quest.Credit}");
            }
        }
    }

    //public void MapQuest(QuestDatabase questDatabase)
    //{
    //    Quest quest = ScriptableObject.CreateInstance<Quest>();

    //    quest.Id = dbQuest.Id;
    //    quest.Title = dbQuest.Title;
    //    quest.ComplitionTime = dbQuest.ComplitionTime;
    //    quest.Days = ParseDays(dbQuest.Days); // převod ze stringu
    //    quest.Repeatable = dbQuest.Repeatable;
    //    quest.Credit = dbQuest.Credit;

    //    // Enum parse – můžeš upravit na bezpečnější variantu s TryParse
    //    quest.QuestStatus = Enum.TryParse<QuestStatus>(dbQuest.QuestStatus, true, out var status)
    //        ? status
    //        : QuestStatus.None; // Fallback, pokud neplatná hodnota

    //    quest.UserId = dbQuest.UserId;
    //    quest.ChildrenId = dbQuest.ChildrenId;

    //    // Můžeš si zde načítat obrázek z adresáře nebo použít výchozí
    //    quest.Image = defaultSprite;

    //    return quest;
    //}

    string FixJson(string value)
    {
        if (!value.TrimStart().StartsWith("{"))
            return "{\"data\":" + value + "}";
        return value;
    }

    [ContextMenu("Send Test Quest")]
    public void SendTestQuest()
    {
        StartCoroutine(SendQuestCoroutine());
    }

    IEnumerator SendQuestCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("Title", "Ukliď pokoj");
        form.AddField("ComplitionTime", "2025-06-01");
        form.AddField("Days", "7");
        form.AddField("Repeatable", 1); // true
        form.AddField("Credit", 500);
        form.AddField("QuestStatus", "Test");
        form.AddField("ProfileId", 1);
        form.AddField("ChildrenId", 1);
        form.AddField("ImageId", 5);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/create_quest.php", form);
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
