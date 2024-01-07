using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Amor : MonoBehaviour
{
    [SerializeField] private GameObject[] _weakSpots;
    [SerializeField] private int _armor = 3;

    private void Update()
    {
        if (_armor == 3)
        {
            _weakSpots[0].SetActive(true);
        }
        if (_armor == 2)
        {
            _weakSpots[1].SetActive(true);
        }
        if (_armor == 1)
        {
            _weakSpots[2].SetActive(true);
        }
        if (_armor == 0)
        {
            _weakSpots[3].SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.layer == 7)
        {
            _armor--;

        }
    }
}
