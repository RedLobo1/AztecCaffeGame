using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RGBCounter : MonoBehaviour
{
    public TextMeshProUGUI redMesh, blueMesh, greenMesh;

    void Update()
    {
        redMesh.text = (CollectedExtraParts.Red ).ToString();
        blueMesh.text = (CollectedExtraParts.Blue ).ToString();
        greenMesh.text = (CollectedExtraParts.Green).ToString();

        //Debug.Log($"Red is:{CollectedExtraParts.Red}");
        //Debug.Log($"Blue is:{CollectedExtraParts.Blue}");
        //Debug.Log($"Green is:{CollectedExtraParts.Green}");

    }
}
