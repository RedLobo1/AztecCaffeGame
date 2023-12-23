using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleShakeLogic : MonoBehaviour
{
    public GameLogic GameLogic;

    public float speed = 10f; // How fast the object shakes
    public float amount = 0.2f; // How much the object shakes

    private Vector3 currentPosition;
    private bool shaking = false;

    void Awake()
    {
        currentPosition = transform.position;
        GameLogic.ImpactEvent += ShakeCamera;
    }

    void Update()
    {
        if (shaking)
        {
            float offset = Mathf.Sin(Time.deltaTime * speed) * amount;
            transform.position = new Vector3(currentPosition.x + offset, currentPosition.y,-10f);
        }
    }

    public void ShakeCamera(object sender, System.EventArgs e)
    {
        if (shaking)
        {
            return;
        }

        shaking = true;

        Camera.main.transform.position = new Vector3(currentPosition.x, currentPosition.y, Camera.main.transform.position.z);

        Invoke("StopShake", 0.1f); // Stop shaking after half a second
    }

    private void StopShake()
    {
        shaking = false;
        transform.position = currentPosition;
    }
}

