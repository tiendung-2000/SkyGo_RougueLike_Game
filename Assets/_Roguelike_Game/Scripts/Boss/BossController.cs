using API.UI;
using Spine.Unity;
using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController Ins;
    public SkeletonAnimation ske;

    public float animationDuration;

    public int curDamage = 1;

    [Header("Boss")]
    public float currentHealth;
    
    public GameObject hitEffect;
    public GameObject levelExit;

    //public bool playerOnZone = false;

    private void Awake()
    {
        if (Ins == null)
        {
            Ins = this;
        }
    }

    // Start is called before the first frame update
    //void ()
    //{
    //}

    private void OnEnable()
    {
        //UIController.Ins.bossHub.SetActive(true);
        StartCoroutine(IEGetBossData());
    }

    IEnumerator IEGetBossData()
    {
        yield return new WaitForSeconds(0.5f);
        BossHubUI.Ins.bossHealthBar.maxValue = currentHealth;
        BossHubUI.Ins.bossHealthBar.value = currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            ske.AnimationState.SetAnimation(0, AnimationKeys.DIE, false);

            StartCoroutine(DisableBossHub());
            CanvasManager.Ins.CloseUI(UIName.BossHubUI);

        }
        BossHubUI.Ins.bossHealthBar.value = currentHealth;
    }

    IEnumerator DisableBossHub()
    {
        yield return new WaitForSeconds(animationDuration);
        Destroy(gameObject);
        levelExit.SetActive(true);
    }
}
