using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float speed = 6.0f;
    public float sprintMultiplier = 2.0f; // Умножитель для ускорения
    public float jumpHeight = 2.0f;
    public float gravity = -9.81f;
    public float groundCheckRadius = 0.4f; // Радиус для проверки касания земли
    public Transform groundCheck;          // Точка для проверки земли
    public LayerMask groundMask;           // Маска слоя для земли

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;               // Флаг для проверки на земле
    private bool isJumping;

    public PlayerAnimation playerAnimation; // Добавляем ссылку на PlayerAnimation

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Проверка касания земли с помощью CheckSphere
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            isJumping = false; // Сбрасываем флаг прыжка при приземлении
            playerAnimation.Idle(true); // Если на земле, ставим состояние Idle
            playerAnimation.Jump(false);
            playerAnimation.Run(false);
            playerAnimation.Walk(false);
        }
        else
        {
            playerAnimation.Idle(false); // Если не на земле, убираем Idle
        }

        // Движение по плоскости X и Z
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Изменение: проверка нажатия клавиши Shift для ускорения
        float currentSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift)) // Проверяем, удерживается ли клавиша Shift
        {
            currentSpeed *= sprintMultiplier; // Увеличиваем скорость при удерживании Shift
            playerAnimation.Run(true); // Если бежим, устанавливаем состояние Run
            playerAnimation.Walk(false);
            playerAnimation.Idle(false);
        }
        else if (move.magnitude > 0) // Если есть движение
        {
            playerAnimation.Walk(true); // Если движемся, устанавливаем состояние Walk
            playerAnimation.Run(false);
            playerAnimation.Idle(false);
        }
        else
        {
            playerAnimation.Walk(false); // Если не движемся, убираем состояние Walk
            playerAnimation.Run(false); // Убираем состояние Run
        }

        controller.Move(move * currentSpeed * Time.deltaTime);

        // Прыжок при нажатии на пробел, если персонаж на земле и не в прыжке
        if (Input.GetButtonDown("Jump") && isGrounded && !isJumping)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJumping = true; // Устанавливаем флаг прыжка
            playerAnimation.Jump(true); // Устанавливаем состояние Jump
        }

        // Применение гравитации
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Если персонаж на земле, убираем состояние Jump
        if (isGrounded && !isJumping)
        {
            playerAnimation.Jump(false); // Убираем состояние Jump, когда приземляемся
        }
    }
}
