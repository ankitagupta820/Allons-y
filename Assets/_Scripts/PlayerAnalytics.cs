using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
public class PlayerAnalytics : MonoBehaviour
{
    public void RunEvent()
    {
        int totalPotions = 5;
        int totalCoins = 100;
#if ENABLE_CLOUD_SERVICES_ANALYTICS
        Debug.Log("clicked");
        Debug.Log(Analytics.CustomEvent("gameOver", new Dictionary<string, object>
        {
            { "potions", totalPotions },
            { "coins", totalCoins }
        }));
#endif

    }
}
