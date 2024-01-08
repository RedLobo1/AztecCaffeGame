using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC_ShootingLogic : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField, Range(0.1f, 10f)] private float accuracy = 1f;
    [SerializeField, Range(0.1f, 5f)] private float reloadingSpeed = 1f;
    [SerializeField, Range(1, 10)] private int shotCount = 3;

    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject target;

    private bool CR_running;

    public bool CanShoot;

    [SerializeField] private float _timer;
    [SerializeField] private float _maxTime;


    private void Start()
    {
        _timer = _maxTime;
        // Set the target using GameObject.Find
        target = GameObject.Find("MainCharacter");

        // Ensure that the target is found
        if (target == null)
        {
            Debug.LogError("Target not found. Make sure there is a GameObject named 'MainCharacter' in the scene.");
        }

        firePoint = transform;
        StartCoroutine(ShootRoutine());


    }

    private void Update()
    {
        if (CanShoot)
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
            else
            {
                CanShoot = false;
                _timer = _maxTime;
            }
        }
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            
                yield return new WaitForSeconds(reloadingSpeed);

                for (int i = 0; i < shotCount; i++)
                {
                    if (CanShoot)
                    {
                    Shoot();
                    }
                    yield return new WaitForSeconds(0.1f); // Interval between shots
                }
            

        }
    }

    private void Shoot()
    {
        Vector2 shootDirection = CalculateShootDirection();
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, shootDirection);
        rotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z + 90f);
        Instantiate(bulletPrefab, firePoint.position, rotation);
        ShakeLogic.Instance.ShakeCameraUpdated(2f, 0.1f);


    }

    private Vector2 CalculateShootDirection()
    {
        float randomOffset = Random.Range(-1f, 1f) * (1f - accuracy);
        //Vector2 shootDirection = new Vector3(target.transform.position.x,target.transform.position.y,0) - transform.position + new Vector3(randomOffset, randomOffset,0);
        Vector3 shootDirection = target.transform.position - transform.position + new Vector3(randomOffset, randomOffset,0);
        shootDirection.Normalize();
        return shootDirection;
    }
}

