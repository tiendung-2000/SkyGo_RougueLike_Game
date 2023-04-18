using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;
    public SkeletonAnimation ske;
    [SerializeField] float animationDuration;

    [Header("==========Chase Player==========")]
    public bool shouldChasePlayer;
    public float rangerToChasePlayer;
    private Vector3 moveDirection;

    [Header("==========Run Away==========")]
    public bool shouldRunAway;
    public float runAwayRange;

    [Header("==========Wandering==========")]
    public bool shouldWander;
    public float wanderLength, pauseLength;
    private float wanderCouter, pauseCouter;
    private Vector3 wanderDirection;

    [Header("==========Shooting==========")]
    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;
    public float shootRange;

    [Header("==========Variables==========")]
    public int health = 150;
    public GameObject[] deathSplatter;
    public GameObject hitEffect;

    [Header("==========Drop Item==========")]
    public bool shouldDropItem;
    public GameObject[] itemToDrop;
    public float itemDropPercent;

    void Start()
    {
        if (shouldWander)
        {
            pauseCouter = Random.Range(pauseLength * .75f, pauseLength * 1.25f);
        }

        //if (shouldChasePlayer || shouldRunAway || shouldWander)
        //{
        //    ske.AnimationState.SetAnimation(0, "Move", true);
        //}
        //else
        //{
        //    ske.AnimationState.SetAnimation(0, "Idel", true);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.Ins.gameObject.activeInHierarchy)
        {
            if (Vector3.Distance(transform.position, PlayerController.Ins.transform.position) < rangerToChasePlayer && shouldChasePlayer)
            {
                moveDirection = PlayerController.Ins.transform.position - transform.position;
            }
            else
            {
                if (shouldWander)
                {
                    if (wanderCouter > 0)
                    {
                        wanderCouter -= Time.deltaTime;

                        //move the enemy
                        moveDirection = wanderDirection;

                        if (wanderCouter <= 0)
                        {
                            pauseCouter = Random.Range(pauseLength * .75f, pauseLength * 1.25f);
                        }
                    }

                    if (pauseCouter > 0)
                    {
                        pauseCouter -= Time.deltaTime;
                        if (pauseCouter <= 0)
                        {
                            wanderCouter = Random.Range(wanderLength * .75f, wanderLength * 1.25f);

                            wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                        }
                    }
                }
            }

            if (shouldRunAway && Vector3.Distance(transform.position, PlayerController.Ins.transform.position) < runAwayRange)
            {
                moveDirection = transform.position - PlayerController.Ins.transform.position;
            }
            //else
            //{
            //    moveDirection = Vector3.zero;
            //}

            moveDirection.Normalize();

            theRB.velocity = moveDirection * moveSpeed;


            if (shouldShoot && Vector3.Distance(transform.position, PlayerController.Ins.transform.position) < shootRange)
            {
                fireCounter -= Time.deltaTime;

                if (fireCounter <= 0)
                {
                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    AudioManager.instance.PlaySFX(13);
                }
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
        }

        //if (moveDirection != Vector3.zero)
        //{
        //    ske.AnimationState.SetAnimation(0, "Move", true);
        //}

        //Flip enemy
        if (this.transform.position.x > PlayerController.Ins.transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }


    }

    public void DamageEnemy(int damage)
    {
        health -= damage;

        AudioManager.instance.PlaySFX(2);

        Instantiate(hitEffect, transform.position, transform.rotation);

        if (health <= 0)
        {
            moveSpeed = 0f;
            ske.AnimationState.SetAnimation(0, "Die", false);
            StartCoroutine(IEDestroy());

            AudioManager.instance.PlaySFX(1);

            int selectedSplatter = Random.Range(0, deathSplatter.Length);

            int rotation = Random.Range(0, 4);

            Instantiate(deathSplatter[selectedSplatter], transform.position, Quaternion.Euler(0f, 0f, rotation * 90f));

            if (shouldDropItem)
            {
                float dropChance = Random.Range(0f, 100f);

                if (dropChance < itemDropPercent)
                {
                    int randomItem = Random.Range(0, itemToDrop.Length);

                    Instantiate(itemToDrop[randomItem], transform.position, transform.rotation);
                }
            }
        }
    }

    IEnumerator IEDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
