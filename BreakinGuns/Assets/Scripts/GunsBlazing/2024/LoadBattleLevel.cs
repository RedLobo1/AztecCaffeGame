using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBattleLevel : MonoBehaviour
{
    
    [SerializeField] private Gun _gun;
    [SerializeField] private GunAttachLogic _viewGun;
    [SerializeField] private GameObject _gunMain;
    void Awake()
    {

        //find the gun object in the scene
        _gunMain = GameObject.Find("GunMain");
        _viewGun = _gunMain.GetComponent<GunAttachLogic>();
        _gun = _viewGun.GetComponent<Gun>();

    }

    public void LoadBattle()
    {
            //add all the mods to the gun when loading the battle scene

            Static_GunModCurrent.currentGun = _gun; 
            SceneManager.LoadScene(0);
    }
}
