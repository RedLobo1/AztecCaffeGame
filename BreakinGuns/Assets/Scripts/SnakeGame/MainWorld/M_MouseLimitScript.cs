using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_MouseLimitScript : MonoBehaviour
{

    public float radius = 100f; // Specify the radius
    [SerializeField] private Transform targetObject; // Reference to the target object
    [SerializeField] private CharacterControl2D _control;

    void Update()
    {

        if (_control.Aiming) 
        {
            UnlimitedMouse();
        }
        else
        {
            LimitMouse();
            RotateMouse();
        }
    }

    private void UnlimitedMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        transform.rotation = Quaternion.identity;
    }

    private void RotateMouse()
    {

        // Get the direction from the arrow to the player
        Vector2 directionToPlayer = targetObject.position - transform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        // Rotate the arrow to face the player
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void LimitMouse()
    {
        Vector2 targetObjectVector2 = new Vector2(targetObject.position.x, targetObject.position.y);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Get mouse position in world space

        Vector2 clampedMousePosition = Vector2.ClampMagnitude(mousePosition - targetObjectVector2, radius); // Limit mouse position to radius

        transform.position = targetObjectVector2 + clampedMousePosition; // Set mouse position

    }
}


