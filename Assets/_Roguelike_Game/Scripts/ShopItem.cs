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

    public Weapon[] potentialWeapon;
    private Weapon theWeapon;
    public SpriteRenderer weaponSprite;
    public Text inforText;

    void Start()
    {
        if (isWeapon)
        {
            int selectedGun = Random.Range(0, potentialWeapon.Length);
            theWeapon = potentialWeapon[selectedGun];

            weaponSprite.sprite = theWeapon.weaponShopSprite;
            inforText.text = theWeapon.weaponName + "\n - " + theWeapon.itemCost + " Gold - ";
            itemCost = theWeapon.itemCost;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inBuyZone)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (CharacterTracker.Ins.currentCoins >= itemCost)
                {
                    CharacterTracker.Ins.SpendCoins(itemCost);

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
                        Weapon gunClone = Instantiate(theWeapon);
                        gunClone.transform.parent = PlayerController.Ins.playerHand;
                        gunClone.transform.position = PlayerController.Ins.playerHand.position;
                        gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                        gunClone.transform.localScale = Vector3.one;

                        PlayerController.Ins.availableWeapons.Add(gunClone);
                        PlayerController.Ins.currentWeapon = PlayerController.Ins.availableWeapons.Count - 1;
                        PlayerController.Ins.SwitchWeapon();
                    }

                    gameObject.SetActive(false);
                    inBuyZone = false;

                    AudioController.instance.PlaySFX(18);
                }
                else
                {
                    AudioController.instance.PlaySFX(19);
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
