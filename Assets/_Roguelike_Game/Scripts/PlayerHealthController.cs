using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController Ins;

    public int currentHealth;
    public int maxHealth;

    public float damageInvincLength = 1f;
    private float invincCount;

    private void Awake()
    {
        Ins = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = CharacterTracker.Ins.maxHealth;
        currentHealth = CharacterTracker.Ins.currentHealth;

        UIController.Ins.healthSlider.maxValue = maxHealth;
        UIController.Ins.healthSlider.value = currentHealth;
        UIController.Ins.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincCount > 0)
        {
            invincCount -= Time.deltaTime;

            if (invincCount <= 0)
            {
                PlayerController.Ins.bodySR.color = new Color(PlayerController.Ins.bodySR.color.r, PlayerController.Ins.bodySR.color.g, PlayerController.Ins.bodySR.color.b, 1f);
            }
        }
    }

    public void DamagePlayer()
    {
        if (invincCount <= 0)
        {
            AudioManager.instance.PlaySFX(11);
            currentHealth--;

            invincCount = damageInvincLength;

            PlayerController.Ins.bodySR.color = new Color(PlayerController.Ins.bodySR.color.r, PlayerController.Ins.bodySR.color.g, PlayerController.Ins.bodySR.color.b, .5f);

            if (currentHealth <= 0)
            {
                PlayerController.Ins.gameObject.SetActive(false);

                StartCoroutine(ShowLose());

                AudioManager.instance.PlayGameOver();

                AudioManager.instance.PlaySFX(9);
            }

            UIController.Ins.healthSlider.value = currentHealth;
            UIController.Ins.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
        }
    }

    IEnumerator ShowLose()
    {
        yield return new WaitForSeconds(1f);
        UIController.Ins.deathScreen.SetActive(true);
    }

    public void MakeInvincible(float length)
    {
        invincCount = length;
        PlayerController.Ins.bodySR.color = new Color(PlayerController.Ins.bodySR.color.r, PlayerController.Ins.bodySR.color.g, PlayerController.Ins.bodySR.color.b, .5f);

    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.Ins.healthSlider.value = currentHealth;
        UIController.Ins.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;

        currentHealth = maxHealth;

        UIController.Ins.healthSlider.maxValue = maxHealth;
        UIController.Ins.healthSlider.value = currentHealth;
        UIController.Ins.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
