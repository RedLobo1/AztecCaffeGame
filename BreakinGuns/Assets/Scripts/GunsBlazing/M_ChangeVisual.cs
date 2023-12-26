using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_ChangeVisual : MonoBehaviour
{
    [SerializeField] private GameObject _crosshair,_dash;
    [SerializeField] private CharacterControl2D _control; 


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_control.Aiming)
        {
            _crosshair.SetActive(true);
            _dash.SetActive(false);
        }
        else
        {
            _crosshair.SetActive(false);
            _dash.SetActive(true);
        }
    }
}
