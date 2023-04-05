using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamPunkRhinoceros : MonoBehaviour
{
    public static SteamPunkRhinoceros Ins;
    BossController bossController;
    PlayerController player;

    public float delayStarting;
    public float delayAction;
    public float delaySequence;

    [Header("Moving")]
    public bool shouldMove;
    public Rigidbody2D theRB;
    public float moveSpeed;
    private Vector2 moveDirection;

    [Header("Shooting")]
    public Transform[] shotPoints;
    public Transform[] shotPointRadia;
    public GameObject bullet1, bullet2;
    public float timeBetweenShoots; //fireRate
    public float shootCounter;

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
        StartCoroutine(IEStarting());
        bossController.ske.AnimationState.Complete += AnimationState_Complete;
    }

    private void AnimationState_Complete(Spine.TrackEntry trackEntry)
    {
        switch (trackEntry.Animation.Name)
        {
            case "Move":
                bossController.ske.AnimationState.SetAnimation(0, "Skill_1", false);
                break;
        }

    }

    private void Update()
    {
        shootCounter -= Time.deltaTime;

        if (shouldMove)
        {
            Moving();
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

    public void Shoot(int type)
    {
        // type == 0 => bawsn thuong || type == 1 => 
        if (bossController.currentHealth > 0)
        {
            if (type == 0)
            {
                if (shoot != null) StopCoroutine(shoot);
                shoot = StartCoroutine(IEShoot());
            }
            else if (type == 1)
            {
                if (shootRadia != null) StopCoroutine(shootRadia);
                shootRadia = StartCoroutine(IEShootRadia());
            }
        }
    }

    public void Dash()
    {
        if (bossController.currentHealth > 0)
        {
            if (dash != null) StopCoroutine(dash);

            dash = StartCoroutine(IEDash());
        }
    }

    IEnumerator IEStarting()
    {
        yield return new WaitForSeconds(delayStarting);
        bossController.ske.AnimationState.SetAnimation(0, "Move", false);
        DOVirtual.DelayedCall(0.367f, () =>
        {
            //Shoot(0);
        });
    }

    Coroutine shoot;
    IEnumerator IEShoot()
    {
        //Skil_1
        shouldMove = false;
        if (shootCounter <= 0)
        {
            bossController.ske.AnimationState.SetAnimation(0, "Skill_1", false);
            shootCounter = timeBetweenShoots;

            DOVirtual.DelayedCall(0.833f, () =>
            {
                foreach (Transform t in shotPoints)
                {
                    Instantiate(bullet1, t.position, t.rotation);
                }
            });
        }

        yield return new WaitForSeconds(delayAction);
        shouldMove = true;
        bossController.ske.AnimationState.SetAnimation(0, "Move", false);
        DOVirtual.DelayedCall(0.367f, () =>
        {
            Shoot(1);
        });
    }

    Coroutine shootRadia;
    IEnumerator IEShootRadia()
    {
        //Skill_2

        shouldMove = false;
        if (shootCounter <= 0)
        {
            bossController.ske.AnimationState.SetAnimation(0, "Skill_2", false);
            shootCounter = timeBetweenShoots;
            DOVirtual.DelayedCall(0.833f, () =>
            {
                foreach (Transform t in shotPointRadia)
                {
                    Instantiate(bullet2, t.position, t.rotation);
                }
            });

        }

        yield return new WaitForSeconds(delayAction);
        shouldMove = true;
        bossController.ske.AnimationState.SetAnimation(0, "Move", false);
        DOVirtual.DelayedCall(0.367f, () =>
        {
            Dash();
        });
    }

    Coroutine dash;
    IEnumerator IEDash()
    {
        //Skill_3_Ready
        bossController.ske.AnimationState.SetAnimation(0, "Skill_3_Ready", false);

        shouldMove = false;
        DOVirtual.DelayedCall(0.833f, () =>
        {
            //Skill_3_Attack
            bossController.ske.AnimationState.SetAnimation(0, "Skill_3_Attack", false);

            var pos = PlayerController.Ins.transform.position;
            Debug.Log(pos);
            transform.DOMove(pos, 1f);
        });

        yield return new WaitForSeconds(2f);
        shouldMove = true;
        bossController.ske.AnimationState.SetAnimation(0, "Move", false);
        DOVirtual.DelayedCall(0.367f, () =>
        {
            Shoot(0);
        });

    }
}
