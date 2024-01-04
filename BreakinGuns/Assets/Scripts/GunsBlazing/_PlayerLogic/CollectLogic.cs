using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CollectLogic : MonoBehaviour
{
    UIGunLogic _uiGunLogic;
    CinemachineTargetGroup _cTargetGroup;
    SpriteRenderer _spriteRenderer;
    Collider2D _collider;
    Color _originalColor;
    [SerializeField]private float _invoulnerableSeconds;

    public WeaponPartColors weaponPartColor;

    void Awake()
    {
        GameObject UILogic = GameObject.Find("Gun-Base");
        GameObject CameraTargetGroup = GameObject.Find("TargetGroup");

        _uiGunLogic = UILogic.GetComponent<UIGunLogic>();
        _cTargetGroup = CameraTargetGroup.GetComponent<CinemachineTargetGroup>();

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
        _collider = GetComponent<Collider2D>(); 

        StartCoroutine(Invoulnerable());
        _cTargetGroup.AddMember(transform, 2, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Dictionary<string,bool> ColorsD = collision.gameObject.GetComponent<CollectingLimit>().ColorsDictionary;
            CollectingLimit pCollectingLogic = collision.gameObject.GetComponent<CollectingLimit>();

            if (pCollectingLogic.CanCollect == false) return;

            AddToCollected(ColorsD, collision);
            StartCoroutine(Collected());
        }
    }

    private void AddToCollected(Dictionary<string, bool> ColorsD, Collision2D collision) //set dictionary to true based on color
    {
        if(weaponPartColor == WeaponPartColors.Blue)
        {
            ColorsD["Blue"] = true;
            Debug.Log("blue");
            _uiGunLogic.BlueC = true;
        }
        else if (weaponPartColor == WeaponPartColors.Green)
        {
            ColorsD["Green"] = true;
            Debug.Log("green");
            _uiGunLogic.GreenC = true;
        }
        else if (weaponPartColor == WeaponPartColors.Red)
        {
            ColorsD["Red"] = true;
            Debug.Log("red");
            _uiGunLogic.RedC = true;
        }
        else if (weaponPartColor == WeaponPartColors.Yellow)
        {
            //TODO> IMPLEMENT BLOCK
        }
        else if (weaponPartColor == WeaponPartColors.Pruple)
        {
            //TODO> IMPLEMENT BLOCK
        }
        else if (weaponPartColor == WeaponPartColors.Brown)
        {
            //TODO> IMPLEMENT BLOCK
        }
        collision.gameObject.GetComponent<CollectingLimit>().ColorsDictionary = ColorsD;
    }

    IEnumerator Collected()
    {
        yield return ColorCoroutine();
        yield return null;
        Destroy(gameObject);
    }
    IEnumerator ColorCoroutine()
    {
        
        Time.timeScale = 0f;
        _spriteRenderer.color = Color.white;
        yield return new WaitForSecondsRealtime(0.1f);
        _spriteRenderer.color = _originalColor;
        _cTargetGroup.RemoveMember(transform);

    }
    IEnumerator Invoulnerable()
    {
        float t = 0;
        while(t < _invoulnerableSeconds)
        {
            t += Time.deltaTime;
            yield return null;
        }
        _collider.excludeLayers = LayerMask.NameToLayer("Player");
    }
}
