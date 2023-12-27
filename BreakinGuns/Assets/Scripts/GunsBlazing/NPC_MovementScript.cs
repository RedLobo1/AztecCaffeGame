using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_MovementScript : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private FieldOfView _fieldOfView;
    private bool _flip;
    public bool Flip
    {
        get { return _flip; }
        set
        {
            if(_flip == true)
            {
                _flipped = !_flipped;
            }
            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, 1f);
            _flip = value;
        }
    }


    private Rigidbody2D _rb;
    private bool _flipped;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _fieldOfView.SetOrigin(transform.position);
        Vector2 direction = MoveNPC();

        _rb.AddForce(direction * _speed);
    }



    private Vector2 MoveNPC()
    {

        if (Flip)
        {
            return Vector2.right;
        }
        else
        {
            return Vector2.left;

        }
    }
}
