using Spine.Unity;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController Ins;
    public SkeletonAnimation ske;

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
        UIController.Ins.bossHub.SetActive(true);
        UIController.Ins.bossHealthBar.maxValue = currentHealth;
        UIController.Ins.bossHealthBar.value = currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            ske.AnimationState.SetAnimation(0, AnimationKeys.DIE, false);

            levelExit.SetActive(true);
            UIController.Ins.bossHub.SetActive(false);
        }
        UIController.Ins.bossHealthBar.value = currentHealth;
    }
}
