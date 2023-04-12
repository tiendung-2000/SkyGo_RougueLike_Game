using API.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToLoad = 4f;

    public string nextLevel;

    public bool isPaused;

    public Transform startPoint;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.Ins.transform.position = startPoint.position;
        PlayerController.Ins.canMove = true;

        Time.timeScale = 1f;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public IEnumerator LevelEnd()
    {
        AudioManager.instance.PlayerLevelWin();

        PlayerController.Ins.canMove = false;

        UIController.Ins.StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);

        //CharacterTracker.Ins.currentCoins = currentCoins;
        CharacterTracker.Ins.currentHealthSave = PlayerHealthController.Ins.currentHealth;
        CharacterTracker.Ins.maxHealthSave = PlayerHealthController.Ins.maxHealth;

        SceneManager.LoadScene(nextLevel);

        CanvasManager.Ins.CloseUI(UIName.WinUI);

        //PlayerHealthController.Ins.ReHealth();

    }

    public IEnumerator LevelTown()
    {
        PlayerController.Ins.canMove = false;

        UIController.Ins.StartFadeToBlack();

        yield return new WaitForSeconds(0.5f);

        //CharacterTracker.Ins.currentCoins = currentCoins;
        CharacterTracker.Ins.currentHealthSave = PlayerHealthController.Ins.currentHealth;
        CharacterTracker.Ins.maxHealthSave = PlayerHealthController.Ins.maxHealth;

        SceneManager.LoadScene(CharacterTracker.Ins.townLevel);

        Destroy(PlayerController.Ins.gameObject);

        CanvasManager.Ins.CloseUI(UIName.WinUI);
    }

    public void PauseUnpause()
    {
        if (!isPaused)
        {
            UIController.Ins.pauseMenu.SetActive(true);
            isPaused = true;

            Time.timeScale = 0f;
        }
        else
        {
            UIController.Ins.pauseMenu.SetActive(false);
            isPaused = false;

            Time.timeScale = 1f;
        }
    }

    

    
}
