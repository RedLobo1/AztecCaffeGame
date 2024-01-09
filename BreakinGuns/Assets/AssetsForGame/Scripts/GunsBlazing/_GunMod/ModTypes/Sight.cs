using UnityEngine;

public class Sight : GunMod
{
    public string BarrelName;

    public bool LaserSight;

    public bool Jugler;

    public bool RunModRun;
    public override void ApplyModEffect()
    {
        base.ApplyModEffect();
        Debug.Log(BarrelName);
    }
}