using UnityEngine;

public class CoinRotator : MonoBehaviour
{
    public float RotateSpeed = 500;

    private void Update()
    {
        transform.Rotate(0,0,RotateSpeed * Time.deltaTime);
    }
}
