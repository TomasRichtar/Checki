using Richi;
using System.Collections;
using System.Collections.Generic;
using TastyCore._Examples.UiManager.Scripts;
using UnityEngine;

public class BasicPage : Page
{
    public void Settings()
    {
        PageController.PushPage<BasicPage>(new ExampleOptionPageData
        {
            ExampleDebugOption = "Data passing does work"
        });
    }

    protected override void InitData(IPageData data) { }
}
