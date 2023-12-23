using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]private float _speed;
    [SerializeField]private Rigidbody2D _rb;
    [SerializeField]private GameObject _flash;
    [SerializeField]private GameObject _smoke;

    [SerializeField]public Vector3 Direction;

    public Bullet(Vector3 direction)
    {
        Direction = direction;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Instantiate(_flash,gameObject.transform.position,Quaternion.identity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //gameObject.transform.position += transform.position * Time.deltaTime * _speed;

        _rb.AddForce(Direction * _speed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        Instantiate(_flash, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
