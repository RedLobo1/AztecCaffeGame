using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject _flash;
    [SerializeField] private GameObject _regular, _bomb;

    [SerializeField] public Vector3 Direction;

    public bool Destructible = true;
    //public TimeBrain Brain;

    public int maxBounces = 2; // Set the maximum number of bounces

    private int bounceCount = 0;

    public Bullet(Vector3 direction = default(Vector3))
    {
        Direction = direction;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Instantiate(_flash, gameObject.transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if (_regular || _bomb == null) return;
        if (Destructible)
        {
            _regular.SetActive(true);
            _bomb.SetActive(false);
        }
        else
        {
            _regular.SetActive(false);
            _bomb.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector2 forwardVector =  transform.rotation * Vector2.right;
        _rb.velocity = (forwardVector * _speed);
        //_rb.MovePosition(forwardVector * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(_flash, gameObject.transform.position, Quaternion.identity);
        //Destroy(gameObject);

        if (collision.gameObject.tag == "Wall")
        {
            bounceCount++;
            // Check if the maximum number of bounces is reached
            if (bounceCount >= maxBounces && Destructible)
            {
                Destroy(gameObject);
            }
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
