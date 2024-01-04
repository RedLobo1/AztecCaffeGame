using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeGameLogic : MonoBehaviour
{
    Gun gun = new Gun();
    Barrel _doubleDoubleBarrel = new Barrel();

    void Start()
    {
        _doubleDoubleBarrel.Shots = 2;
        _doubleDoubleBarrel.modName  = "Double Barrel";
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetButton("Jump"))
        {
            gun.AddMod(_doubleDoubleBarrel);
        }

        if (Input.GetButton("Fire1"))
        {
            gun.DebugAllMods();
        }*/
    }
}
