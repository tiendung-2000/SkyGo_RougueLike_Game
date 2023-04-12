using UnityEngine;

public class CharacterTracker : Singleton<CharacterTracker>
{
    public int currentHealth, maxHealth, currentCoins;

    private void Start()
    {
        UIController.Ins.cointText.text = currentCoins.ToString();
    }

    public void GetCoins(int amount)
    {
        currentCoins += amount;
        UIController.Ins.cointText.text = currentCoins.ToString();
    }

    public void SpendCoins(int amount)
    {
        currentCoins -= amount;

        if (currentCoins < 0)
        {
            currentCoins = 0;
        }
        UIController.Ins.cointText.text = currentCoins.ToString();
    }
}
