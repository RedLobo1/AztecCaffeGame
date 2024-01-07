using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_FlipCharacter : MonoBehaviour
{
    [SerializeField] private NPC_MovementScript NpcMovementScript;
    Collider2D _collider2D;
    void Start()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NpcMovementScript.currentState == NPC_MovementScript.EnemyState.ClimbLadder)
        {
            _collider2D.enabled = false;
        }
        else
        {
            _collider2D.enabled = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            NpcMovementScript.IsFacingRight = !NpcMovementScript.IsFacingRight;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            if (NpcMovementScript.currentState == NPC_MovementScript.EnemyState.Idle)
            {
                NpcMovementScript.IsFacingRight = !NpcMovementScript.IsFacingRight;
            }
            
        }
    }
}
