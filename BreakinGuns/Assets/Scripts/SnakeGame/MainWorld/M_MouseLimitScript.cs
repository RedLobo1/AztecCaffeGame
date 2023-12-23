using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_MouseLimitScript : MonoBehaviour
{

    public float radius = 100f; // Specify the radius
    [SerializeField] private Transform targetObject; // Reference to the target object

    void Update()
    {
        Vector2 targetObjectVector2 = new Vector2(targetObject.position.x,targetObject.position.y);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Get mouse position in world space

        Vector2 clampedMousePosition = Vector2.ClampMagnitude(mousePosition - targetObjectVector2, radius); // Limit mouse position to radius

        transform.position = targetObjectVector2 + clampedMousePosition; // Set mouse position

        Debug.DrawLine(clampedMousePosition, targetObjectVector2);
        Debug.Log(targetObjectVector2);
    }
}


