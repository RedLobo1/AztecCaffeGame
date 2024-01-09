using System;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    private float homingForce = 100f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetHomingForce(float force)
    {
        homingForce = force;
    }

    void Update()
    {
        // Find the target (e.g., the player) and steer towards it
        GameObject target = FindTarget();

        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            Vector3 homingVelocity = direction * (homingForce * Time.deltaTime);

            // Update the bullet's position
            rb.AddForce(homingVelocity);
        }
        else
        {
            // No target found, you can handle this situation based on your game design
            Destroy(gameObject);
        }

        // You can add your bullet movement logic here
        // For example, transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    GameObject FindTarget()
    {
        // Implement your logic to find the target (e.g., the player)
        // For example: GameObject.FindGameObjectWithTag("Player");
        // Replace this with your actual logic based on your game design
        return GameObject.FindGameObjectWithTag("Mouse");

    }
}
