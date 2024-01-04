using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGunMod : MonoBehaviour
{

    [SerializeField] private string _name;
    [SerializeField] private int _shotNumber;

    public Barrel Barrel;


    void Start()
    {
        Barrel barrel = new Barrel();
        barrel.BarrelName = _name;
        barrel.Shots = _shotNumber;
        Barrel = barrel;
    }
}
