using UnityEngine;

public class PlatformCollisionHandler : MonoBehaviour
{
    public CharacterController PlayerController;
    public Transform RespawnPlace;

    private void Start()
    {
        PlayerController = FindObjectOfType<CharacterController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out HorizontalPlatform horizontalPlatform))
        {
            // Отключаем управление игроком
            PlayerController.enabled = false;

            // Перемещаем игрока к точке возрождения
            PlayerController.transform.position = RespawnPlace.position;

            // Включаем управление обратно
            PlayerController.enabled = true;
        }
    }
}
