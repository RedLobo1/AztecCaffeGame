using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CocoaDropCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cocoaCount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _cocoaCount.text = $"Cocoa count:{GlobalVariableCollection.CocoaDropCount}";
    }
}
