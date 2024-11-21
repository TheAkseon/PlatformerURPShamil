using UnityEngine;

public class HorizontalPlatform : MonoBehaviour
{
    public float speed = 2.0f; // �������� �������� ���������
    public float moveDistance = 5.0f; // ������������ ���������� �� ��������� ������� � ���� �������

    private Vector3 startPosition; // ��������� ������� ���������
    private int direction = 1; // ����������� �������� (1 - ������, -1 - �����)

    private void Start()
    {
        // ��������� ��������� ������� ���������
        startPosition = transform.position;
    }

    private void Update()
    {
        // ��������� ����� ��������� ���������
        Vector3 newPosition = transform.position;
        newPosition.x += speed * direction * Time.deltaTime;

        // ���������, �� ����� �� ��������� �� ������� ���������
        if (Mathf.Abs(newPosition.x - startPosition.x) > moveDistance)
        {
            // ������ ����������� ��������
            direction *= -1;
        }

        // ��������� ����� ���������
        transform.position = newPosition;
    }
}
