using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SetGunLocation : MonoBehaviour
{
    [SerializeField] private Transform _gunLocation;
    [SerializeField] private Transform _characterTransform;

    private bool _inGround;
    [SerializeField]private float _offSet;
    private Vector3 _originalPosition;

    void Start()
    {
        _originalPosition = _gunLocation.position;
    }

    private void Update()
    {
        if (_inGround)
        {
            gameObject.transform.position = _characterTransform.position;
        }
        else
        {
            float lerpFactor = 0.5f; // You can adjust this value based on how fast you want the gun to move towards its original position.

            // Lerping between the current position and the target position
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _characterTransform.position + new Vector3(_originalPosition.x + _offSet, _originalPosition.y), lerpFactor * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall" && other.gameObject.layer == 3)
        {
            _inGround = true;
        }
            
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall" && other.gameObject.layer == 3)
        {

            _inGround = false;
        }
    }
}
