using API.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingUI : BaseUIMenu
{
    public Image progress;

    Action OnLoaded;

    Tween OnEnableTween;

    public override void Init(object[] initParams)
    {
        base.Init(initParams);
        OnLoaded = null;
        if (initParams != null)
        {
            if (initParams.Length > 0)
            {
                OnLoaded = (Action)initParams[0];
            }
        }
        LoadingRun();
    }
    public void LoadingRun()
    {
        progress.fillAmount = 0;
        OnEnableTween?.Kill();
        OnEnableTween = progress.DOFillAmount(1f, 4.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            OnLoaded?.Invoke();
            OnLoaded = null;
            Close();
        });
    }
}
