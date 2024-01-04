using System;
using UnityEngine;
using UnityEngine.Serialization;

public class NPC_MovementScript : MonoBehaviour
{
    public enum NPCMovementType
    {
        RegularRightLeftMovement,
        FollowPlayer
    }

    [Header("Properties")]
    [SerializeField] private float _speed;
    [SerializeField] private float _speedMultiplier;

    [Header("References")]
    [SerializeField] private FieldOfView _fieldOfView;
    [SerializeField] private FieldOfViewLite _fieldOfViewlite;
    [SerializeField] private Vector3 WHAT;

    [Header("Test")]
    [SerializeField] private GameObject Test;

    private bool _flip;
    private bool _flipped;
    private Rigidbody2D _rb;

    [Header("Bools")]
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
        _fieldOfViewlite.SetOrigin(transform.position);
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

        _rb.AddForce(directionOfLastL * (_speed * _speedMultiplier));

        if (directionOfLastL.x > 0)
        {
            if (LastPlayerLocation.x <= transform.position.x)
                StopMovement = true;
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void RegularRightLeftMovement()
    {
        
        var direction = MoveNPC();

        _rb.AddForce(direction * (_speed * _speedMultiplier));
    }


    private Vector2 MoveNPC()
    {
        if (Flip)
            return Vector2.right;
        return Vector2.left;
    }
}