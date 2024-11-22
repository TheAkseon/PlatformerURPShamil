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
            // ��������� ���������� �������
            PlayerController.enabled = false;

            // ���������� ������ � ����� �����������
            PlayerController.transform.position = RespawnPlace.position;

            // �������� ���������� �������
            PlayerController.enabled = true;
        }
        else if (collision.gameObject.TryGetComponent(out ReloadPlatform reloadPlatform))
        {
            SceneChanger.ReloadCurrentScene();
        }
    }
}
