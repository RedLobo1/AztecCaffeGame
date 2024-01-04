using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_FlipCharacter : MonoBehaviour
{
    [SerializeField] private NPC_MovementScript npcMovementScript;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall" && npcMovementScript.Flip == false)
        {
            Debug.Log("Flip");
            npcMovementScript.Flip = true;
        }
        else if(collision.gameObject.tag == "Wall" && npcMovementScript.Flip == true)
        {
            Debug.Log("Flip Back");
            npcMovementScript.Flip = false;
        }
    }
}
