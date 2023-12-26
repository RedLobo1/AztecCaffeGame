using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject _flash;
    [SerializeField] private GameObject _smoke;

    [SerializeField] public Vector3 Direction;
    //public TimeBrain Brain;

    public int maxBounces = 2; // Set the maximum number of bounces

    private int bounceCount = 0;

    public Bullet(Vector3 direction)
    {
        Direction = direction;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Instantiate(_flash, gameObject.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //gameObject.transform.position += transform.position * Time.deltaTime * _speed;

        _rb.AddForce(Direction * _speed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(_flash, gameObject.transform.position, Quaternion.identity);
        //Destroy(gameObject);

        if (collision.gameObject.tag == "Wall")
        {
            bounceCount++;
            // Check if the maximum number of bounces is reached
            if (bounceCount >= maxBounces)
            {
                StartCoroutine(StopTime());
                Destroy(gameObject);
            }
        }
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
    IEnumerator StopTime()
    {
        // Stop time
        Time.timeScale = 0;

        // Wait for the specified duration
        yield return new WaitForSecondsRealtime(0.05f);

        // Resume time
        Time.timeScale = 1;
    }
}
