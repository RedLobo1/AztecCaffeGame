using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariableCollection
{
    public static int CocoaDropCount;

    public static Vector3 LastCharacterLocation;


}
public static class MousePositionInworld
{
    public static Vector3 MousePosition;
    static MousePositionInworld()
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
