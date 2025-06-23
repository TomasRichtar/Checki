using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SingletonMonoBehaviour<SceneController>
{
    private void OnEnable()
    {
        //MyGameManager.Instance.OnDataLoaded += LoadSceneFromInit;
    }

    private void OnDisable()
    {
        //MyGameManager.Instance.OnDataLoaded -= LoadSceneFromInit;
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    public void LoadSceneFromInit()
    {
        SwitchScene("GamePartScene");
    }
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ChildScene(bool loadAll = false)
    {
        if (loadAll || PlayerPrefs.GetInt("ChildId") == 0 || PlayerPrefs.GetInt("ProfileId") == 0)
        {
            MyGameManager.Instance.ProfileId = PlayerPrefs.GetInt("ProfileId");
            MyGameManager.Instance.ChildrenId = PlayerPrefs.GetInt("ChildId");
            MyGameManager.Instance.LoadAllData(() =>
            {
                SceneManager.LoadScene("GamePartScene");
                return;
            });
        }

        if (PlayerPrefs.GetInt("ChildId") == 0 || PlayerPrefs.GetInt("ProfileId") == 0)
        {
            WindowController.Instance.PushPopUpWindow(
               "RegisterAsParrentFirstTitle",
               "RegisterAsParrentFirst",
               "Continue",
               null);
            return;
        }
        SceneManager.LoadScene("GamePartScene");
    }
}
