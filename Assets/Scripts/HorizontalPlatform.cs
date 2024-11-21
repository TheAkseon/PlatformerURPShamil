using UnityEngine;

public class HorizontalPlatform : MonoBehaviour
{
    public float speed = 2.0f; // Скорость движения платформы
    public float moveDistance = 5.0f; // Максимальное расстояние от начальной позиции в одну сторону

    private Vector3 startPosition; // Начальная позиция платформы
    private int direction = 1; // Направление движения (1 - вправо, -1 - влево)

    private void Start()
    {
        // Сохраняем начальную позицию платформы
        startPosition = transform.position;
    }

    private void Update()
    {
        // Вычисляем новое положение платформы
        Vector3 newPosition = transform.position;
        newPosition.x += speed * direction * Time.deltaTime;

        // Проверяем, не вышла ли платформа за пределы диапазона
        if (Mathf.Abs(newPosition.x - startPosition.x) > moveDistance)
        {
            // Меняем направление движения
            direction *= -1;
        }

        // Применяем новое положение
        transform.position = newPosition;
    }
}
