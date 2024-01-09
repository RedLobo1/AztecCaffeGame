using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateIcons : MonoBehaviour
{

    [SerializeField] private GameObject Purple, Yellow, Brown;
    private Image PurpleSR, YellowSR, BrownSR;
    [SerializeField] private GameObject BuildModGameObject;
    private BuildMods _buildMods;
    private void Awake()
    {
        PurpleSR = Purple.GetComponent<Image>();
        YellowSR = Yellow.GetComponent<Image>();
        BrownSR = Brown.GetComponent<Image>();

        PurpleSR.color= Color.gray;
        YellowSR.color = Color.gray;
        BrownSR.color = Color.gray;

        _buildMods = BuildModGameObject.GetComponent<BuildMods>();
    }

    void Update()
    {
        SetColor(PurpleSR,Color.white, _buildMods.BuildModShotGun);
        SetColor(YellowSR, Color.white, _buildMods.BuildPiercing);
        SetColor(BrownSR, Color.white, _buildMods.BuildBomb);
        //TODO> implement other colors

    }

    private void SetColor(Image sRenderer,Color color, bool modBool)
    {
        if (modBool)
        {
            sRenderer.color = color;
        }
        else
        {
            sRenderer.color = Color.gray;
        }
    }
}
