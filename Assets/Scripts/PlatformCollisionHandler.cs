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
            // ��������� ���������� �������
            PlayerController.enabled = false;

            // ���������� ������ � ����� �����������
            PlayerController.transform.position = RespawnPlace.position;

            // �������� ���������� �������
            PlayerController.enabled = true;
        }
    }
}
