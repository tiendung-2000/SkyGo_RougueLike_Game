using DG.Tweening;
using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamelessThingBoss : MonoBehaviour
{
    public static NamelessThingBoss Ins;

    BossController bossController;

    public SkeletonAnimation ske;

    [Header("Shooting")]
    public float xAngle;
    public float yAngle;

    public int dashCount;

    public Transform[] pointToMove;

    public Transform[] shotPointsFirst;
    public Transform[] shotPointsThird;
    public Transform[] shotPointsFour;

    public GameObject bossExplode;
    public GameObject bulletFirst;
    public GameObject bulletThird;
    public GameObject bulletFour;

    public float fireRateFirst;
    public float fireRateThird;
    public float fireRateFour;

    public float shootCounter;

    public bool phaseFirst = false;
    public bool phaseThird = false;
    public bool phaseFour = false;
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

        ske.AnimationState.Complete += AnimationState_Complete;

        ske.AnimationState.SetAnimation(0, "Idle", false);
    }

    private void AnimationState_Complete(TrackEntry trackEntry)
    {
        switch (trackEntry.Animation.Name)
        {
            case AnimationKeys.B3_IDLE:
                ske.AnimationState.SetAnimation(0, AnimationKeys.B3_SKILL_1_ATTACK, false);
                break;

            case AnimationKeys.B3_SKILL_1_ATTACK:
                ske.AnimationState.SetAnimation(0, AnimationKeys.B3_SKILL_2_READY, false);
                break;

            case AnimationKeys.B3_SKILL_2_READY:
                Dash();
                ske.AnimationState.SetAnimation(0, AnimationKeys.B3_SKILL_2_MOVE, false);
                break;

            case AnimationKeys.B3_SKILL_2_MOVE:

                //DOVirtual.DelayedCall(7.098f, () =>
                //{
                //    ske.AnimationState.SetAnimation(0, AnimationKeys.B3_SKILL_3_ATTACK, false);
                //});
                if (dashCount == 3)
                {
                    ske.AnimationState.SetAnimation(0, AnimationKeys.B3_SKILL_3_ATTACK, false);
                }
                else
                {
                    ske.AnimationState.SetAnimation(0, AnimationKeys.B3_SKILL_2_READY, false);
                }
                break;

            case AnimationKeys.B3_SKILL_3_ATTACK:
                dashCount = 0;
                ske.AnimationState.SetAnimation(0, AnimationKeys.B3_SKILL_4_ATTACK, false);
                break;

            case AnimationKeys.B3_SKILL_4_ATTACK:
                ske.AnimationState.SetAnimation(0, AnimationKeys.B3_SKILL_5_READY, false);
                break;

            case AnimationKeys.B3_SKILL_5_READY:
                GetComponent<MeshRenderer>().sortingLayerName = "Default";
                DOVirtual.DelayedCall(2f, () =>
                {
                    GetComponent<MeshRenderer>().sortingLayerName = "Enemies";
                    ske.AnimationState.SetAnimation(0, AnimationKeys.B3_SKILL_5_MOVE, false);
                });
                break;

            case AnimationKeys.B3_SKILL_5_MOVE:
                ske.AnimationState.SetAnimation(0, AnimationKeys.B3_IDLE, false);
                break;

            case AnimationKeys.B3_DIE:
                Destroy(gameObject);
                break;
        }
    }

    private void Update()
    {
        shootCounter -= Time.deltaTime;
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
    public void PhaseThird()
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
    public void Dash()
    {
        //var pos = PlayerController.Ins.transform.localPosition;
        //transform.DOMove(pos, 0f);

        //test
        if (transform.parent.GetComponent<RoomCenter>().checkPoint != null)
        {
            transform.parent.GetComponent<RoomCenter>().GetTarget();
            transform.localPosition = transform.parent.GetComponent<RoomCenter>().checkPoint.localPosition;
            dashCount++;

        }


    }

    private void GetTarget()
    {

    }

    public void Teleport()
    {
        //Hold 2s
        gameObject.tag = "Untagged";
        gameObject.layer = 0;
        DOVirtual.DelayedCall(2, () =>
        {
            gameObject.tag = "Boss";
            gameObject.layer = 10;

            gameObject.transform.position = pointToMove[(Random.Range(0, pointToMove.Length - 1))].position;

            DOVirtual.DelayedCall(.5f, () =>
            {
                Instantiate(bossExplode, transform.position, transform.rotation);
                DOVirtual.DelayedCall(.5f, () =>
                {
                    Instantiate(bossExplode, transform.position, transform.rotation);
                    DOVirtual.DelayedCall(.5f, () =>
                    {
                        Instantiate(bossExplode, transform.position, transform.rotation);
                    });
                });
            });
        });
    }

    ////==================SETING ACTION==================//

    //public void ShootAction(int type)
    //{
    //    // type == 0 => ban 5 vien dan || type == 1 => ban 3 vien dan
    //    if (bossController.currentHealth > 0)
    //    {
    //        if (type == 1)
    //        {
    //            if (shoot1 != null) StopCoroutine(shoot1);
    //            shoot1 = StartCoroutine(DelayShootFirst());
    //        }
    //        else if (type == 2)
    //        {
    //            if (dash != null) StopCoroutine(dash);
    //            dash = StartCoroutine(DelayDash());
    //        }
    //        else if (type == 3)
    //        {
    //            if (shoot3 != null) StopCoroutine(shoot3);
    //            shoot3 = StartCoroutine(DelayShootThird());
    //        }
    //        else if (type == 4)
    //        {
    //            if (shoot4 != null) StopCoroutine(shoot4);
    //            shoot4 = StartCoroutine(DelayShootFour());
    //        }
    //        else if (type == 5)
    //        {
    //            if (tele != null) StopCoroutine(tele);
    //            tele = StartCoroutine(DelayTele());
    //        }
    //    }
    //}

    //IEnumerator Starting()
    //{
    //    yield return new WaitForSeconds(delayStarting);
    //    phaseFirst = false;
    //    ShootAction(1);
    //}

    //Coroutine shoot1;
    //IEnumerator DelayShootFirst()
    //{
    //    phaseFirst = true;
    //    yield return new WaitForSeconds(2f);
    //    phaseFirst = false;
    //    DOVirtual.DelayedCall(1, () =>
    //    {
    //        ShootAction(2);
    //    });
    //}

    //Coroutine dash;
    //IEnumerator DelayDash()
    //{
    //    DOVirtual.DelayedCall(.5f, () =>
    //    {
    //        Dash();
    //        DOVirtual.DelayedCall(.5f, () =>
    //        {
    //            Dash();
    //            DOVirtual.DelayedCall(.5f, () =>
    //            {
    //                Dash();
    //            });
    //        });
    //    });

    //    //for (int i = 0; i <= dashLoop; i++)
    //    //{
    //    //    Debug.Log("vao day");
    //    //    DOVirtual.DelayedCall(.5f, () =>
    //    //        {
    //    //            Dash();
    //    //        });
    //    //}
    //    yield return new WaitForSeconds(delayAction);
    //    DOVirtual.DelayedCall(2, () =>
    //    {
    //        ShootAction(3);
    //    });
    //}

    //Coroutine shoot3;
    //IEnumerator DelayShootThird()
    //{
    //    phaseThird = true;
    //    yield return new WaitForSeconds(delayAction);
    //    phaseThird = false;
    //    DOVirtual.DelayedCall(1, () =>
    //    {
    //        ShootAction(4);
    //    });
    //}

    //Coroutine shoot4;
    //IEnumerator DelayShootFour()
    //{
    //    phaseFour = true;
    //    yield return new WaitForSeconds(3f);
    //    phaseFour = false;
    //    DOVirtual.DelayedCall(1, () =>
    //    {
    //        ShootAction(5);
    //    });
    //}

    //Coroutine tele;
    //IEnumerator DelayTele()
    //{
    //    Teleport();
    //    yield return new WaitForSeconds(3f);
    //    DOVirtual.DelayedCall(2, () =>
    //    {
    //        ShootAction(1);
    //    });

    //}
}
