using API.UI;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : BaseUIMenu
{
    [SerializeField] GameObject result;
    [SerializeField] TMP_Text coinText;

    [SerializeField] Button homeButton;
    [SerializeField] Button nextButton;

    private void Start()
    {
        homeButton.onClick.AddListener(GoHome);
        nextButton.onClick.AddListener(NextLevel);
    }

    void GoHome()
    {
        CanvasManager.Ins.OpenUI(UIName.LoadingUI, null);
        StartCoroutine(LevelManager.instance.LevelTown());
    }

    void NextLevel()
    {
        CanvasManager.Ins.OpenUI(UIName.LoadingUI, null);
        StartCoroutine(LevelManager.instance.LevelEnd());
    }

    private void OnEnable()
    {
        coinText.text = CharacterTracker.Ins.currentCoins.ToString();
        result.transform.DOScale(1f, 0.5f);
    }

    private void OnDisable()
    {
        result.transform.DOScale(0.5f, 0f);
    }
}
