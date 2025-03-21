using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private InputActions inputActions;

    public event EventHandler OnMouseClickAction;

    private void Awake()
    {
        Instance = this;

        inputActions = new InputActions();
        inputActions.Enable();

        inputActions.Player.MouseClick.performed += MouseClick_performed;
    }

    private void MouseClick_performed(InputAction.CallbackContext obj)
    {
        OnMouseClickAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMousePosition()
    {
        return inputActions.Player.MousePosition.ReadValue<Vector2>();
    }

    private void OnDestroy()
    {
        inputActions.Player.MouseClick.performed -= MouseClick_performed;
        inputActions.Dispose();
    }
}