using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static Action<GameEvents> GameStart;
    public Action<GameEvents> GameResume;
    public Action<GameEvents> GamePause;
    public static Action<GameEvents> CameraMoving;
    public static Action<GameEvents> CameraStop;
    public Action<GameEvents> WarningSignal;
    public Action<GameEvents> EpicPatternStart;
    public Action<GameEvents> EpicPatternEnd;
    public Action<GameEvents> GameOver;

    public void CallOnGameStart()
    {
        GameStart?.Invoke(this);
    }

    public void CallOnGameResume()
    {
        GameResume?.Invoke(this);
    }

    public void CallOnGamePause()
    {
        GamePause?.Invoke(this);
    }

    public void CallCameraMoving()
    {
        CameraMoving?.Invoke(this);
    }

    public void CallCameraStop()
    {
        CameraStop?.Invoke(this);
    }
    
    public void CallOnWarningSignal()
    {
        WarningSignal?.Invoke(this);
    }

    public void CallOnEpicPatternStart()
    {
        EpicPatternStart?.Invoke(this);
    }

    public void CallOnEpicPatternEnd()
    {
        EpicPatternEnd?.Invoke(this);
    }

    public void CallOnGameOver()
    {
        GameOver?.Invoke(this);
    }
}
