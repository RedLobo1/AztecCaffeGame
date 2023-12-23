using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public event EventHandler ImpactEvent;

    private bool _shotEvent;
    public bool ShotEvent
    {
        get { return _shotEvent; }
        set 
        { 

          ImpactEvent.Invoke(this, EventArgs.Empty);
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(0);
        }
    }
}
