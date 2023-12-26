using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGunLogic : MonoBehaviour
{
    [SerializeField] private GameObject Red,Blue,Green;
    public bool RedC,BlueC,GreenC;
    public bool Reset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (RedC)
        {
            Red.SetActive(true);
        }
        if (BlueC)
        {
            Blue.SetActive(true);
        }
        if (GreenC)
        {
            Green.SetActive(true);
        }
        if (Reset)
        {
            RedC = false;
            BlueC = false;
            GreenC = false;
            Red.SetActive(false);
            Blue.SetActive(false);
            Green.SetActive(false);
            Reset = false;
        }
    }
}
