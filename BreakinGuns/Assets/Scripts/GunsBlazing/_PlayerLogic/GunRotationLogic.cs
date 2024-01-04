using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunRotationLogic : MonoBehaviour
{
    GameObject targetGameObject;
    float targetRotation;
    bool flipY;

    void Awake()
    {
        targetGameObject = GameObject.Find("MainCharacter");

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = targetGameObject.transform.position;
        targetRotation = targetGameObject.transform.eulerAngles.y;

        CalculateFlipState(targetPosition);
        RotateVisual();
        UpdateRotation(targetPosition);
    }

    private void UpdateRotation(Vector3 targetPosition)
    {
        targetRotation = targetGameObject.transform.eulerAngles.y;
        

        flipY = transform.position.x - targetPosition.x > 0;
    }

    private void RotateVisual()
    {
        transform.rotation = Quaternion.Euler(0f, targetRotation, transform.rotation.eulerAngles.z-90);
        if (flipY)
        {
            
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, targetRotation, transform.rotation.eulerAngles.z - 180);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void CalculateFlipState(Vector3 targetPosition)
    {
        float difference = transform.position.x - targetPosition.x;
        flipY = difference > 0;
    }
}
