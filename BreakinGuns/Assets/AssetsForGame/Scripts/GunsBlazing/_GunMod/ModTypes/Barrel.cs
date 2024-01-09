using UnityEngine;

public class Barrel : GunMod
{
    public string BarrelName;

    public int Shots;

    public bool Explosive;

    public bool ModShot;
    //tag
    public override void ApplyModEffect()
    {
        base.ApplyModEffect();
        Debug.Log(BarrelName );
    }
}
//create a list of Bullet types inside in game gun and save 2 prefabs there, chose based on the gun mod
//add bullet prefab inside the script and create Dictionary<string, GameObject> and save the prefabs there