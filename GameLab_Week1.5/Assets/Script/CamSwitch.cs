using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public Camera mainCam;
    public Camera firstViewCam;
    private CamManager.ViewState viewState;
    void Start()
    {
        GameEvents.CameraStop += OnCameraStop;
        GameEvents.CameraMoving += OnCameraMoving;
        viewState = mainCam.GetComponent<CamManager>().GetViewState();
    }

    private void OnCameraMoving(GameEvents gameEvents)
    {
        viewState = mainCam.GetComponent<CamManager>().GetViewState();
        if(viewState == CamManager.ViewState.Front)
        {
            mainCam.enabled = true;
            firstViewCam.enabled = false;
        }
    }
    void OnCameraStop(GameEvents gameEvents)
    {
        viewState = mainCam.GetComponent<CamManager>().GetViewState();
        if(viewState == CamManager.ViewState.Top || viewState == CamManager.ViewState.Side)
        {
            mainCam.enabled = true;
            firstViewCam.enabled = false;
        }
        else if(viewState == CamManager.ViewState.Front)
        {
            mainCam.enabled = false;
            firstViewCam.enabled = true;
        }
    }
}
