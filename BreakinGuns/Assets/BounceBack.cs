using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBack : MonoBehaviour
{
    [SerializeField] private bool _left, _right;
    [SerializeField] private float _bounceStrength;
    [SerializeField] private float _enemyBounceStrength;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            if (_left)
            {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-5, 5) * _bounceStrength , ForceMode2D.Impulse);
            }
            else if (_right)
            {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(5, 5) * _bounceStrength , ForceMode2D.Impulse);
                Debug.Log("AddForce");
            }

            if (other.gameObject.tag == "Enemy")
            {
                if (_left)
                {
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-5, 5) * _bounceStrength * _enemyBounceStrength, ForceMode2D.Impulse);
                }
                else if (_right)
                {
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(5, 5) * _bounceStrength * _enemyBounceStrength, ForceMode2D.Impulse);
                }

            }
        }

    }
}
