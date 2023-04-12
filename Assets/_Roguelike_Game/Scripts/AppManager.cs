using API.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : Singleton<AppManager>
{
    public string townLevel;

    void Start()
    {
        Action onLoaded = () =>
        {
            CanvasManager.Ins.OpenUI(UIName.MenuUI, null);
        };
        CanvasManager.Ins.OpenUI(UIName.LoadingUI, new object[] { onLoaded });
    }
}
