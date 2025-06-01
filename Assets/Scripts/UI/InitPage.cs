using Richi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPage : Page
{
    public void Initialize()
    {
        PageController.PushPage<ExampleScanPage>();
        PageController.PushPage<ExampleScanPage>();
    }

    protected override void InitData(IPageData data) { }
}

