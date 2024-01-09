using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    private InputMaster master;
    public List<GameObject> Parts;

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
        Parts = new List<GameObject>();
        Cursor.visible = false;
        master = new InputMaster();
    }

    private void OnEnable()
    {
        master.Enable();
    }
    private void OnDisable()
    {
        master.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (master.Player.Debug.triggered)
        {
            CollectedExtraParts.Blue = 3;
            CollectedExtraParts.Red = 3;
            CollectedExtraParts.Green = 3;
            SceneManager.LoadScene(0);
        }

    }
}
