using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMod 
{

    public string modName;

    public virtual void ApplyModEffect()
    {
        Debug.Log("Applying mod effect " + modName);
    }
}