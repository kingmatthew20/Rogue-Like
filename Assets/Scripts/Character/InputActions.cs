using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActions : MonoBehaviour, Controls.IPlayerActions
{
    Controls controls;
    PlayerController player;

    void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
        player = GetComponent<PlayerController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        player.moveInputDirection = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        player.lookInputDirection = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase ==  InputActionPhase.Started)
            player.Jump();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            player.Dash();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            player.StartFire();
        if (context.phase == InputActionPhase.Canceled)
            player.EndFire();

    }
}
