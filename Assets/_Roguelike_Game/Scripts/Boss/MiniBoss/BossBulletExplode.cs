using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletExplode : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DOVirtual.DelayedCall(1, () =>
        {
            Destroy(gameObject);
        });

        //AudioManager.instance.PlaySFX(4);

        if (other.tag == "Player")
        {
            PlayerHealthController.Ins.DamagePlayer();
        }
    }
}
