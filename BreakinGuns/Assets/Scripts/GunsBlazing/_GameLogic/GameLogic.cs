using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    //public event EventHandler ImpactEvent;
    public GameObject Neni;
    [SerializeField] float _shakeIntensity, _time;
    private bool _shotEvent;
    public bool ShotEvent
    {
        get { return _shotEvent; }
        set 
        { 

          ShakeLogic.Instance.ShakeCameraUpdated(_shakeIntensity, _time);
          _shotEvent = value;
        }
    }
    void Awake()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.L))
        {
            //SceneManager.LoadScene(0);
            Instantiate(Neni);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(0);
        }*/

    }
}