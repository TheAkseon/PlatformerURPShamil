using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float speed = 6.0f;
    public float sprintMultiplier = 2.0f; // ���������� ��� ���������
    public float jumpHeight = 2.0f;
    public float gravity = -9.81f;
    public float groundCheckRadius = 0.4f; // ������ ��� �������� ������� �����
    public Transform groundCheck;          // ����� ��� �������� �����
    public LayerMask groundMask;           // ����� ���� ��� �����

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;               // ���� ��� �������� �� �����
    private bool isJumping;

    public PlayerAnimation playerAnimation; // ��������� ������ �� PlayerAnimation

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // �������� ������� ����� � ������� CheckSphere
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            isJumping = false; // ���������� ���� ������ ��� �����������
            playerAnimation.Idle(true); // ���� �� �����, ������ ��������� Idle
            playerAnimation.Jump(false);
            playerAnimation.Run(false);
            playerAnimation.Walk(false);
        }
        else
        {
            playerAnimation.Idle(false); // ���� �� �� �����, ������� Idle
        }

        // �������� �� ��������� X � Z
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // ���������: �������� ������� ������� Shift ��� ���������
        float currentSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift)) // ���������, ������������ �� ������� Shift
        {
            currentSpeed *= sprintMultiplier; // ����������� �������� ��� ����������� Shift
            playerAnimation.Run(true); // ���� �����, ������������� ��������� Run
            playerAnimation.Walk(false);
            playerAnimation.Idle(false);
        }
        else if (move.magnitude > 0) // ���� ���� ��������
        {
            playerAnimation.Walk(true); // ���� ��������, ������������� ��������� Walk
            playerAnimation.Run(false);
            playerAnimation.Idle(false);
        }
        else
        {
            playerAnimation.Walk(false); // ���� �� ��������, ������� ��������� Walk
            playerAnimation.Run(false); // ������� ��������� Run
        }

        controller.Move(move * currentSpeed * Time.deltaTime);

        // ������ ��� ������� �� ������, ���� �������� �� ����� � �� � ������
        if (Input.GetButtonDown("Jump") && isGrounded && !isJumping)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJumping = true; // ������������� ���� ������
            playerAnimation.Jump(true); // ������������� ��������� Jump
        }

        // ���������� ����������
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // ���� �������� �� �����, ������� ��������� Jump
        if (isGrounded && !isJumping)
        {
            playerAnimation.Jump(false); // ������� ��������� Jump, ����� ������������
        }
    }
}
