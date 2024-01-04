using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    private float spreadAngle = 0f;
    private Rigidbody2D _rb;

    // Function to set the spread angle
    public void SetSpreadAngle(float angle)
    {
        spreadAngle = angle;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        // Apply shotgun spread on bullet instantiation
        ApplySpread();
    }

    void ApplySpread()
    {
        // Rotate the bullet based on the spread angle
        transform.Rotate(Vector3.forward, Random.Range(-spreadAngle / 2f, spreadAngle / 2f));
    }

    void Update()
    {
    }
}