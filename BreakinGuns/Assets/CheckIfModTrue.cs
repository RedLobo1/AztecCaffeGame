using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfModTrue : MonoBehaviour
{
    [SerializeField] private string _modTag;
    [SerializeField] private string _boolName;

    [SerializeField] GunAttachLogic _GunLogic;
    public GameObject Mod ;
    private GameObject _placeHolderMod;

    private void Awake()
    {
        _placeHolderMod = new GameObject();
    }

    void SetBoolValue(string boolName, bool value)
    {
        // Use reflection to set the boolean property by name
        System.Reflection.FieldInfo field = _GunLogic.GetType().GetField(boolName);

        if (field != null && field.FieldType == typeof(bool))
        {
            field.SetValue(_GunLogic, value);

        }
        else
        {
            Debug.LogError("Invalid boolean property name or type: " + boolName);
            // Or handle the error accordingly
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == _modTag)
        {
            SetBoolValue(_boolName, true);
            Mod = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == _modTag)
        {
            SetBoolValue(_boolName, false);
            Mod = _placeHolderMod;
        }
    }
}
