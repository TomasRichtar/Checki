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
}
