using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float walkingSpeed = 7.5f;
    [SerializeField] private float runningSpeed = 11.5f;
    [SerializeField] private float lookSpeed = 2.0f;
    [SerializeField] private float lookLimit = 45.0f;
    [SerializeField] private Camera playerCamera;

    [Header("Player Arms")]
    [SerializeField] private Transform rightArm;
    [SerializeField] private Transform leftArm;
    [SerializeField] private Transform baseballBat;

    [Header("Arm Offset Settings")]
    [SerializeField] private Vector3 rightArmOffset;
    [SerializeField] private Vector3 leftArmOffset;

    [Header("Animator")]
    [SerializeField] private Animator playerAnim;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    [HideInInspector] public bool isRunning = false;
    [HideInInspector] public bool playRunAnim = false;
    [HideInInspector] public bool isMoving = false;
    [HideInInspector] public bool isJumping = false;
    [HideInInspector] public bool isStrafing = false;
    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public bool isHeavyAttacking = false;

    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool canRun = true;

    [HideInInspector] public Transform respawnPoint;

    public GameObject gameMenuUI;
    private GameMenu gameMenu;
    private PlayerHealth playerHealth;

    private float gravity = 20.0f;
    [SerializeField] private float jumpForce = 10;
    private float verticalVelocity = 0;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        gameMenu = gameMenuUI.GetComponent<GameMenu>();
        playerHealth = GetComponent<PlayerHealth>();

        if (playerAnim == null)
        {
            playerAnim = GetComponent<Animator>();
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!gameMenu.isPaused)
        {
            HandleMovement();
            ApplyGravity();
            MoveCharacter();
            HandleCameraRotation();
        }
    }

    private void HandleMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunningAttempt = Input.GetKey(KeyCode.LeftShift) && canRun;
        isRunning = isRunningAttempt && playerHealth.playerStamina > 0;

        playerHealth.SetRunning(isRunning);

        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;

        isStrafing = Mathf.Abs(Input.GetAxis("Horizontal")) > 0 && Mathf.Abs(Input.GetAxis("Vertical")) == 0;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        isMoving = moveDirection.magnitude > 0 && !isStrafing && !isRunning;
        playRunAnim = isRunning && moveDirection.magnitude > 0;

        isJumping = characterController.isGrounded && Input.GetKey(KeyCode.Space);

        if (isJumping)
        {
            verticalVelocity += jumpForce;
            playerAnim.SetTrigger("jump");
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }

    private void MoveCharacter()
    {
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = 0; // Reset vertical velocity when grounded
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime; // Apply gravity
        }

        moveDirection.y = verticalVelocity;
    }

    private void HandleCameraRotation()
    {
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookLimit, lookLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
