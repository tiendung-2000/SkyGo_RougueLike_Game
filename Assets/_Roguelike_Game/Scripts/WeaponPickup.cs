using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public static WeaponPickup Ins;

    public Weapon theWeapon;

    public float waitToBeColleted = .5f;

    private void Awake()
    {
        if (Ins == null)
        {
            Ins = this;
        }
    }

    void Update()
    {
        if (waitToBeColleted > 0)
        {
            waitToBeColleted -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && waitToBeColleted <= 0)
        {
            bool hasWeapon = false;
            foreach (Weapon weaponCheck in PlayerController.Ins.availableWeapons)
            {
                if (theWeapon.weaponName == weaponCheck.weaponName)
                {
                    hasWeapon = true;

                    //tang damage neu nhat vu khi giong nhau
                }
            }

            if (!hasWeapon)
            {
                Weapon gunClone = Instantiate(theWeapon);
                gunClone.transform.parent = PlayerController.Ins.playerHand;
                gunClone.transform.position = PlayerController.Ins.playerHand.position;
                gunClone.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 45f));
                gunClone.transform.localScale = Vector3.one;

                PlayerController.Ins.availableWeapons.Add(gunClone);
                PlayerController.Ins.currentWeapon = PlayerController.Ins.availableWeapons.Count - 1;
                PlayerController.Ins.SwitchWeapon();
            }

            Destroy(gameObject);

            AudioManager.instance.PlaySFX(7);
        }
    }
}
