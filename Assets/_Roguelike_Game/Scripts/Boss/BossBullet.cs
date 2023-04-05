using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed;
    private Vector3 direction;

    public bool hasSpawn;

    // Start is called before the first frame update
    void OnEnable()
    {
        direction = transform.right;

        if (hasSpawn)
        {
            DOVirtual.DelayedCall(1, () =>
            {
                transform.GetComponent<BossBulletSpawn>().Spawn();
            });
        }
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
        if (other.tag == "Player")
        {
            PlayerHealthController.Ins.DamagePlayer();
        }

        Destroy(gameObject);

        //AudioManager.instance.PlaySFX(4);
    }
}
