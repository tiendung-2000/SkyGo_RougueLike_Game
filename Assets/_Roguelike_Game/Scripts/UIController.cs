using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using API.UI;
using TMPro;

public class UIController : BaseUIMenu
{
    public static UIController Ins;

    public GameObject map;

    public Slider healthSlider;
    public TMP_Text healthText, cointText;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeToBlack, fadeOutBlack;


    public GameObject pauseMenu, mapDisplay, bigMapText;

    private void Awake()
    {
        Ins = this;

    }

    void Start()
    {
        fadeOutBlack = true;
        fadeToBlack = false;
        CanvasManager.Ins.OpenUI(UIName.LoadingUI, null);
    }

    void Update()
    {
        if (fadeOutBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                fadeOutBlack = false;
            }
        }

        if (fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }
        healthSlider.value = PlayerHealthController.Ins.currentHealth;

        healthText.text = PlayerHealthController.Ins.currentHealth.ToString() + " / " + PlayerHealthController.Ins.maxHealth.ToString();
    }

    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }

    //public void NewGame()
    //{
    //    Time.timeScale = 1f;

    //    SceneManager.LoadScene(newGameScene);

    //    Destroy(PlayerController.Ins.gameObject);
    //}

    

    public void Resume()
    {
        LevelManager.instance.PauseUnpause();
    }
}
