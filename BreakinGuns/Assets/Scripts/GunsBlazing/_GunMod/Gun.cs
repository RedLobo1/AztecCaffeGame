using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string GunName;
    public int ShotCount;

    //all attachedMods will be called in order
    private List<GunMod> attachedMods = new List<GunMod>();

    public void AddMod(GunMod mod)
    {
        attachedMods.Add(mod);
    }

    public void DebugAllMods()
    {
        foreach (var gunMod in attachedMods)
        {
            Debug.Log(gunMod.modName);
        }
    }
    public void RemoveMod(GunMod mod)
    {
        attachedMods.Remove(mod);
    }
}
