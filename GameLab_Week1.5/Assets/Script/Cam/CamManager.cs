using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private Transform center;
    [SerializeField] private float orbitSpeed = 10.0f;

    public enum ViewState
    {
        Top,
        Side,
        Front
    }
    [SerializeField] private ViewState viewState = ViewState.Side;
    private bool isOrbiting = false;

    private struct CamAngleData
    {
        public float x;
        public float y;
        public float z;
    }
    private CamAngleData camAngleData;
    void Start()
    {
        gameEvents = GameManager.instance.gameEvents;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isOrbiting)
        {
            /*if(Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(MoveCamera(0));
            }*/
            /*if(Input.GetKeyDown(KeyCode.W))
            {
                StartCoroutine(MoveCamera(1));
            }*/
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(viewState == ViewState.Front)
                {
                    StartCoroutine(MoveCamera(2));
                }
                else if(viewState == ViewState.Side)
                {
                    StartCoroutine(MoveCamera(1));
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Time.timeScale += 0.1f;
            }
        }
    }

    IEnumerator MoveCamera(int dir)
    {
        gameEvents = GameManager.instance.gameEvents;
        isOrbiting = true;
        float rotProgress = 0;
        float curTimeScale = Time.timeScale;
        Time.timeScale = 0.0f;
        if(dir == 0) //going to topview
        {
            if(viewState == ViewState.Top)
            {
                isOrbiting = false;
                yield return null;
            }
            else if(viewState == ViewState.Side)
            {
                while (true)
                {
                    float step = orbitSpeed*Time.unscaledDeltaTime;

                    if (rotProgress + step > 90)
                    {
                        step = 90 - rotProgress;
                    }
                    transform.RotateAround(center.position, transform.up, step);
                    rotProgress += step;
                    if(rotProgress >= 90)
                    {
                        break;
                    }
                    yield return null;            
                }

                rotProgress = 0;

                while (true)
                {
                    float step = orbitSpeed*Time.unscaledDeltaTime;

                    if (rotProgress + step > 90)
                    {
                        step = 90 - rotProgress;
                    }
                    transform.RotateAround(center.position, transform.right, step);
                    rotProgress += step;
                    if(rotProgress >= 90)
                    {
                        break;
                    }
                    yield return null;            
                }
                transform.rotation = Quaternion.Euler(90, 90, 0);
                viewState = ViewState.Top;
                gameEvents.CallCameraStop();
            }
            else if(viewState == ViewState.Front)
            {
                gameEvents.CallCameraMoving();
                while (true)
                {
                    float step = orbitSpeed*Time.unscaledDeltaTime;

                    if (rotProgress + step > 90)
                    {
                        step = 90 - rotProgress;
                    }
                    transform.RotateAround(center.position, transform.right, step);
                    rotProgress += step;
                    if(rotProgress >= 90)
                    {
                        break;
                    }
                    yield return null;            
                }
                viewState = ViewState.Top;
                gameEvents.CallCameraStop();
            }        
            transform.position = new Vector3(0, 80, 0);
        }

        else if(dir == 1) //going to frontview
        {

            if(viewState == ViewState.Front)
            {
                isOrbiting = false;
                yield return null;
            }
            else if(viewState == ViewState.Side)
            {
                gameEvents.CallCameraMoving();
                while (true)
                {
                    float step = orbitSpeed*Time.unscaledDeltaTime;

                    if (rotProgress + step > 90)
                    {
                        step = 90 - rotProgress;
                    }
                    transform.RotateAround(center.position, transform.up, step);
                    rotProgress += step;
                    if(rotProgress >= 90)
                    {
                        break;
                    }
                    yield return null;            
                }

                rotProgress = 0;

                transform.rotation = Quaternion.Euler(0, 90, 0);
                viewState = ViewState.Front;
                gameEvents.CallCameraStop();
            }
            else if(viewState == ViewState.Top)
            {
                gameEvents.CallCameraMoving();
                while (true)
                {
                    float step = orbitSpeed*Time.unscaledDeltaTime;

                    if (rotProgress + step > 90)
                    {
                        step = 90 - rotProgress;
                    }
                    transform.RotateAround(center.position, -transform.right, step);
                    rotProgress += step;
                    if(rotProgress >= 90)
                    {
                        break;
                    }
                    yield return null;            
                }
                viewState = ViewState.Front;
                gameEvents.CallCameraStop();
            }
            transform.position = new Vector3(-80, 0, 0);    
        }

        else if(dir == 2) //going to sideview
        {
            if(viewState == ViewState.Side)
            {
                isOrbiting = false;
                yield return null;
            }
            else if(viewState == ViewState.Top)
            {
                while (true)
                {
                    float step = orbitSpeed*Time.unscaledDeltaTime;

                    if (rotProgress + step > 90)
                    {
                        step = 90 - rotProgress;
                    }
                    transform.RotateAround(center.position, -transform.right, step);
                    rotProgress += step;
                    if(rotProgress >= 90)
                    {
                        break;
                    }
                    yield return null;            
                }

                rotProgress = 0;

                while (true)
                {
                    float step = orbitSpeed*Time.unscaledDeltaTime;

                    if (rotProgress + step > 90)
                    {
                        step = 90 - rotProgress;
                    }
                    transform.RotateAround(center.position, -transform.up, step);
                    rotProgress += step;
                    if(rotProgress >= 90)
                    {
                        break;
                    }
                    yield return null;            
                }      
                transform.rotation = Quaternion.Euler(0, 0, 0);
                viewState = ViewState.Side;
                gameEvents.CallCameraStop();
            }
            else if(viewState == ViewState.Front)
            {
                gameEvents.CallCameraMoving();
                while (true)
                {
                    float step = orbitSpeed*Time.unscaledDeltaTime;

                    if (rotProgress + step > 90)
                    {
                        step = 90 - rotProgress;
                    }
                    transform.RotateAround(center.position, -transform.up, step);
                    rotProgress += step;
                    if(rotProgress >= 90)
                    {
                        break;
                    }
                    yield return null;            
                }
                viewState = ViewState.Side;
                gameEvents.CallCameraStop();

            }
            transform.position = new Vector3(0, 0, -80);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
        /*gameEvents.CallCameraMoving();
        if(dir == 2)
        {
            viewState = ViewState.Side;
        }
        else
        {
            viewState = ViewState.Front;
        }*/
        gameEvents.CallCameraStop();
        Time.timeScale = curTimeScale;
        SetCamAngleData();
        isOrbiting = false;
        yield return null;
    }

    private void SetCamAngleData()
    {
        camAngleData.x = transform.rotation.eulerAngles.x;
        camAngleData.y = transform.rotation.eulerAngles.y;
        camAngleData.z = transform.rotation.eulerAngles.z;
    }

    public void GetCamAngleData(Transform rot)
    {
        rot.transform.rotation = Quaternion.Euler(camAngleData.x, camAngleData.y, camAngleData.z);
    }

    public ViewState GetViewState()
    {
        return viewState;
    }
}
