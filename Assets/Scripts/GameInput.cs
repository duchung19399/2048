using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
    public static GameInput Instance { get; private set; }

    public event EventHandler<OnMoveEventArgs> OnMoveAction;
    public class OnMoveEventArgs : EventArgs {
        public PlayerAction action;
    }

    public enum PlayerAction {
        Up,
        Down,
        Left,
        Right
    }
    private PlayerInputAction playerInputAction;

    private void Awake() {
        Instance = this;

        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
        playerInputAction.Player.Move.started += Move_Started;
    }

    private void OnDestroy() {
        playerInputAction.Player.Move.started -= Move_Started;
        playerInputAction.Player.Disable();
    }

    private void Move_Started(InputAction.CallbackContext context) {
        Vector2 direction = context.ReadValue<Vector2>();
        if(direction.x == 0) {
            if(direction.y > 0) {
                OnMoveAction?.Invoke(this, new OnMoveEventArgs { action = PlayerAction.Up });
            } else {
                OnMoveAction?.Invoke(this, new OnMoveEventArgs { action = PlayerAction.Down });
            }
        } else {
            if(direction.x > 0) {
                OnMoveAction?.Invoke(this, new OnMoveEventArgs { action = PlayerAction.Right });
            } else {
                OnMoveAction?.Invoke(this, new OnMoveEventArgs { action = PlayerAction.Left });
            }
        }
    }
}
