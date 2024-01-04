using UnityEngine;

public class Stock : GunMod
{
    public string StockName;

    public bool ChargedShot;
    public override void ApplyModEffect()
    {
        base.ApplyModEffect();
        Debug.Log(StockName);
    }
}