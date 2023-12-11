using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Player : MonoBehaviour
{
    private Vector2 positionInput;
    private Vector2 minimumBounds;
    private Vector2 maximumBounds;

    private float moveSpeed = 5f;

    private float paddingLeft = 0.5f;
    private float paddingRight = 0.5f;
    private float paddingTop = 8f;
    private float paddingBottom = 2f;

    Shooter shooter;

    private void Awake() {
        shooter = GetComponent<Shooter>();
    }

    private void Start() {
        InitializeBounds();
    }
    private void Update() {
        Move();
    }
    private void InitializeBounds() {
        Camera mainCamera = Camera.main;
        minimumBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maximumBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
    }

    private void Move() {

        Vector3 deltaPosition = positionInput * moveSpeed * Time.deltaTime;
        Vector3 updatedPosition = new Vector3 {
            x = Mathf.Clamp(transform.position.x + deltaPosition.x, minimumBounds.x + paddingLeft, maximumBounds.x - paddingRight),
            y = Mathf.Clamp(transform.position.y + deltaPosition.y, minimumBounds.y + paddingBottom, maximumBounds.y - paddingTop),
            z = 0
        };
        transform.position = updatedPosition;
    }

    private void OnMove(InputValue value) {
        positionInput = value.Get<Vector2>();
    }

    private void OnFire(InputValue value) {
        if(shooter != null) {
            shooter.SetIsFiring(value.isPressed);
        }
    }
}
