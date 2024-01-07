using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBarrelScript : MonoBehaviour
{
    private bool _explode;
    [SerializeField] private float _explosionStrength;
    [SerializeField] private float _radius;

    [SerializeField] private GameLogic _logic;
    [SerializeField] private GameObject _explosion;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!_explode) return;

        Explode();
        _logic.ShotEvent = false;
        Instantiate(_explosion,transform.position,Quaternion.identity);
        Destroy(gameObject);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7  && collision.gameObject.tag == "Player")
        {
            _explode = true;
        }
    }
    private void Explode()
    {
        Collider2D[] colliders2D = Physics2D.OverlapCircleAll(transform.position, _radius);

        foreach (Collider2D collider in colliders2D)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 forceDirection = collider.transform.position - transform.position;
                forceDirection.Normalize();
                _logic.ShotEvent = true;
                rb.AddForce(forceDirection * _explosionStrength, ForceMode2D.Impulse);
                if(rb.gameObject.tag == "Enemy")
                {
                    Destroy(rb.gameObject);

                }

            }
        }
    }
}

