using API.UI;
using UnityEngine;

public class CheckPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CanvasManager.Ins.OpenUI(UIName.BossHubUI, null);
            gameObject.SetActive(false);
        }
    }
}
