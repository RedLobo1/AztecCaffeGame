using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl2D : MonoBehaviour
{
    public GunShotLogic GunLogic;

    public Transform MousePosition;

    public bool CanShoot;

    private Rigidbody2D _rb;
    private Vector3 _mousePosition;
    private Vector2 _dashDir;

    [SerializeField] private float _dashDistance;
    [SerializeField] private float _timeSlowScale;
    [SerializeField] private float _dashMultiplier;

    private bool _dashing;
    private bool _shot;
    private bool _aiming;

    void Awake()
    {
        _rb =GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {

        SetMousePosition();

        //Movement
        CharacterDash();
        CharacterShoot();

        if (_dashing || _aiming) return;
        else
        {
            Time.timeScale = 1;
        }
    }


    private void CharacterShoot()
    {
        if(!CanShoot) return;
        GunLogic.Ready = true;
        if(Input.GetMouseButtonDown(1) && !_aiming)
        {
            Time.timeScale = _timeSlowScale;
            Time.fixedDeltaTime = Time.timeScale * .02f;
            _aiming = true;
        }
        if (Input.GetMouseButtonDown(0) && _aiming)
        {
            _shot = true;
            CalculateDirection();
            _rb.velocity = Vector2.zero;
            _rb.AddForce(_dashDir * -10, ForceMode2D.Impulse);
            _aiming = false;
            GunLogic.Shot = _shot;
        }
        else
        {
            _shot = false;
        }
    }

    private void CharacterDash()
    {
        if (Input.GetMouseButtonDown(0) && !_aiming)
        {
            Debug.Log($"{MousePosition}");
            Debug.DrawLine(gameObject.transform.position, MousePosition.position);
            _dashing = true;

            Time.timeScale = _timeSlowScale;
            Time.fixedDeltaTime = Time.timeScale * .02f;

        }

        if (Input.GetMouseButtonUp(0) && _dashing)
        {
            CalculateDirection();

            _rb.velocity = Vector2.zero;
            _rb.AddForce(_dashDir, ForceMode2D.Impulse);
            _dashing = false;
        }
    }

    private void CalculateDirection()
    {
        Vector2 moveDirection = MousePosition.position - gameObject.transform.position;
        _dashDistance = Mathf.Sqrt(moveDirection.sqrMagnitude) * _dashMultiplier;
        _dashDir = moveDirection.normalized * _dashDistance;
    }

    private void SetMousePosition()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePosition.z = -10;
    }

}
