using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_ladderTopLogic : MonoBehaviour
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
        if (other.gameObject.CompareTag("LadderTop"))
        {
            NpcMovementScript.currentState = NPC_MovementScript.EnemyState.Idle;
        }
    }
}
