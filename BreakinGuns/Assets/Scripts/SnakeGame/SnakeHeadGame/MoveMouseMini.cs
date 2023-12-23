using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMouseMini : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
    }
}
