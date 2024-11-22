using UnityEngine;

public class PlatformCollisionHandler : MonoBehaviour
{
    public CharacterController PlayerController;
    public Transform RespawnPlace;
    public SceneChanger SceneChanger;

    private void Start()
    {
        PlayerController = FindObjectOfType<CharacterController>();
        SceneChanger = FindObjectOfType<SceneChanger>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out HorizontalPlatform horizontalPlatform))
        {
            // Отключаем управление игроком
            PlayerController.enabled = false;

            // Перемещаем игрока к точке возрождения
            PlayerController.transform.position = RespawnPlace.position;

            // Включаем управление обратно
            PlayerController.enabled = true;
        }
        else if (collision.gameObject.TryGetComponent(out ReloadPlatform reloadPlatform))
        {
            SceneChanger.ReloadCurrentScene();
        }
    }
}
