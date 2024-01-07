using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMods : MonoBehaviour
{
    InputMaster InputMaster;
    public bool BuildModShotGun;
    public bool BuildPiercing;
    public bool BuildBomb;
    private int _red,_blue, _green;

    [SerializeField] private Vector3 _buildModShotCost; //x = red, y = green, z = blue
    [SerializeField] private Vector3 _buildPiercingCost; //x = red, y = green, z = blue
    [SerializeField] private Vector3 _buildBombCost; //x = red, y = green, z = blue
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

        if (InputMaster.Player.BuildModShotGun.triggered)
        {
            BuildModShotGun = BuildMod(_buildModShotCost, "Shotgun", BuildModShotGun);
        }
        if (InputMaster.Player.BuildPiercing.triggered)
        {
            BuildPiercing = BuildMod(_buildPiercingCost, "Piercing", BuildPiercing);
        }
        if (InputMaster.Player.BuildBomb.triggered)
        {
            BuildBomb = BuildMod(_buildBombCost, "Bomb", BuildBomb);
        }

        if (InputMaster.Player.ResetBuild.triggered)
        {
            if (BuildModShotGun || BuildPiercing || BuildBomb)
            {
                if(BuildModShotGun)
                {
                    PayBack(_buildModShotCost);
                    Debug.Log("Shotgun refunded");
                    BuildModShotGun = false;
                }
                if (BuildPiercing)
                {
                    PayBack(_buildPiercingCost);
                    Debug.Log("Shotgun refunded");
                    BuildPiercing = false;
                }
                if (BuildBomb)
                {
                    PayBack(_buildBombCost);
                    Debug.Log("Shotgun refunded");
                    BuildBomb = false;
                }
            }
        }
    }

    private void PayBack( Vector3 costs)
    {
        CollectedExtraParts.Red += (int)costs.x;
        CollectedExtraParts.Green += (int)costs.y;
        CollectedExtraParts.Blue += (int)costs.z;
    }

    private bool BuildMod(Vector3 costs, string debug, bool modBuilt)
    {
            if (modBuilt) return true;
            if (CollectedExtraParts.Red > costs.x && CollectedExtraParts.Green > costs.y && CollectedExtraParts.Blue > costs.z)
            {
                CollectedExtraParts.Red -= (int)costs.x;
                CollectedExtraParts.Green -= (int)costs.y;
                CollectedExtraParts.Blue -= (int)costs.z;
                Debug.Log(debug);
                return true;
            }
            return false; //this will cause problems
    }
}
