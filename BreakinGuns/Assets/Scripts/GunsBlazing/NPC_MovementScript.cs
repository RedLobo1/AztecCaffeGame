using System;
using UnityEngine;
using UnityEngine.Serialization;

public class NPC_MovementScript : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private FieldOfView _fieldOfView;
    [SerializeField] private Vector3 WHAT;

    [SerializeField] private GameObject Test;

    private bool _flip;
    private bool _flipped;

    private Rigidbody2D _rb;

    public bool StopMovement = true;

    public Vector3 LastPlayerLocation;


    public bool Flip
    {
        get => _flip;
        set
        {
            if (_flip) _flipped = !_flipped;
            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, 1f);
            _flip = value;
        }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        _fieldOfView.SetOrigin(transform.position);
        //Test.transform.position = WHAT;
    }

    private void Update()
    {
        
        if (StopMovement) return;
        //make it so that npc goes in direction of player using addforce
        //if player is in field of view

        var directionOfLastL = LastPlayerLocation - transform.position;
        directionOfLastL = new Vector3(directionOfLastL.x, 0, 0);
        directionOfLastL.Normalize();

        _rb.AddForce(directionOfLastL * _speed);

        if (directionOfLastL.x > 0)
        {
            if (LastPlayerLocation.x <= transform.position.x)
                StopMovement = true;
        }

    }

    private void RegularRightLeftMovement()
    {
        
        var direction = MoveNPC();

        _rb.AddForce(direction * _speed);
    }


    private Vector2 MoveNPC()
    {
        if (Flip)
            return Vector2.right;
        return Vector2.left;
    }
}