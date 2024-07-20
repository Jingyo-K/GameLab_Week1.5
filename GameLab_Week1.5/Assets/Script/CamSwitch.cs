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
            mainCam.transform.position = new Vector3(-80, 0, 0);
            mainCam.transform.rotation = Quaternion.Euler(0, 90, 0);
            mainCam.rect = new Rect(0.1f, 0.15f, 0.6f, 0.7f);
            firstViewCam.rect = new Rect(0.725f, 0.15f, 0.25f, 0.25f);
        }
    }
    void OnCameraStop(GameEvents gameEvents)
    {
        viewState = mainCam.GetComponent<CamManager>().GetViewState();
        if(viewState == CamManager.ViewState.Top || viewState == CamManager.ViewState.Side)
        {
            mainCam.rect = new Rect(0.1f, 0.15f, 0.6f, 0.7f);
            firstViewCam.rect = new Rect(0.725f, 0.15f, 0.25f, 0.25f);
        }
        else if(viewState == CamManager.ViewState.Front)
        {
            mainCam.transform.position = new Vector3(0, 0, -80);
            mainCam.transform.rotation = Quaternion.Euler(0, 0, 0);
            mainCam.rect = new Rect(0.725f, 0.15f, 0.25f, 0.25f);
            firstViewCam.rect = new Rect(0.1f, 0.15f, 0.6f, 0.7f);
        }
    }
}
