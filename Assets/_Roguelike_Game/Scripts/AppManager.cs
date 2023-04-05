using API.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : Singleton<AppManager>
{
    //[SerializeField] bool removeAds = false;

    //[SerializeField] bool hackCoin = false;

    // Start is called before the first frame update
    void Start()
    {
        Action onLoaded = () =>
        {
            CanvasManager.Ins.OpenUI(UIName.MenuUI, null);
        };
        CanvasManager.Ins.OpenUI(UIName.LoadingUI, new object[] { onLoaded });
    }
}
