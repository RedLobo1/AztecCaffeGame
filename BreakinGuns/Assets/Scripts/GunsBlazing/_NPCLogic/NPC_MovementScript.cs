using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class NPC_MovementScript : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        FollowPlayer,
        ClimbLadder,
        Stunned,
    }

    public EnemyState currentState = EnemyState.Idle;

    [Header("Properties")]
    [SerializeField] private float _speed;
    [SerializeField] private float _flollowSpeed;
    [SerializeField] private float _climbSpeed;
    [SerializeField] private float _speedMultiplier;

    [Header("References")]

    public Transform Player;
    public Transform LadderTop;

    [SerializeField] private FieldOfView _fieldOfView;
    [SerializeField] private FieldOfViewLite _fieldOfViewlite;
    [SerializeField] private LayerMask _specialStateLayer;
    [SerializeField] private LayerMask _originalSetup;
    [SerializeField] private GameObject _deathExplosion;
    private Rigidbody2D _rb;
    private Collider2D _collider2D;

    [Header("Bools")]

    public bool IsFacingRight = true;
    public bool StopMovement = true;
    public bool PlayerSeen;
    public Vector3 LastPlayerLocation;
    private bool _coroutine = true;
    public bool Voulnerable = false;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _originalSetup = _collider2D.forceReceiveLayers;
    }

    private void LateUpdate()
    {
        _fieldOfView.SetOrigin(transform.position);
        _fieldOfViewlite.SetOrigin(transform.position);
    }
    
    private void Update()
    {
        _collider2D.forceReceiveLayers = _specialStateLayer;
        //if (Voulnerable == true)renderer
        switch (currentState)
        {
            case EnemyState.Idle:
                IdleState();
                break;
            case EnemyState.FollowPlayer:
                FollowPlayer();
                break;
            case EnemyState.ClimbLadder:
                ClimbLadderState();
                break;
            case EnemyState.Stunned:
                Stunned();
                break;
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void Stunned()
    {
        Voulnerable = true;
    }

    private void IdleState()
    {
        Voulnerable = false;
        _collider2D.forceReceiveLayers = _originalSetup;
        // Move left and right in idle state
        Vector2 movement = new Vector2(IsFacingRight ? 1 : -1, _rb.velocity.y);
        _rb.velocity = new Vector2( movement.x * _speed, movement.y);

        if (Player != null && Vector2.Distance(transform.position, Player.position) < 5f || PlayerSeen)
        {
            currentState = EnemyState.FollowPlayer;
        }
    }
    private void FollowPlayer()
    {
        _collider2D.forceReceiveLayers = _originalSetup;
        var directionOfLastL = LastPlayerLocation - transform.position;
        directionOfLastL = new Vector3(directionOfLastL.x, 0, 0);
        directionOfLastL.Normalize();

        _rb.AddForce(directionOfLastL * (_flollowSpeed * _speedMultiplier));

        if (directionOfLastL.x > 0)
        {
            if (LastPlayerLocation.x <= transform.position.x){}
                //StopMovement = true;
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerSeen == false)
        {
            if (_coroutine == true)
            {
                StartCoroutine(SwitchToIdle());
            }
        }

    }
    
    private IEnumerator SwitchToIdle()
    {
        _coroutine = false;
        yield return new WaitForSeconds(2);
        currentState = EnemyState.Idle;
        _coroutine = true;

    }

    private void ClimbLadderState()
    {
        Vector2 climbMovement = new Vector2(0, 1);
        _rb.velocity = climbMovement * _climbSpeed;
        _collider2D.forceReceiveLayers = _specialStateLayer;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.layer == 7)
        {
            StartCoroutine(StunnedTimer());
        }

        if (other.gameObject.tag == "Saw" && Voulnerable)
        {
            Instantiate(_deathExplosion, transform.position, Quaternion.identity);
            NPC_Brain brain = GetComponent<NPC_Brain>();
            brain.ShouldDestroy = true;
        }

        
    }
    private IEnumerator StunnedTimer()
    {
        currentState = EnemyState.Stunned;
        yield return new WaitForSeconds(2);
        currentState = EnemyState.Idle;
    }
    
}