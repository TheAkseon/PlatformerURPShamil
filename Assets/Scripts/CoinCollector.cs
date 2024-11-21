using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public int CountCollectedCoin = 0;
    public PlayerStatsCanvas PlayerStatsCanvas;

    private void Start()
    {
        PlayerStatsCanvas = FindObjectOfType<PlayerStatsCanvas>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Coin coin))
        {
            CountCollectedCoin++;
            Destroy(coin.gameObject);
            PlayerStatsCanvas.UpdateMoney(CountCollectedCoin);
        }
    }
}
