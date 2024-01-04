using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashDestroy : MonoBehaviour
{
    private float _time;
    [SerializeField] private float _timer;
    // Update is called once per frame
    void Update()
    {
        if (_time < _timer)
        {
            _time += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
