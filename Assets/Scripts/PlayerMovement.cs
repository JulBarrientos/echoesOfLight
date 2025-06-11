using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int IsFalling = Animator.StringToHash("isFalling");
    
    private Animator characterAnimator;
    private CharacterController characterController;
    
    [SerializeField] private float playerSpeed = 10f, rotationSpeed = 5f, jumpForce = 10f;

    
    private Vector3 direction;
    
    private bool isFalling;
    private bool isGrounded = true;
    private float rotationY;
    
    private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float velocity;

    private void Awake()
    {
        characterAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
         
    }

    private void Update()
    {
        if (isGrounded) return;
        if (!isFalling)
        {
            isFalling = true;
            characterAnimator.SetBool(IsJumping, false);
            characterAnimator.SetBool(IsFalling, true);
        }
        else
        {
            isFalling = false;
            characterAnimator.SetBool(IsFalling, false);
            characterAnimator.SetBool(IsGrounded, true);
            isGrounded = true;
        }
    }

    public void Move(Vector2 movementVector)
    {
        var isMovementPressed = movementVector.y != 0 || movementVector.x != 0;
        characterAnimator.SetBool(IsWalking, false);
        
        if (!isMovementPressed) return;
        characterAnimator.SetBool(IsWalking, true);
        Vector3 positionToLook;
        positionToLook.x = movementVector.x;
        positionToLook.y = 0f;
        positionToLook.z = movementVector.y;
        var targetRotation = Quaternion.LookRotation(positionToLook);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        var move = transform.forward * movementVector.y + transform.right * movementVector.x;
        move *= (playerSpeed * Time.deltaTime);
        characterController.Move(positionToLook * (playerSpeed * Time.deltaTime));
        // transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    public void Jump(InputAction.CallbackContext jumpContext)
    {
        //ApplyGravity();
        //if (!jumpContext.started) return;
        //if (!characterController.isGrounded) return;
        //velocity += jumpForce;
        isGrounded = false;
        characterAnimator.SetBool(IsJumping, true);
        characterAnimator.SetBool(IsGrounded, false);
        
    }
    
    public void Rotate(Vector2 rotationVector)
    {
        rotationY += rotationVector.x * rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, rotationY, 0);
    }
    
}
