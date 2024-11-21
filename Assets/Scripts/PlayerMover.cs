using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float speed = 6.0f;
    public float sprintMultiplier = 2.0f;
    public float jumpHeight = 2.0f;
    public float gravity = -9.81f;
    public float groundCheckRadius = 0.4f;
    public Transform groundCheck;
    public LayerMask groundMask;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isJumping;

    public PlayerAnimation playerAnimation;
    public Transform cameraTransform;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Проверка, находится ли игрок на земле
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            if (isJumping)
            {
                isJumping = false;
                playerAnimation.Jump(false); // Выключаем анимацию прыжка при приземлении
            }
        }

        // Получение входных данных для движения
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            // Вычисление направления поворота
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float smoothAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, 0.1f);
            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

            // Приведение движения к направлению камеры
            Vector3 move = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            // Скорость бега
            float currentSpeed = speed;
            controller.Move(move.normalized * currentSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.LeftShift) && !isJumping)
            {
                currentSpeed *= sprintMultiplier;
                controller.Move(move.normalized * currentSpeed * Time.deltaTime);

                // Анимация бега
                playerAnimation.Run(true);
                playerAnimation.Walk(false);
                playerAnimation.Idle(false);
            }
            else if(!isJumping)
            {

                // Анимация ходьбы
                playerAnimation.Walk(true);
                playerAnimation.Run(false);
                playerAnimation.Idle(false);
            }
        }
        else
        {
            // Если нет движения, включаем Idle
            playerAnimation.Walk(false);
            playerAnimation.Run(false);
            if (!isJumping)
            {
                playerAnimation.Idle(true);
            }
        }

        // Прыжок
        if (Input.GetButtonDown("Jump") && isGrounded && !isJumping)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJumping = true;
            playerAnimation.Jump(true); // Включаем анимацию прыжка
            playerAnimation.Walk(false); // Выключаем другие анимации
            playerAnimation.Run(false);
            playerAnimation.Idle(false);
        }

        // Применение гравитации
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Если приземлились
        if (isGrounded && !isJumping && velocity.y < 0)
        {
            playerAnimation.Jump(false); // Убираем состояние прыжка
        }
    }
}
