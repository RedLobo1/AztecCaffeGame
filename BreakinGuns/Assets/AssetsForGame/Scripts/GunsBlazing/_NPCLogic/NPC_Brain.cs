using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class NPC_Brain : MonoBehaviour
{
    public event System.Action OnEnemyDeath;

    private bool _shouldDestroy = false;
    [SerializeField] private GameObject[] _spawnPiece;
    [SerializeField] private GameObject _BaseKill;
    [SerializeField] private GameObject _deathExplosion;
    CinemachineTargetGroup _cTargetGroup;
    // Boolean property with getter and setter
    public bool ShouldDestroy
    {
        get { return _shouldDestroy; }
        set
        {
            _shouldDestroy = value;
            if (_shouldDestroy)
            {
                // Destroy the GameObject when the property is set to true
                int random = Random.Range(0, 3);
                Instantiate(_spawnPiece[random], transform.position, Quaternion.identity);
                Instantiate(_deathExplosion, transform.position, Quaternion.identity);
                Destroy(_BaseKill);
            }
        }
    }

    private void Awake()
    {
        GameObject CameraTargetGroup = GameObject.Find("TargetGroup");
        _cTargetGroup = CameraTargetGroup.GetComponent<CinemachineTargetGroup>();
        _cTargetGroup.AddMember(transform, 2, 0);
    }

    private void OnDestroy()
    {
        Debug.Log("Enemy should destroy and invoke OnEnemyDeath.");
        OnEnemyDeath?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
                if (collision.gameObject.GetComponent<CharacterControl2D>() == null) return;
                if (collision.gameObject.GetComponent<CharacterControl2D>().Invincible == false)
                {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(15f, 15f), ForceMode2D.Impulse);
                }
        }
    }
}
