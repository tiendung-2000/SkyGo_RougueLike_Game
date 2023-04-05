using API.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : BaseUIMenu
{
    public string levelToLoad;

    public GameObject deletePanel;

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
        CanvasManager.Ins.OpenUI(UIName.GameplayUI, null);
        Close();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void DeleteSave()
    {
        deletePanel.SetActive(true);
    }

    public void ConfirmDelete()
    {
        deletePanel.SetActive(false);

        //PlayerPrefs.DeleteAll();

    }

    public void CancelDelete()
    {
        deletePanel.SetActive(false);
    }


}
