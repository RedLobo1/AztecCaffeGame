using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using CodeMonkey.Utils;
using CodeMonkey;

public class DragLogic : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool _clicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        _clicked = !_clicked;
    }

    // Update is called once per frame
    void Update()
    {
        if (_clicked)
        {
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
        }

    }


}
