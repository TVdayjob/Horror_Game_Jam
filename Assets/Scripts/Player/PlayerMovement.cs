using System.Collections;
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

    [Header("Arm Offset Settings")]
    [SerializeField] private Vector3 rightArmOffset;
    [SerializeField] private Vector3 leftArmOffset;

    [Header("Animator")]
    [SerializeField] private Animator playerAnim;

    [Header("Weapon")]
    [SerializeField] private GameObject baseballBat;
    [SerializeField] private float batDamage = 20f; // Damage value

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

    [HideInInspector] public Transform respawnPoint;

    public GameObject gameMenuUI;
    private GameMenu gameMenu;

    private float gravity = 20.0f;
    [SerializeField] private float jumpForce = 10;
    private float verticalVelocity = 0;

    private bool isHoldingBat = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        gameMenu = gameMenuUI.GetComponent<GameMenu>();
        if (playerAnim == null)
        {
            playerAnim = GetComponent<Animator>();
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Check if the player is holding the baseball bat at start
        isHoldingBat = baseballBat != null && baseballBat.activeInHierarchy;
        UpdateIdleAnimation();
    }

    void Update()
    {
        if (!gameMenu.isPaused)
        {
            HandleMovement();
            ApplyGravity();
            MoveCharacter();
            HandleCameraRotation();
            CheckForBaseballBat();
            HandleAttackInput();
        }
    }

    private void HandleMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        isRunning = Input.GetKey(KeyCode.LeftShift);
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

    private void MoveCharacter()
    {
        characterController.Move(moveDirection * Time.deltaTime);
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

    private void CheckForBaseballBat()
    {
        bool currentlyHoldingBat = baseballBat != null && baseballBat.activeInHierarchy;
        if (currentlyHoldingBat != isHoldingBat)
        {
            isHoldingBat = currentlyHoldingBat;
            UpdateIdleAnimation();
        }
    }

    private void HandleAttackInput()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking) // Left mouse button
        {
            isAttacking = true;
            playerAnim.SetBool("isAttacking", true);
        }
    }

    private void UpdateIdleAnimation()
    {
        if (isHoldingBat)
        {
            playerAnim.SetBool("holdingBat", true);
        }
        else
        {
            playerAnim.SetBool("holdingBat", false);
        }
    }

    // This function should be called by an Animation Event at the end of the attack animation
    public void ResetAttackState()
    {
        isAttacking = false;
        playerAnim.SetBool("isAttacking", false);
    }

    // Handle bat collision with enemy
    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking && other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(batDamage);
            }
        }
    }
}
