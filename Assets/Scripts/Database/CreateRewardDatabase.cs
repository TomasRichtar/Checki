using System;
using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Reward
{
    public int Id;
    public string Title;
    public int ImageId;
    public int Price;
    public string ValidUntil;
    public int ChildrenId;
    public int ProfileId;
}

[System.Serializable]
public class RewardResponse
{
    public bool success;
    public List<Reward> data;
}

public class CreateRewardDatabase : SingletonMonoBehaviour<CreateRewardDatabase>
{
    public List<Reward> databaseRewards = new List<Reward>();
    // public List<Quest> quests = new List<Quest>();

    void Start()
    {
        //StartCoroutine(SendQuestCoroutine());
        //StartCoroutine(GetQuestsCoroutine());
    }
    public void GetRewardData(int id, Action<List<Reward>> onSuccess)
    {
        StartCoroutine(GetRewardCoroutine(id, onSuccess));
    }

    IEnumerator GetRewardCoroutine(int id, Action<List<Reward>> onSuccess)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/get_rewards.php?id=" + id);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Chyba: " + www.error);
            onSuccess?.Invoke(null);
        }
        else
        {
            Debug.Log("Odpoved: " + www.downloadHandler.text);

            RewardResponse response = JsonUtility.FromJson<RewardResponse>(FixJson(www.downloadHandler.text));
            databaseRewards = response.data;

            onSuccess?.Invoke(databaseRewards);
        }
    }
    string FixJson(string value)
    {
        if (!value.TrimStart().StartsWith("{"))
            return "{\"data\":" + value + "}";
        return value;
    }

    public void CreateReward(Reward data, Action<bool> onSuccess)
    {
        StartCoroutine(SendDataCoroutine(data, onSuccess));
    }
    public IEnumerator SendDataCoroutine(Reward data, Action<bool> onSuccess)
    {
        WWWForm form = new WWWForm();
        form.AddField("Title", data.Title);
        form.AddField("ImageId", data.ImageId);
        form.AddField("Price", data.Price);
        form.AddField("ValidUntil", data.ValidUntil);
        form.AddField("ChildrenId", data.ChildrenId);
        form.AddField("ProfileId", data.ProfileId);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/create_rewards.php", form);
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

    IEnumerator DeleteDataCoroutine(int rewardId, Action<bool> onSuccess)
    {
        WWWForm form = new WWWForm();
        form.AddField("Id", rewardId);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/delete_rewards.php", form))
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
}
