using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC_PickUpPartLogic : MonoBehaviour
{
    [SerializeField] private AIDestinationSetter _destination;
    [SerializeField] private LayerMask _colletLayer;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private GameLogic _gameLogic;

    public GameObject PickUp;
    public GameObject DropPoint;
    private GameObject _pickedUpPart;


    private GameObject _nullPart;
    [SerializeField] private int _maxParts = 3;

    private bool _hit;
    private bool _pickedUp;
    private bool _startLooking;

    void Start()
    {
        _gameLogic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
        _nullPart = new GameObject();
        _pickedUpPart = _nullPart;
        PickUp = DropPoint;
        DropPoint = GameObject.Find("DropPoint");

        if (_gameLogic.Parts.Count > 0)
        {
            int ObjectToGrab = Random.Range(0, _gameLogic.Parts.Count - 1);
            string ObjectName = _gameLogic.Parts[ObjectToGrab].name;
            PickUp = GameObject.Find(ObjectName);
            _pickedUp = false;
            _hit = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (_pickedUp == true || PickUp == null)
        {
            _destination.target = DropPoint.transform;
        }

        if (_gameLogic.Parts.Count > 0 && !_pickedUp)
        {
            _startLooking = true;
        }
        if (_startLooking)
        {
            StartCoroutine(WasHit());
            _startLooking = false;
        } 
        if (_pickedUpPart == _nullPart)
        {
            _destination.target = PickUp.transform;
        }

        if (_hit) return;

        _pickedUpPart.transform.position = transform.position - new Vector3(0,-0.5f,0);

    }

    private void LetGoPart(Collision2D other)
    {
        _pickedUpPart = _nullPart;
        _hit = true;
        StartCoroutine(WasHit());

    }

    private IEnumerator WasHit()
    {
   
        yield return new WaitForSeconds(2f);

        if (_gameLogic.Parts.Count > 0)
        {
            int ObjectToGrab = Random.Range(0, _gameLogic.Parts.Count-1);
            string ObjectName = _gameLogic.Parts[ObjectToGrab].name;
            PickUp = GameObject.Find(ObjectName);
            _pickedUp = false;
            _hit = false;
        }
    }

    private void PickupPart(Collision2D other)
    {
        _pickedUpPart = other.gameObject;
        _pickedUp = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            Debug.Log("Grab");
            PickupPart(other);
        }
        if (other.gameObject.layer == 6)
        {
            LetGoPart(other);
        }
    }
}
