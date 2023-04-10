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

    public int currentCoins;

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

        currentCoins = CharacterTracker.Ins.currentCoins;

        Time.timeScale = 1f;

        UIController.Ins.cointText.text = currentCoins.ToString();
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

        CharacterTracker.Ins.currentCoins = currentCoins;
        CharacterTracker.Ins.currentHealth = PlayerHealthController.Ins.currentHealth;
        CharacterTracker.Ins.maxHealth = PlayerHealthController.Ins.maxHealth;

        SceneManager.LoadScene(nextLevel);

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

    public void GetCoins(int amount)
    {
        currentCoins += amount;
        UIController.Ins.cointText.text = currentCoins.ToString();
    }

    public void SpendCoins(int amount)
    {
        currentCoins -= amount;

        if (currentCoins < 0)
        {
            currentCoins = 0;
        }
        UIController.Ins.cointText.text = currentCoins.ToString();
    }
}
