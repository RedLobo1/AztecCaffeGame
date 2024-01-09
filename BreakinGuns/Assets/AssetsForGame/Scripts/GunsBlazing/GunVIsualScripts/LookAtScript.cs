using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScript : MonoBehaviour
{
    [SerializeField] private bool _mouse;

    public Transform Target;

    void Awake()
    {
        if (!_mouse)
        {
            Target = GameObject.Find("MainCharacter").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Target.position - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.up,direction);
    }
}
