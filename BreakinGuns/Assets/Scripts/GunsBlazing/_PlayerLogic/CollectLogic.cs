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
    GameLogic _gameLogic;
    Collider2D _collider;
    Color _originalColor;
    [SerializeField]private float _invoulnerableSeconds;

    public WeaponPartColors weaponPartColor;
    private bool _collected;

    void Start()
    {
        _gameLogic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
        _gameLogic.Parts.Add(gameObject);

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

            //if (pCollectingLogic.CanCollect == false) return;

            if (!_collected)
            {
                AddToCollected(ColorsD, collision);
            }
            StartCoroutine(Collected());
        }
    }

    private void AddToCollected(Dictionary<string, bool> ColorsD, Collision2D collision) //set dictionary to true based on color
    {
        if(weaponPartColor == WeaponPartColors.Blue)
        {
            ColorsD["Blue"] = true;
            Debug.Log("blue");
            if (_uiGunLogic.BlueC)
            {
                CollectedExtraParts.Blue++;
            }
            _uiGunLogic.BlueC = true;
        }
        else if (weaponPartColor == WeaponPartColors.Green)
        {
            ColorsD["Green"] = true;
            Debug.Log("green");
            if (_uiGunLogic.GreenC)
            {
                CollectedExtraParts.Green++;
            }
            _uiGunLogic.GreenC = true;
        }
        else if (weaponPartColor == WeaponPartColors.Red)
        {
            ColorsD["Red"] = true;
            Debug.Log("red");
            if (_uiGunLogic.RedC)
            {
                CollectedExtraParts.Red++;
            }
            _uiGunLogic.RedC = true;
        }
        else if (weaponPartColor == WeaponPartColors.Yellow)
        {
            //ColorsD["Yellow"] = true;
            //// Debug.Log("red");
            //_uiGunLogic.YellowC = true;
        }
        else if (weaponPartColor == WeaponPartColors.Pruple)
        {
            //ColorsD["Purple"] = true;
            //// Debug.Log("red");
            //_uiGunLogic.PurpleC = true;
        }
        else if (weaponPartColor == WeaponPartColors.Brown)
        {
            //TODO> IMPLEMENT BLOCK
        }

        _collected = true;
        collision.gameObject.GetComponent<CollectingLimit>().ColorsDictionary = ColorsD;
    }

    IEnumerator Collected()
    {
        yield return ColorCoroutine();
        yield return null;
        _gameLogic.Parts.Remove(gameObject);
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
