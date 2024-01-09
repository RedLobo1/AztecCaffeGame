using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController2D _characterController2D;
    [SerializeField] private float _runSpeed; 
    [SerializeField] private float _jumpSpeed; 

    private float _horizontalMove;
    private bool _jump;
    void Awake()
    {
        _characterController2D = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;

        if (Input.GetButton("Jump"))
        {
            _jump=true;
            _horizontalMove += _jumpSpeed;
        }
    }
    private void FixedUpdate()
    {
        _characterController2D.Move(_horizontalMove * Time.fixedDeltaTime, false, _jump);
        _jump = false;
    }
}
