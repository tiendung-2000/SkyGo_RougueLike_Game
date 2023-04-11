using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string levelToLoad;

    //public bool comeToBossLevel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //SceneManager.LoadScene(levelToLoad);

            StartCoroutine(LevelManager.instance.LevelEnd());

            //if (comeToBossLevel)
            //{
            //    StartCoroutine(IEShowBossHub());
            //}

        }
    }

    //IEnumerator IEShowBossHub()
    //{
    //    yield return new WaitForSeconds(2f);
    //    UIController.Ins.bossHub.SetActive(true);
    //}
}
