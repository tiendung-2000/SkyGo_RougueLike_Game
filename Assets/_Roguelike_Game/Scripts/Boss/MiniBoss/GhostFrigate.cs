using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFrigate : MonoBehaviour
{
    public static GhostFrigate Ins;

    BossController bossController;

    public float delayStarting;
    public float delayAction;
    public float delaySequence;

    [Header("Moving")]
    public bool shouldMove;
    public Rigidbody2D theRB;
    public float moveSpeed;
    private Vector2 moveDirection;

    [Header("Shooting")]
    public float xAngle;
    public float yAngle;
    public Transform[] shotPointsFirst;
    public Transform[] shotPointsSecond;
    public Transform[] shotPointsThird;
    public GameObject bulletFirst;
    public GameObject bulletSecond;
    public GameObject bulletThird;
    public float fireRateFirst;
    public float fireRateSecond;
    public float fireRateThird;
    public float shootCounter;
    public bool shootFirst = false;
    public bool shootSecond = false;
    public bool shootThird = false;
    private void Awake()
    {
        if (Ins == null)
        {
            Ins = this;
        }
    }

    private void Start()
    {
        bossController = GetComponent<BossController>();
        StartCoroutine(Starting());
    }

    private void Update()
    {
        shootCounter -= Time.deltaTime;

        if (shouldMove)
        {
            Moving();
        }

        if (shootFirst)
        {
            ShootFirst();
        }

        if (shootSecond)
        {
            ShootSecond();
        }

        if (shootThird)
        {
            ShootThird();
        }
    }

    public void Moving()
    {
        //handle movement
        if (bossController.currentHealth > 0)
        {
            moveDirection = Vector2.zero;
            moveDirection = PlayerController.Ins.transform.position - transform.position;
            moveDirection.Normalize();
            theRB.velocity = moveDirection * moveSpeed;
        }
    }

    public void ShootFirst()
    {
        if (shootCounter <= 0)
        {
            shootCounter = fireRateFirst;

            foreach (Transform t in shotPointsFirst)
            {
                Instantiate(bulletFirst, t.position, t.rotation);
            }
        }
    }

    public void ShootSecond()
    {
        if (shootCounter <= 0)
        {
            shootCounter = fireRateSecond;

            foreach (Transform t in shotPointsSecond)
            {
                var newBullet = Instantiate(bulletSecond, t.position, t.rotation);
                newBullet.transform.Rotate(0f, 0f, Random.Range(-xAngle, yAngle));
            }
        }
    }

    public void ShootThird()
    {
        if (shootCounter <= 0)
        {
            shootCounter = fireRateThird;

            foreach (Transform t in shotPointsThird)
            {
                Instantiate(bulletThird, t.position, t.rotation);
            }
        }
    }

    //==================SETING ACTION==================//
    public void ShootAction(int type)
    {
        // type == 0 => ban 5 vien dan || type == 1 => ban 3 vien dan
        if (bossController.currentHealth > 0)
        {
            if (type == 0)
            {
                if (shootSt != null) StopCoroutine(shootSt);
                shootSt = StartCoroutine(DelayShootFirst());
            }
            else if (type == 1)
            {
                if (shootNd != null) StopCoroutine(shootNd);
                shootNd = StartCoroutine(DelayShootSecond());
            }
            else if (type == 2)
            {
                if (shootTh != null) StopCoroutine(shootTh);
                shootTh = StartCoroutine(DelayShootThird());
            }
        }
    }

    IEnumerator Starting()
    {
        yield return new WaitForSeconds(delayStarting);
        shootFirst = false;
        ShootAction(0);
    }

    Coroutine shootSt;
    IEnumerator DelayShootFirst()
    {
        shootFirst = true;
        yield return new WaitForSeconds(delayAction);
        shootFirst = false;
        DOVirtual.DelayedCall(1, () =>
        {
            ShootAction(1);
        });
    }

    Coroutine shootNd;
    IEnumerator DelayShootSecond()
    {
        shootSecond = true;
        yield return new WaitForSeconds(delayAction);
        shootSecond = false;
        DOVirtual.DelayedCall(1, () =>
        {
            ShootAction(2);
        });
    }

    Coroutine shootTh;
    IEnumerator DelayShootThird()
    {
        shootThird = true;
        yield return new WaitForSeconds(delayAction);
        shootThird = false;
        DOVirtual.DelayedCall(3, () =>
        {
            ShootAction(0);
        });
    }
}
