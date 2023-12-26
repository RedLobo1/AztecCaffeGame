using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC_Brain : MonoBehaviour
{
    private bool _shouldDestroy = false;

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
                Destroy(gameObject);
            }
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<CharacterControl2D>().Invincible == false)
            {
                collision.gameObject.GetComponent<CharacterControl2D>().Health--;
                Debug.Log($"A karakter élete: {collision.gameObject.GetComponent<CharacterControl2D>().Health}");
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(15f, 15f), ForceMode2D.Impulse);
            }
        }
    }
}
