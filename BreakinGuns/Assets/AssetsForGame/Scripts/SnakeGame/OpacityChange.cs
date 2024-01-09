using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityChange : MonoBehaviour
{
    private bool _outOfBound;
    [SerializeField] private float _alphaStrenght;
    [SerializeField]  private float _maxTimer,_currentTimer;

    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public bool OutOfBound
    {
        get => _outOfBound;
        set
        {
            if (_outOfBound == value)
            {
                return;
            }

            
            if (_outOfBound == false)
            {
                _spriteRenderer.color = new Color(1, 0, 0, 0);
                _outOfBound = value;
            }
            else if (_outOfBound == true)
            {

               StartCoroutine(MoveRightCoroutine());
                _outOfBound = value;
            }

        }
    }
    IEnumerator MoveRightCoroutine()
    {
            _spriteRenderer.color = new Color(1, 0, 0, _alphaStrenght);
            yield return new WaitForSeconds(0.4f);
            _spriteRenderer.color = new Color(1, 0, 0, 0);
            yield return null;
    }
}
