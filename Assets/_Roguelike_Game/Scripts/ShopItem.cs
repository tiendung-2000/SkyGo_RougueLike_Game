using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public GameObject buyMessage;

    private bool inBuyZone;

    public bool isHealthRestore, isHealthUpgrade, isWeapon;

    public int itemCost;

    public int healthUpgradeAmount;

    public Gun[] potentialGuns;
    private Gun theGun;
    public SpriteRenderer gunSprite;
    public Text inforText;

    // Start is called before the first frame update
    void Start()
    {
        if (isWeapon)
        {
            int selectedGun = Random.Range(0, potentialGuns.Length);
            theGun = potentialGuns[selectedGun];

            gunSprite.sprite = theGun.gunShopSprite;
            inforText.text = theGun.weaponName + "\n - " + theGun.itemCost + " Gold - ";
            itemCost = theGun.itemCost;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inBuyZone)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (LevelManager.instance.currentCoins >= itemCost)
                {
                    LevelManager.instance.SpendCoins(itemCost);

                    if (isHealthRestore)
                    {
                        PlayerHealthController.Ins.HealPlayer(PlayerHealthController.Ins.maxHealth);

                    }

                    if (isHealthUpgrade)
                    {
                        PlayerHealthController.Ins.IncreaseMaxHealth(healthUpgradeAmount);
                    }

                    if (isWeapon)
                    {
                        Gun gunClone = Instantiate(theGun);
                        gunClone.transform.parent = PlayerController.Ins.gunArm;
                        gunClone.transform.position = PlayerController.Ins.gunArm.position;
                        gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                        gunClone.transform.localScale = Vector3.one;

                        PlayerController.Ins.availableGuns.Add(gunClone);
                        PlayerController.Ins.currentGun = PlayerController.Ins.availableGuns.Count - 1;
                        PlayerController.Ins.SwitchGun();
                    }

                    gameObject.SetActive(false);
                    inBuyZone = false;

                    AudioManager.instance.PlaySFX(18);
                }
                else
                {
                    AudioManager.instance.PlaySFX(19);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buyMessage.SetActive(true);

            inBuyZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buyMessage.SetActive(false);

            inBuyZone = false;
        }
    }
}
