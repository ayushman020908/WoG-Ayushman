using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public float range = 20f;
    public LayerMask toBeHit;

    private Vector2 mousePos;
    private Vector2 aimDirection;

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        aimDirection = (mousePos - (Vector2)firePoint.position).normalized;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RunRaycast();
        }
    }

    void RunRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            firePoint.position,
            aimDirection,
            range,
            toBeHit
        );

        if (hit.collider != null)
        {
            Debug.Log("Name of collision: " + hit.collider.name);
        }
        else
        {
            Debug.Log("NO Collision");
        }
    }
}
