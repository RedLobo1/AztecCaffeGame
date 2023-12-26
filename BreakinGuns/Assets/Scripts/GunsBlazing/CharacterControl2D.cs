using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl2D : MonoBehaviour
{
    public GunShotLogic GunLogic;
    public GameLogic GameLogic;
    public Transform MousePosition;
    public SpriteRenderer SRenderer;

    public bool CanShoot;

    private Rigidbody2D _rb;
    private Vector3 _mousePosition;
    private Vector2 _dashDir;

    [SerializeField] private float _dashDistance;
    [SerializeField] private float _timeSlowScale;
    [SerializeField] private float _dashMultiplier;
    [SerializeField] private float _invincibleTime;

    private bool _dashing;
    private bool _shot;
    public bool Aiming;
    public bool StopTime;

    private bool _dead;
    public bool Invincible;
    private int _health = 2;
    public int Health
    {
        get
        {
            return _health;
        }

        set
        {
            if (_health <= 0)
            {
                Invincible = true;
                Dead = true;
                return;
            }

            Invincible = true;
            StartCoroutine(MakeCharacterWhiteAndInvincible());

            _health = value;
        }
    }

    private IEnumerator MakeCharacterWhiteAndInvincible()
    {

        SRenderer.color = Color.red;

        yield return new WaitForSeconds(_invincibleTime);

        SRenderer.color = Color.white;
        Invincible = false;
    }


public bool Dead 
    {   get
        {
            return _dead;
        }
        set 
        {
            if (_dead == true)
            {
                _dead = !_dead;
            }
            SRenderer.color = Color.black;
            _dead = value;
        } 
    }

    void Awake()
    {
        _rb =GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Dead == true) return;
        SetMousePosition();

        //Movement
        CharacterDash();
        GunLogic.Ready = false;
        CharacterShoot();

        if (_dashing || Aiming || StopTime) return;
        else
        {
            Time.timeScale = 1;
        }
    }


    private void CharacterShoot()
    {
        if (Input.GetMouseButtonDown(1) && Aiming)
        {
            Aiming = false;
            return;
        }

        if (!CanShoot) return;
        GunLogic.Ready = true;

        if (Input.GetMouseButtonDown(1) && !Aiming)
        {
            Time.timeScale = _timeSlowScale;
            Time.fixedDeltaTime = Time.timeScale * .02f;
            Aiming = true;
        }
        if (Input.GetMouseButtonDown(0) && Aiming)
        {
            CalculateDirection();
            _rb.velocity = Vector2.zero;
            _rb.AddForce(_dashDir * -2, ForceMode2D.Impulse);
            Aiming = false;
            GunLogic.Shot = true;
            GameLogic.ShotEvent = true;
            
        }
    }

    private void CharacterDash()
    {
        if (Input.GetMouseButtonDown(0) && !Aiming)
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
