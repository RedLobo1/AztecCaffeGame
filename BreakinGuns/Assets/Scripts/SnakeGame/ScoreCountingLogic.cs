using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreCountingLogic : MonoBehaviour
{
    [SerializeField] CheckForPlayer _outOfBoundLogic;
    [SerializeField] TextMeshProUGUI _scoreCount;
    [SerializeField] TextMeshProUGUI _timerCount;

    int _maxScore = 200;
    [SerializeField] private float _maxTime, _currentTime;

    private void Awake()
    {
        _currentTime = _maxTime;
    }
    void Update()
    {
        CountScore();
        TimeCount();
        SetUpCoffeDrop();
        if (_maxScore > 0) return;

        SceneManager.LoadScene(1);
    }

    private void TimeCount()
    {
        _currentTime -= Time.deltaTime;
        _timerCount.text = _currentTime.ToString();
        if(_currentTime > 0) return;
        SceneManager.LoadScene(1);
    }

    private void CountScore()
    {
        if (_outOfBoundLogic.OutOfBound)
        {
            _maxScore--;
        }

        _scoreCount.text = _maxScore.ToString();
    }

    private void SetUpCoffeDrop()
    {
       if(_maxScore >= 150)
       {
            GlobalVariableCollection.CocoaDropCount = 25;
       }
       else if(_maxScore >= 100)
       {
            GlobalVariableCollection.CocoaDropCount = 15;
        }
        else if (_maxScore >= 50)
        {
            GlobalVariableCollection.CocoaDropCount = 10;
        }
        else if (_maxScore > 0)
        {
            GlobalVariableCollection.CocoaDropCount = 8;
        }
    }
}
