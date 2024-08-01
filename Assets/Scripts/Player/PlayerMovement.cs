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
    [SerializeField] private float lookLimit = 90.0f;
    [SerializeField] private Camera playerCamera;

    [Header("Animator")]
    [SerializeField] private Animator playerAnim;

    [Header("Weapon")]
    [SerializeField] private Transform handTransform; // Player's hand transform

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
    private float gravity = -9.81f;

    [SerializeField] private float jumpForce = 5f;
    private float verticalVelocity = 0;

    private Inventory playerInventory;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        gameMenu = gameMenuUI.GetComponent<GameMenu>();
        playerInventory = GetComponent<Inventory>();
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
            MoveCharacter();
            HandleCameraRotation();
            HandlePickup();
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

        if (characterController.isGrounded)
        {
            verticalVelocity = 0; 

            if (Input.GetKey(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
                playerAnim.SetTrigger("jump");
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime; 
        }
    }

    private void MoveCharacter()
    {
        moveDirection.y = verticalVelocity;
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

    private void HandlePickup()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2f)) // Adjust the distance as needed
            {
                Weapon weapon = hit.collider.GetComponent<Weapon>();
                if (weapon != null && !weapon.isPickedUp)
                {
                    playerInventory.AddWeapon(weapon);
                    weapon.PickUp();
                    EquipWeapon(weapon);
                    Debug.Log("Picked up weapon: " + weapon.name);
                }
            }
        }
    }

    private void EquipWeapon(Weapon weapon)
    {
        // Implement the logic to equip the weapon
        // For simplicity, let's just activate the weapon as a child of the player's hand
        weapon.transform.SetParent(handTransform);
        weapon.transform.localPosition = new Vector3(-0.027f, 0.219f, 0.008f);
        weapon.transform.localRotation = Quaternion.Euler(-10.222f, 349.32f, 525.088f);
        weapon.gameObject.SetActive(true);
    }
}
