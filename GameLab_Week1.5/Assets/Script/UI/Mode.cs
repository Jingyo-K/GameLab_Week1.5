using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mode : MonoBehaviour
{
    CamManager.ViewState viewState;
    void Start()
    {
        viewState = GameObject.FindWithTag("MainCamera").GetComponent<CamManager>().GetViewState();
        GameEvents.CameraStop += OnCameraStop;
        
    }

    // Update is called once per frame
    void OnCameraStop(GameEvents gameEvents)
    {
        viewState = GameObject.FindWithTag("MainCamera").GetComponent<CamManager>().GetViewState();
        if(viewState == CamManager.ViewState.Side)
        {
            transform.GetChild(0).transform.localScale = new Vector3(1.2f, 1.2f, 1);
            transform.GetChild(1).transform.localScale = new Vector3(0.8f, 0.8f, 1);
        }
        else
        {
            transform.GetChild(0).transform.localScale = new Vector3(0.8f, 0.8f, 1);
            transform.GetChild(1).transform.localScale = new Vector3(1.2f, 1.2f, 1);
        }
    }

    void OnDestroy()
    {
        GameEvents.CameraStop -= OnCameraStop;
    }   
}
