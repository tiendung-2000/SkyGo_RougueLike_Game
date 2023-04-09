using UnityEngine;

public class CheckPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //BossController.Ins.playerOnZone = true;
        }
    }
}
