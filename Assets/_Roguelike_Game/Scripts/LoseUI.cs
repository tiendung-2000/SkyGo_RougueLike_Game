using API.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseUI : BaseUIMenu
{
    public string townScene = "Town";
    public Button restButton;

    public GameObject lose;

    private void Start()
    {
        restButton.onClick.AddListener(ReturnToTown);
    }

    private void OnEnable()
    {
        lose.transform.DOScale(1f, 0.5f);
    }

    private void OnDisable()
    {
        lose.transform.DOScale(0.5f, 0f);
    }

    public void ReturnToTown()
    {
        Time.timeScale = 1f;

        CanvasManager.Ins.OpenUI(UIName.LoadingUI, null);
        SceneManager.LoadScene(townScene);

        Destroy(PlayerController.Ins.gameObject);

        Close();
    }
}
