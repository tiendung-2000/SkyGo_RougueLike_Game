using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrathFlameSkul : MonoBehaviour
{
    public static WrathFlameSkul Ins;

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
    public GameObject bulletFirst;
    public GameObject bulletSecond;
    public float fireRateFirst;
    public float fireRateSecond;
    public float shootCounter;
    public bool shootFirst = false;
    public bool shootSecond = false;
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
                var newBullet = Instantiate(bulletFirst, t.position, t.rotation);
                newBullet.transform.Rotate(0f, 0f, Random.Range(-xAngle, yAngle));
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
                Instantiate(bulletSecond, t.position, t.rotation);
            }
        }
    }

    //public void ShotRadia()
    //{
    //DOVirtual.DelayedCall(1, () =>
    //{
    //    if (shootCounter <= 0)
    //    {
    //        shootCounter = fireRateSecond;

    //        foreach (Transform t in shotPointRadia)
    //        {
    //            Instantiate(bullet, t.position, t.rotation);
    //        }
    //    }
    //});


    //}

    //==================SETING ACTION==================//
    public void ShootAction(int type)
    {
        // type == 0 => ban 5 vien dan || type == 1 => ban 3 vien dan
        if (bossController.currentHealth > 0)
        {
            if (type == 0)
            {
                if (shoot != null) StopCoroutine(shoot);
                shoot = StartCoroutine(DelayShootFirst());
            }
            else if (type == 1)
            {
                if (shootRadia != null) StopCoroutine(shootRadia);
                shootRadia = StartCoroutine(DelayShootSecond());
            }
        }
    }

    public void DashAction()
    {
        if (bossController.currentHealth > 0)
        {
            if (dash != null) StopCoroutine(dash);

            dash = StartCoroutine(DelayDash());
        }
    }

    IEnumerator Starting()
    {
        yield return new WaitForSeconds(delayStarting);
        shootFirst = false;
        ShootAction(0);
    }

    Coroutine shoot;
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

    Coroutine shootRadia;
    IEnumerator DelayShootSecond()
    {
        shootSecond = true;
        yield return new WaitForSeconds(delayAction);
        shootSecond = false;
        DashAction();
    }

    Coroutine dash;
    IEnumerator DelayDash()
    {
        DOVirtual.DelayedCall(1, () =>
        {
            var pos = PlayerController.Ins.transform.localPosition;
            Debug.Log(pos);
            transform.DOMove(pos, 1f);
        });
        yield return new WaitForSeconds(delaySequence + 1);
        ShootAction(0);
    }
}
