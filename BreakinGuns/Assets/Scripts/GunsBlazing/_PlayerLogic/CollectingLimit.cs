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
    public InputMaster InputMaster;
    private bool _allTrue;

    public Dictionary<string, bool> ColorsDictionary = new Dictionary<string, bool>(); //add to this in another level setup script

    private void OnEnable()
    {
        InputMaster.Player.Enable();
    }

    private void OnDisable()
    {
        InputMaster.Player.Disable();
    }
    private void Awake()
    {
        InputMaster = new InputMaster();
        characterControl = gameObject.GetComponent<CharacterControl2D>();
        
        ShotLogic.ResetShootingLogic += CharacterControl_ResetShootingLogic;
    }

    private void CharacterControl_ResetShootingLogic(object sender, EventArgs e)
    {
        if (CollectedExtraParts.Green >= 2 && CollectedExtraParts.Blue >= 2 && CollectedExtraParts.Red >= 2)
        {
            ColorsDictionary["Blue"] = true;
            ColorsDictionary["Green"] = true;
            ColorsDictionary["Red"] = true;
            CollectedExtraParts.Green--;
            CollectedExtraParts.Blue--;
            CollectedExtraParts.Red--;
            UIGunLogic.Reset = true;
        }
        else
        {
            if (CollectedExtraParts.Green == 1)
            {
                ColorsDictionary["Green"] = false;
            }
            if (CollectedExtraParts.Red == 1)
            {
                
                ColorsDictionary["Red"] = false;
            }
            if (CollectedExtraParts.Blue == 1)
            {
                
                ColorsDictionary["Blue"] = false;
            }

            CollectedExtraParts.Red--;
            CollectedExtraParts.Blue--;
            CollectedExtraParts.Green--;

            UIGunLogic.Reset = true;
        }
    }

    private void Update()
    {

        bool allTrue = true;
        _allTrue = allTrue;

        if (InputMaster.Player.Debug.triggered)
        {
            DebugThis();
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
            //CanCollect = true;

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
