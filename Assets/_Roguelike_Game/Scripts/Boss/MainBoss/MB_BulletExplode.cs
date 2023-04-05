using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_BulletExplode : MonoBehaviour
{
    public float speed;
    private Vector3 direction;
    public GameObject explodeEffect;
    //public bool hasSpanw;

    // Start is called before the first frame update
    void Start()
    {
        direction = transform.right;

        //if (hasSpanw)
        //{
        //    DOVirtual.DelayedCall(1, () =>
        //    {
        //        transform.GetComponent<BossBulletSpawn>().Spawn();
        //    });
        //}
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (!BossController.Ins.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(explodeEffect, transform.position, transform.rotation);
        //DOVirtual.DelayedCall(.5f, () =>
        //{
        //    SmartPool.Ins.Spawn(explodeEffect, transform.position, transform.rotation);
        //});
        Destroy(gameObject);

        //AudioManager.instance.PlaySFX(4);

        if (other.tag == "Player")
        {
            PlayerHealthController.Ins.DamagePlayer();
        }
    }
}
