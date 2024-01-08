using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    [SerializeField] private Bullet _bulletLogic;
    [SerializeField] private Rigidbody2D _rbRigidbody2D;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" && _bulletLogic.Destructible == true)
        {
            _rbRigidbody2D.velocity = Vector2.zero;
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Enemy" && _bulletLogic.Destructible == false)
        {
            _rbRigidbody2D.velocity = Vector2.zero;
        }

    }
}
