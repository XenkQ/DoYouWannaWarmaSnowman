using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour {
    private PlayerInputActions _playerInputActions;

    private void Awake() {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }

    void Update() {
       transform.position = GetMouseWorldPosition();
    }
    public Vector3 GetMouseWorldPosition() {
        Ray mouseRay = Camera.main.ScreenPointToRay(_playerInputActions.Player.Mouse.ReadValue<Vector2>());
        
        Physics.Raycast(mouseRay, out RaycastHit mouseRaycastHit);
        
        return mouseRaycastHit.point;
    }
}
