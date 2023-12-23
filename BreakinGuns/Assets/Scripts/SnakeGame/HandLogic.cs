using DrawLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLogic : MonoBehaviour
{
    private bool _kinfeState, _startedCutting;
    public bool AtStart, AtEnd;
    private GameViewLogic _gameViewLogic;
    public GameObject KnifeLookVisual, HandViewVisual;
    public DrawMesh DrawMesh;
    public bool KnifeState
    {
        get => _kinfeState;
        set
        {
            if(_kinfeState == value)
            {
                return;
            }
            _kinfeState = value;

            KnifeLookVisual.SetActive(true);
            HandViewVisual.SetActive(false);
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.02f, 0.02f);
        }
    }
    public bool StartedCutting
    {
        get => _startedCutting;
        set
        {
            if (_startedCutting == value || !AtStart)
            {
                return;
            }
            _startedCutting = value;
            DrawMesh.Drawing = true;
        }
    }

    void Awake()
    {
        _gameViewLogic = FindObjectOfType<GameViewLogic>();
        KnifeLookVisual.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
