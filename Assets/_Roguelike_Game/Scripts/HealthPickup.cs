using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 1;

    public float waitToBeColleted =.5f;

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
        if(other.tag == "Player" && waitToBeColleted <= 0)
        {
            PlayerHealthController.Ins.HealPlayer(healAmount);

            Destroy(gameObject);

            AudioManager.instance.PlaySFX(7);
        }
    }
}
