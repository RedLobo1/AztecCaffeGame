using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectingLimit : MonoBehaviour
{
    public UIGunLogic UIGunLogic;
    CharacterControl2D characterControl;
    public GunShotLogic ShotLogic;
    public bool CanCollect;

    private bool _allTrue;

    public Dictionary<string, bool> ColorsDictionary = new Dictionary<string, bool>(); //add to this in another level setup script

    private void Awake()
    {

        characterControl = gameObject.GetComponent<CharacterControl2D>();
        ShotLogic.ResetShootingLogic += CharacterControl_ResetShootingLogic;
    }

    private void CharacterControl_ResetShootingLogic(object sender, EventArgs e)
    {
        ColorsDictionary["Blue"] = false;
        ColorsDictionary["Green"] = false;
        ColorsDictionary["Red"] = false;
        UIGunLogic.Reset = true;
    }

    private void Update()
    {

        bool allTrue = true;
        _allTrue = allTrue;


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

    private void DebugThis()
    {
        foreach (KeyValuePair<string, bool> pair in ColorsDictionary) //megnézzük hogy igazak e mind
        {
            Debug.Log($"{pair.Key} has {pair.Value}");
        }
    }
}
