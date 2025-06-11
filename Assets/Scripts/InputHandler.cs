using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler: MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    
    private InputAction moveAction, lookAction, jumpAction;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        //lookAction = InputSystem.actions.FindAction("Look");
        jumpAction = InputSystem.actions.FindAction("Jump");
        
        Cursor.visible = false;
    }

    private void Update()
    {
        var movementVector = moveAction.ReadValue<Vector2>();
        playerMovement.Move(movementVector);
        jumpAction.performed += ctx => playerMovement.Jump(ctx);
        //var lookVector = lookAction.ReadValue<Vector2>();
        //playerMovement.Rotate(lookVector);
        
    }
}
