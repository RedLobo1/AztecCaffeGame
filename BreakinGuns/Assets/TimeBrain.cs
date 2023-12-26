using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBrain : MonoBehaviour
{
    private TimeStates _timeStatesP = TimeStates.Normal;
    public TimeStates TimeStates
    {
        get { return _timeStatesP; }
        set
        {
            if(TimeStates == TimeStates.Stop)
            {
                for( int i = 1; i < 10; i++)
                {
                    Time.timeScale = 0f;
                }
                Time.timeScale = 1f;
                TimeStates = TimeStates.Normal;
                _timeStatesP = value;
            }
            else if(TimeStates == TimeStates.SlowDown)
            {
                Time.timeScale = 0.15f;
                _timeStatesP = value;
            }
            else
            {
                Time.timeScale = 1f;
                _timeStatesP = value;
            }
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
