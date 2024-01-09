using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class CheckForPlayer : MonoBehaviour
{
    public bool OutOfBound;

    private bool _checkBound;

    [SerializeField] private bool _puzzle;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag == TagCollection.Player)
        {
            if (Input.GetMouseButton(0))
            {
                if (collision.GetComponent<HandLogic>() == null) return;
                HandLogic handLogic = collision.GetComponent<HandLogic>();

                StartCutting(handLogic);
                ChangeToKnife(handLogic);
            }  
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag == TagCollection.Player)
        {
            if (_checkBound == false) return;
            OutOfBound = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag == TagCollection.Player)
        {
            if (_checkBound == false) return;
            OutOfBound = false;
        }
    }
    private void StartCutting(HandLogic handLogic)
    {
        if (handLogic.KnifeState == false) return ;
        handLogic.StartedCutting = true;
        _checkBound = true;
    }

    private void ChangeToKnife(HandLogic handLogic)
    {

        if (handLogic.KnifeState == true) return;

        if(_puzzle == false)
        {
            handLogic.KnifeState = true;
            Debug.Log("pickup");
            Destroy(gameObject);
        }

    }
}
