using System;
using UnityEngine;

public class PiercingBullet : MonoBehaviour
{
    private float piercingTime = 1.5f;

    // Function to set the piercing time
    public void SetPiercingTime(float time)
    {
        piercingTime = time;
        // You can add additional logic if needed based on the piercing time
    }

    void Update()
    {
        // Example: Decrease piercing time or check for collisions here
        if (piercingTime > 0f)
        {
            piercingTime -= Time.deltaTime;
            // Additional logic for piercing effect
        }
        else
        {
            // The piercing effect has ended, you may want to destroy the bullet or apply other effects
            Destroy(gameObject);
        }

        // You can add your bullet movement logic here
        // For example, transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
