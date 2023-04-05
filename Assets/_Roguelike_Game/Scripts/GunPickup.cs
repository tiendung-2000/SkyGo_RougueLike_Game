using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public static GunPickup instance;

    public Gun theGun;

    public float waitToBeColleted = .5f;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
            bool hasGun = false;
            foreach (Gun gunToCheck in PlayerController.Ins.availableGuns)
            {
                if (theGun.weaponName == gunToCheck.weaponName)
                {
                    hasGun = true;
                }
            }

            if (!hasGun)
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

            Destroy(gameObject);

            AudioManager.instance.PlaySFX(7);
        }
    }
}
