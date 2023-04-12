using API.UI;
using UnityEngine;
using UnityEngine.UI;

public class BossHubUI : BaseUIMenu
{
    public static BossHubUI Ins;

    //public GameObject bossHub;
    public Slider bossHealthBar;

    private void Awake()
    {
        Ins = this;
    }
}
