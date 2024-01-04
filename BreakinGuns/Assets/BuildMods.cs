using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMods : MonoBehaviour
{
    InputMaster InputMaster;
    public bool BuildModShotGun;
    private int _red,_blue, _green;
    [SerializeField] private int _redCost ,_blueCost, _greenCost; //make it a vector3
    //create 2 more
    private void OnEnable()
    {
        InputMaster.Player.Enable();
    }

    private void OnDisable()
    {
        InputMaster.Player.Disable();
    }

    void Awake()
    {
        InputMaster = new InputMaster();
    }


    // Update is called once per frame
    void Update()
    {
        //todo> try to make method out of this that can be reused also include other mods

        if (InputMaster.Player.BuildModShotGun.triggered)
        {
            if (CollectedExtraParts.Blue > _blueCost && CollectedExtraParts.Red > _redCost)
            {
                CollectedExtraParts.Blue -= _blueCost;
                CollectedExtraParts.Red -= _redCost;
                Debug.Log("Shotgun");
                BuildModShotGun = true;
            }
        }

        if (InputMaster.Player.ResetBuild.triggered)
        {
            if (BuildModShotGun)
            {
                CollectedExtraParts.Blue += _blueCost;
                CollectedExtraParts.Red += _redCost;
                BuildModShotGun = false;
            }
        }
    }
}
