using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.rigidbody.velocity = Vector2.zero;
            other.rigidbody.AddForce(_rb.velocity * 10, ForceMode2D.Impulse);
        }

    }
}
