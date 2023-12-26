using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunShotLogic : MonoBehaviour
{
    [SerializeField] public bool Shot;
    [SerializeField] public bool Ready;
    //[SerializeField] private bool _gunCollected;
    public event EventHandler ResetShootingLogic;
    [SerializeField] private float _explosionStrength;

    [SerializeField] private SpriteRenderer _gunVisual;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private CharacterControl2D Control;

    [SerializeField] private Transform _target;

    public GameObject[] Parts;
    [SerializeField]private float _stopTime;

    void Awake()
    {
        
    }

    void Update()
    {
        _gunVisual.enabled = Ready;
        if (Shot)
        {

            SpawnParts();
            Explode();
            SpawnBullet();
            Shot = false;
            ResetShootingLogic.Invoke(this, EventArgs.Empty);
            StartCoroutine(StopTime());

        }
    }

    private void SpawnBullet()
    {
        Vector3 direction = _target.position - gameObject.transform.position;
        direction.Normalize();

        GameObject bullet = Instantiate(_bullet, gameObject.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Direction = direction;
    }

    private void SpawnParts()
    {
        foreach (var part in Parts)
        {
            while (true)
            {
                // Generate a random rotation and position
                float randomRotation = Random.Range(0, 359);
                Vector2 random = Random.insideUnitCircle;
                Vector3 randomLocation = new Vector3(random.x, random.y, 0f);

                // Check for collisions with existing colliders
                
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + randomLocation, 1f, part.gameObject.layer);

                // If there are no collisions, instantiate the part
                if (colliders.Length == 0)
                {
                GameObject generatedPart = Instantiate(part, transform.position + randomLocation, Quaternion.Euler(0, 0, randomRotation));
                break; // Exit the while loop
                }
            }
        }

        //foreach(var part in Parts)
        //{
        //    float randomRotation = Random.Range(0, 359);
        //    Vector2 random = Random.insideUnitCircle;
        //    Vector3 randomLocation = new Vector3(random.x, random.y, 0f);

        //    GameObject generatedPart = Instantiate(part, transform.position + randomLocation,Quaternion.Euler(0,0, randomRotation));
        //}
    }

    private void Explode()
    {
       Collider2D[] colliders2D = Physics2D.OverlapCircleAll(transform.position, 2f);


        foreach (Collider2D collider in colliders2D)
        {
            if(collider.tag == "Player")
            {
                return;
            }
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                Vector2 forceDirection = collider.transform.position - transform.position;
                forceDirection.Normalize();

                rb.AddForce(forceDirection * _explosionStrength, ForceMode2D.Impulse);
                
            }
        }
    }
    IEnumerator StopTime()
    {
        Control.StopTime = true;
        // Stop time
        Time.timeScale = 0;

        // Wait for the specified duration
        yield return new WaitForSecondsRealtime(_stopTime);

        // Resume time
        Control.StopTime = false;
        Time.timeScale = 1;
    }
}