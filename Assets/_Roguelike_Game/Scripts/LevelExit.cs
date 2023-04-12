using API.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string levelToLoad;

    public bool mainGate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController.Ins.canMove = false;
            if (mainGate == true)
            {
                CanvasManager.Ins.OpenUI(UIName.LoadingUI, null);
                SceneManager.LoadScene(levelToLoad);
            }
            else
            {
                StartCoroutine(IEShowWin());
            }
        }
    }

    public IEnumerator IEShowWin()
    {
        yield return new WaitForSeconds(1f);
        CanvasManager.Ins.OpenUI(UIName.WinUI, null);
    }
}
