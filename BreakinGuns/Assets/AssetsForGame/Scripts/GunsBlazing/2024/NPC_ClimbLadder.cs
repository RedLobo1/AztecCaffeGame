using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC_ClimbLadder : MonoBehaviour
{
    [SerializeField] private NPC_MovementScript NpcMovementScript;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LadderEntry"))
        {
            // Npc decides if wants to use the ladder or not
            int random = Random.Range(0, 2);
            if (random == 1)
            {
                gameObject.transform.position = other.gameObject.transform.position;
                NpcMovementScript.currentState = NPC_MovementScript.EnemyState.ClimbLadder;
            }

        }
    }
}
