using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC_Brain : MonoBehaviour
{
    public event System.Action OnEnemyDeath;

    private bool _shouldDestroy = false;
    [SerializeField] private GameObject[] _spawnPiece;
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
                Debug.Log("Enemy should destroy and invoke OnEnemyDeath.");
                OnEnemyDeath?.Invoke();
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
                if (collision.gameObject.GetComponent<CharacterControl2D>() == null) return;
                if (collision.gameObject.GetComponent<CharacterControl2D>().Invincible == false)
                {
                //collision.gameObject.GetComponent<CharacterControl2D>().Health--;
                //Debug.Log($"A karakter élete: {collision.gameObject.GetComponent<CharacterControl2D>().Health}");
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(15f, 15f), ForceMode2D.Impulse);
                }
        }
    }
}
