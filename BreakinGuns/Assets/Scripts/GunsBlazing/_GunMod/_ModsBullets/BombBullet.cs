using System;
using UnityEngine;

public class BombBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D _collider2D;
    private CharacterControl2D _characterControl;

    private bool _explode;
    [SerializeField] private float _explosionStrength;
    [SerializeField] private float _radius;

    [SerializeField] private GameLogic _logic;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private Bullet _bulletLogic;


    private void Start()
    {
        _characterControl = GameObject.Find("MainCharacter").GetComponent<CharacterControl2D>();
        _logic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
        rb = GetComponent<Rigidbody2D>();
        rb.mass = 5f;
        rb.gravityScale = 2f;
        _collider2D = GetComponent<Collider2D>();
        _collider2D.sharedMaterial = new PhysicsMaterial2D();

    }

    private void Update()
    {
        if (_characterControl.TriggerBomb || _bulletLogic.CollidedWEnemy)
        {
            Explode();
            Instantiate(_explosion,transform.position,Quaternion.identity);
            _explosion.SetActive(true);
            Destroy(gameObject);
        }

    }

    private void OnDestroy()
    {
     Destroy(gameObject.GetComponent<LineRenderer>());
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
                if (rb.gameObject.tag == "Enemy")
                {
                    _characterControl.TriggerBomb = false;
                    if(rb.gameObject.GetComponent<NPC_Brain>() == null) return;
                    rb.gameObject.GetComponent<NPC_Brain>().ShouldDestroy = true;
                }

            }
        }
        
    }

}