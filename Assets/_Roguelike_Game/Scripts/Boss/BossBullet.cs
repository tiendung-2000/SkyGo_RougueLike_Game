using DG.Tweening;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed;
    private Vector3 direction;

    void OnEnable()
    {
        direction = transform.right;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.Ins.DamagePlayer();
        }

        SmartPool.Ins.Despawn(gameObject);

        //AudioManager.instance.PlaySFX(4);
    }
}
