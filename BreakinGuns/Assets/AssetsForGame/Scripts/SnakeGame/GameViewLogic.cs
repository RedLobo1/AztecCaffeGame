using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameViewLogic : MonoBehaviour
{
   // GameStates Gamestate;

    public event EventHandler ShakeCameraEvent;
    public HandLogic HandLogic;
    public CheckForPlayer SnakeHeadBound;
    public OpacityChange OutOfBoundVisual;

    [SerializeField] private SpriteRenderer SnakeHead;

    private bool _atStart, _atEnd;
    private void Awake()
    {
    Cursor.visible = false;
    }
    void Update()
    {
        CheckStates();
    }

    private void CheckStates()
    {

        if (HandLogic.AtEnd == true)
        {
            SceneManager.LoadScene(0);
        }
        if (HandLogic.StartedCutting == true)
        {
            //Gamestate = GameStates.Cutting;
            if(SnakeHeadBound.OutOfBound)
            {
                ShakeCameraEvent.Invoke(this, EventArgs.Empty);
                OutOfBoundVisual.OutOfBound = true;
            }
            else if (!SnakeHeadBound.OutOfBound)
            {
                OutOfBoundVisual.OutOfBound = false;
            }
        }
        else if (HandLogic.KnifeState == true)
        {
           // Gamestate = GameStates.Knife;
            SnakeHead.color = new Color(0.5f,0.5f, 0.5f);
        }

    }
}
