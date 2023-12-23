using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingLimit : MonoBehaviour
{
    CharacterControl2D characterControl;
    public GunShotLogic ShotLogic;
    public bool CanCollect;
    

    public Dictionary<string, bool> ColorsDictionary = new Dictionary<string, bool>(); //add to this in another level setup script

    private void Awake()
    {
        //temp
        ColorsDictionary.Add("Red", false);
        ColorsDictionary.Add("Green", false);
        ColorsDictionary.Add("Blue", false);

        characterControl = gameObject.GetComponent<CharacterControl2D>();
    }
    private void Update()
    {
        bool allTrue = true;

        if (ShotLogic.Shot)
        {
            //temp
            ColorsDictionary["Blue"] = false;
            ColorsDictionary["Green"] = false;
            ColorsDictionary["Red"] = false;
        }

        foreach (KeyValuePair<string, bool> pair in ColorsDictionary) //megnézzük hogy igazak e mind
        {
            if (!pair.Value)
            {
                allTrue = false;
                break;
            }
        }

        if (allTrue) //ha mind az összes fegyverdarab megvan
        {
            characterControl.CanShoot = true;
            CanCollect = false;

        }
        else //ha nincs
        {
            characterControl.CanShoot = false;
            CanCollect = true;
        }
    }
}
