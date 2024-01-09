using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class GunShotLogic : MonoBehaviour
{
    [SerializeField] public bool Shot;
    [SerializeField] public bool Ready;

    public event EventHandler ResetShootingLogic;
    [SerializeField] private float _explosionStrength;

    [SerializeField] private BuildMods _buildMods;
    [SerializeField] private SpriteRenderer _gunVisual;
    [SerializeField] private CharacterControl2D Control;
    [SerializeField] private Transform _target;

    public GameObject[] Parts;
    [SerializeField] private float _stopTime;

    //EXTRA LOGIC MOD LOGIC

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float shotgunSpreadAngle = 15f;
    public float chargeTime = 2f; // Time to charge for piercing shot
    public float homingForce = 5f;
    public float rapidFireRate = 0.1f;
    public float BulletCount = 1f;

    [SerializeField] private bool isRapidFiring = false;

    private void Start()
    {

    }

    void Update()
    {
        CheckMods();

        _gunVisual.enabled = Ready;
        if (Shot)
        {
            SpawnParts();
            Explode();
            Fire();

            _buildMods.BuildModShotGun = false;
            _buildMods.BuildPiercing = false;
            _buildMods.BuildBomb = false;

            Shot = false;
            ResetShootingLogic.Invoke(this, EventArgs.Empty);
            StartCoroutine(StopTime());

        }
    }

    private void CheckMods()
    {
        if (_buildMods.BuildModShotGun)
        {
            BulletCount = 4;
        }
    }

    void Fire()
    {
        Vector3 direction = _target.position - transform.position;
        direction.Normalize();
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        rotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z + 90f);

        // Normal shot
        for (int i = 0; i < BulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
            ApplyMods(bullet, chargeTime);
        }
    }

    void ApplyMods(GameObject bullet, float chargeTime)
    {
        // Apply shotgun spread
        if (_buildMods.BuildModShotGun)
        {
            bullet.GetComponent<ShotgunBullet>().enabled = true;
            float angle = isRapidFiring ? 0f : shotgunSpreadAngle;
            bullet.GetComponent<ShotgunBullet>().SetSpreadAngle(angle);
        }

        // Apply piercing effect based on charge time
        if (_buildMods.BuildPiercing)
        {
            bullet.GetComponent<PiercingBullet>().enabled = true;
            bullet.GetComponent<PiercingBullet>().SetPiercingTime(chargeTime);
            bullet.GetComponent<Collider2D>().isTrigger = true;
        }

        // Apply explosive bullet
        if (_buildMods.BuildBomb)
        {
            bullet.GetComponent<Bullet>().Destructible = false;
            bullet.GetComponent<BombBullet>().enabled = true;
        }

        // Add more mods here
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