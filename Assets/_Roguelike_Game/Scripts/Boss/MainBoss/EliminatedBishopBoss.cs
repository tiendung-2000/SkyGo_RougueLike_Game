using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EliminatedBishopBoss : MonoBehaviour
{
    public static EliminatedBishopBoss Ins;

    BossController bossController;

    public SkeletonAnimation ske;

    [Header("Moving")]
    public bool shouldMove;
    public float moveSpeed;
    public Rigidbody2D theRB;
    private Vector2 moveDirection;

    [Header("Shooting")]
    public float xAngle;
    public float yAngle;

    public Transform[] shotPointsFirst;
    public Transform[] shotPointsSecond;
    public Transform[] shotPointsFour;
    public Transform[] shotPointsFive;

    public GameObject bulletFirst;
    public GameObject bulletSecond;
    public GameObject bulletFour;
    public GameObject bulletFive;

    public float fireRateFirst;
    public float fireRateSecond;
    public float fireRateFour;
    public float fireRateFive;

    public float shootCounter;

    public bool phaseFirst = false;
    public bool phaseSecond = false;
    public bool phaseFour = false;
    public bool phaseFive = false;
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

        ske.AnimationState.Complete += AnimationState_Complete; ;

        if (shouldMove == true)
        {
            ske.AnimationState.SetAnimation(0, "Move", false);
        }
    }

    private void AnimationState_Complete(Spine.TrackEntry trackEntry)
    {
        switch (trackEntry.Animation.Name)
        {
            case AnimationKeys.B2_MOVE:
                shouldMove = false;
                ske.AnimationState.SetAnimation(0, AnimationKeys.B2_ATTACK_1, false);
                phaseFirst = true;
                break;

            case AnimationKeys.B2_ATTACK_1:
                phaseFirst = false;
                ske.AnimationState.SetAnimation(0, AnimationKeys.B2_ATTACK_2, false);
                phaseSecond = true;
                break;

            case AnimationKeys.B2_ATTACK_2:
                phaseSecond = false;
                ske.AnimationState.SetAnimation(0, AnimationKeys.B2_ATTACK_3_READY, false);
                break;

            case AnimationKeys.B2_ATTACK_3_READY:
                Dash();
                ske.AnimationState.SetAnimation(0, AnimationKeys.B2_ATTACK_3_MOVE, false);
                break;

            case AnimationKeys.B2_ATTACK_3_MOVE:
                phaseFour = true;
                ske.AnimationState.SetAnimation(0, AnimationKeys.B2_ATTACK_4, false);
                break;

            case AnimationKeys.B2_ATTACK_4:
                phaseFour = false;
                ske.AnimationState.SetAnimation(0, AnimationKeys.B2_ATTACK_5, false);
                phaseFive = true;
                break;

            case AnimationKeys.B2_ATTACK_5:
                phaseFive = false;
                shouldMove = true;
                ske.AnimationState.SetAnimation(0, AnimationKeys.B1_MOVE, false);
                break;

            case AnimationKeys.B2_DIE:
                Destroy(gameObject);
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

        if (phaseFirst)
        {
            PhaseFirst();
        }

        if (phaseSecond)
        {
            PhaseSecond();
        }

        if (phaseFour)
        {
            PhaseFour();
        }

        if (phaseFive)
        {
            PhaseFive();
        }
    }

    public void Moving()
    {
        if (bossController.currentHealth > 0 && shouldMove == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerController.Ins.transform.position, moveSpeed * Time.deltaTime);
            moveDirection.Normalize();
        }
    }
    public void PhaseFirst()
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
    public void PhaseSecond()
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
    public void Dash()
    {
        var pos = PlayerController.Ins.transform.localPosition;
        transform.DOMove(pos, .6f);
    }
    public void PhaseFour()
    {
        if (shootCounter <= 0)
        {
            shootCounter = fireRateFour;

            foreach (Transform t in shotPointsFour)
            {
                Instantiate(bulletFour, t.position, t.rotation);
            }
        }
    }
    public void PhaseFive()
    {
        if (shootCounter <= 0)
        {
            shootCounter = fireRateFive;
            foreach (Transform t in shotPointsFive)
            {
                Instantiate(bulletFive, t.position, t.rotation);
            }
        }
    }
}
