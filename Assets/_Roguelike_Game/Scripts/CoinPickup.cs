using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1;

    public float waitToBeColleted = .5f;
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
            CharacterTracker.Ins.GetCoins(coinValue);

            Destroy(gameObject);

            AudioController.instance.PlaySFX(5);
        }
    }
}
