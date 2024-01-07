using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunAttachLogic : MonoBehaviour
{
    public bool Sight, Stock, Barrel;

    public Gun GunBase ;
    public GameObject SightG,StockG,BarrelG; //GameObjects for the mods
    private GameObject _placeHolder; //GameObjects for the mods

    public int _MaxModCount = 3;
    void Start()
    {
        _placeHolder = new GameObject();
        Gun gun = new Gun();
        //load all previous Mods

        GunBase = gun;

    }

    // Update is called once per frame
    void Update()
    {
        if (Barrel)
        {
            BarrelG = StockG.GetComponent<CheckIfModTrue>().Mod;
        }
        else
        {
            BarrelG = _placeHolder;

        }
        if (Stock)
        {
            StockG = StockG.GetComponent<CheckIfModTrue>().Mod;
        }
        else
        {
            StockG = _placeHolder;

        }
        if (Sight)
        {
            SightG = SightG.GetComponent<CheckIfModTrue>().Mod;
        }
        else
        {
            SightG = _placeHolder;

        }
    }
    
}
