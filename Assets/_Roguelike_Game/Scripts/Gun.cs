using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    //public PlayerBullet playerBullet;

    public Transform firePoint;

    public float timeBetweenShots;
    private float shotCounter;

    public string weaponName;

    public int itemCost;
    public Sprite gunShopSprite;

    //=========================================//
    [SerializeField] GameObject ArrowPrefab; //bulet
    [SerializeField] SpriteRenderer ArrowGFX; //arrow image
    [SerializeField] Slider BowPowerSlider;

    //[Range(0, 10)]
    //[SerializeField] float BowPower;

    [Range(0, 1)]
    [SerializeField] float MaxBowCharge;

    float BowCharge;
    bool CanFire = true;

    private void Start()
    {
        BowPowerSlider.value = 0f;
        BowPowerSlider.maxValue = MaxBowCharge;

    }

    void Update()
    {
        if (Input.GetMouseButton(0) && CanFire)
        {
            ChargeBow();
        }
        else if (Input.GetMouseButtonUp(0) && CanFire)
        {
            FireBow();
        }
        else
        {
            if (BowCharge > 0f)
            {
                BowCharge -= 1f * Time.deltaTime;
            }
            else
            {
                BowCharge = 0f;
                CanFire = true;
            }

            BowPowerSlider.value = BowCharge;
        }

        if (shotCounter > 0)
        {
            shotCounter -= Time.deltaTime;
        }
    }

    void ChargeBow()
    {
        ArrowGFX.enabled = true;

        BowCharge += Time.deltaTime;

        BowPowerSlider.value = BowCharge;

        if (BowCharge > MaxBowCharge)
        {
            BowPowerSlider.value = MaxBowCharge;
        }
    }

    void FireBow()
    {
        if (BowCharge > MaxBowCharge) BowCharge = MaxBowCharge;
        if (PlayerController.Ins.canMove && !LevelManager.instance.isPaused)
        {
            if (shotCounter > 0)
            {
                shotCounter -= Time.deltaTime;
            }
            else
            {
                Instantiate(ArrowPrefab, firePoint.position, firePoint.rotation);
                shotCounter = timeBetweenShots;
                AudioManager.instance.PlaySFX(12);

                CanFire = false;
                ArrowGFX.enabled = false;
            }
        }


        //float ArrowSpeed = BowCharge + BowPower;

        //Instantiate(ArrowPrefab, firePoint.position, firePoint.rotation);
    }
}
