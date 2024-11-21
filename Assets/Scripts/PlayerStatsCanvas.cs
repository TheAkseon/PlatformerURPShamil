using TMPro;
using UnityEngine;

public class PlayerStatsCanvas : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;

    public void UpdateMoney(int newMoneyCount)
    {
        MoneyText.text = newMoneyCount.ToString();
    }
}
