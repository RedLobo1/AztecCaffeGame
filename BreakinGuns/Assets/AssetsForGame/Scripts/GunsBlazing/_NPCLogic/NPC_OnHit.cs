using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_OnHit : MonoBehaviour
{
    [SerializeField] private NPC_Brain NPC_Brain;   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if (collision.gameObject.tag == "Player")
            {
                NPC_Brain.ShouldDestroy = true;
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            if (other.gameObject.tag == "Player")
            {
                NPC_Brain.ShouldDestroy = true;
            }
        }
    }
}
