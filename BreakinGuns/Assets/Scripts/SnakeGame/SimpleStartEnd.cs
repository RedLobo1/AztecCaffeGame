using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleStartEnd : MonoBehaviour
{
    [SerializeField] private bool _start, _end;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag == TagCollection.Player)
        {
            if (collision.GetComponent<HandLogic>() == null) return;
            HandLogic handLogic = collision.GetComponent<HandLogic>();

            if (_start && handLogic.KnifeState)
            {
                handLogic.AtStart = true;
            }

            if (!handLogic.AtStart) return;

            if (_end && handLogic.StartedCutting)
            {
                handLogic.AtEnd = true;

            }
            
        }
    }
}
