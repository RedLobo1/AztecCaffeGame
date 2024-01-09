using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthLogic : MonoBehaviour
{
    private int _health = 3;
    [SerializeField] private GameObject Good, Ok, Bad;
    [SerializeField] private CharacterControl2D CharacterControl2D;
    void Awake()
    {
        Good.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        _health = CharacterControl2D.Health;
        SetHealth();
        /*if (Input.GetButtonDown("Jump"))
        {
            //_health--;
        }*/
    }

    private void SetHealth()
    {
        if (_health == 2)
        {
            Good.SetActive(true);

            Ok.SetActive(false);
            Bad.SetActive(false);
        }
        else if (_health == 1)
        {
            Ok.SetActive(true);

            Good.SetActive(false);
            Bad.SetActive(false);
        }
        else if (_health == 0)
        {
            Bad.SetActive(true);

            Ok.SetActive(false);
            Good.SetActive(false);
        }
    }
}
