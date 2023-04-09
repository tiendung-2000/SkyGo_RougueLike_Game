using API.UI;
using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController Ins;
    public SkeletonAnimation ske;

    [Header("Boss")]
    public float currentHealth;
    //public GameObject deathEffect;
    public GameObject hitEffect;

    //public BossHubUI bossHubUI;
    //public GameObject levelExit;

    public bool playerOnZone = false;

    private void Awake()
    {
        if (Ins == null)
        {
            Ins = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerOnZone = false;
    }

    private void OnEnable()
    {
        UIController.instance.bossHealthBar.maxValue = currentHealth;
        UIController.instance.bossHealthBar.value = currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            ske.AnimationState.SetAnimation(0, AnimationKeys.DIE, false);
            //CanvasManager.Ins.CloseUI(UIName.BossHubUI);
        }
        UIController.instance.bossHealthBar.value = currentHealth;
    }
}
