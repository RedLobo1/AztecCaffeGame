using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectLogic : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    Collider2D _collider;
    Color _originalColor;
    [SerializeField]private float _invoulnerableSeconds;

    public WeaponPartColors weaponPartColor;

    void Awake()
    {

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
        _collider = GetComponent<Collider2D>(); 

        StartCoroutine(Invoulnerable());
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
        }
        else if (weaponPartColor == WeaponPartColors.Green)
        {
            ColorsD["Green"] = true;
            Debug.Log("green");
        }
        else if (weaponPartColor == WeaponPartColors.Red)
        {
            ColorsD["Red"] = true;
            Debug.Log("red");
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
