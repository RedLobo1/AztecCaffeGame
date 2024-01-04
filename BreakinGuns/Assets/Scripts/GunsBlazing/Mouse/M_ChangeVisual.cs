using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class M_ChangeVisual : MonoBehaviour
{
    [SerializeField] private GameObject _crosshair;
    [SerializeField] private GameObject _laserAiming;
    [SerializeField] private GameObject _dash;
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
            _laserAiming.SetActive(true);
        }
        else
        {
            _crosshair.SetActive(false);
            _dash.SetActive(true);
            _laserAiming.SetActive(false);
        }
    }
}
