using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGunLogic : MonoBehaviour
{
    [SerializeField] private GameObject Red,Blue,Green;
    public bool RedC,BlueC,GreenC,YellowC,PurpleC;
    public bool Reset;
    void Start()
    {
        RedC = true;
        BlueC = true;
        GreenC = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (CollectedExtraParts.Red >= 1)
        {
            Red.SetActive(true);
        }
        if (CollectedExtraParts.Blue >= 1)
        {
            Blue.SetActive(true);
        }
        if (CollectedExtraParts.Green >= 1)
        {
            Green.SetActive(true);
        }
        
        if (Reset)
        {
            Red.SetActive(false);
            Blue.SetActive(false);
            Green.SetActive(false);
            Reset = false;
        }
    }
}
