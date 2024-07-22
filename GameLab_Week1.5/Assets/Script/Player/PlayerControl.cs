using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]private CamManager.ViewState viewState;
    public float speed = 10.0f;
    Rigidbody rb;
    public float maxDistance = 100f; // 레이캐스트 최대 거리

    void Start()
    {
        CamManager camManager = GameObject.FindWithTag("MainCamera").GetComponent<CamManager>();
        viewState = camManager.GetViewState();
        GameEvents.CameraMoving += OnCameraMoving;
        GameEvents.CameraStop += OnCameraStop;
        GameEvents.GameOver += OnGameOver;
        OnCameraStop(null);
        rb = GetComponent<Rigidbody>();
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        Vector3 movementUpDown = Vector3.zero;
        Vector3 movementLeftRight = Vector3.zero;
        Vector3 movementForwardBackward = Vector3.zero;

        if(viewState == CamManager.ViewState.Top)
        {
            if(Input.GetKey(KeyCode.W))
            {
                movementLeftRight = transform.right;
            }
            else if(Input.GetKey(KeyCode.S))
            {
                movementLeftRight = -transform.right;
            }
            if(Input.GetKey(KeyCode.A))
            {
                
                movementForwardBackward = transform.forward;
                
            }
            else if(Input.GetKey(KeyCode.D))
            {
                movementForwardBackward = -transform.forward;
            }
        }
        
    

        else if(viewState == CamManager.ViewState.Side)
        {
            if(Input.GetKey(KeyCode.W))
            {
                movementUpDown = transform.up;
            }
            else if(Input.GetKey(KeyCode.S))
            {
                movementUpDown = -transform.up;
            }
            if(Input.GetKey(KeyCode.A))
            {
                movementLeftRight = -transform.right;
            }
            else if(Input.GetKey(KeyCode.D))
            {
                
                movementLeftRight = transform.right;
            }
        }

        else if(viewState == CamManager.ViewState.Front)
        {
            if(Input.GetKey(KeyCode.W))
            {
                movementUpDown = transform.up;
            }
            else if(Input.GetKey(KeyCode.S))
            {
                movementUpDown = -transform.up;
            }
            if(Input.GetKey(KeyCode.A))
            {
                movementForwardBackward = transform.forward;
            }
            else if(Input.GetKey(KeyCode.D))
            {
                movementForwardBackward = -transform.forward;
            }
        }

        rb.velocity = (movementUpDown + movementLeftRight + movementForwardBackward).normalized* speed;
    }


    public void OnCameraStop(GameEvents gameEvents)
    {
        CamManager camManager = GameObject.FindWithTag("MainCamera").GetComponent<CamManager>();
        viewState = camManager.GetViewState();
        if(viewState == CamManager.ViewState.Top)
        {
            transform.GetChild(2).gameObject.transform.localPosition = new Vector3(-3, 0, 0);
            transform.GetChild(2).gameObject.transform.localRotation = Quaternion.Euler(90, 90, 0);
            transform.GetChild(3).gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            speed = 50;
        }
        else if(viewState == CamManager.ViewState.Side)
        {
            transform.GetChild(2).gameObject.transform.localPosition = new Vector3(0, -3, 0);
            transform.GetChild(2).gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.GetChild(3).gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0); 
            GetComponent<CapsuleCollider>().center = new Vector3(1, 0, 0);
            GetComponent<CapsuleCollider>().height = 6;
            GetComponent<CapsuleCollider>().radius = 1.5f;
            speed = 50;
        }
        else if(viewState == CamManager.ViewState.Front)
        {
            transform.GetChild(2).gameObject.transform.localPosition = new Vector3(0, -3, 0);
            transform.GetChild(2).gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            speed = 20;
            GetComponent<CapsuleCollider>().center = new Vector3(5, 0, 0);
            GetComponent<CapsuleCollider>().height = 2;
        }
    }

    private void OnCameraMoving(GameEvents gameEvents)
    {
        if(viewState == CamManager.ViewState.Front)
        {
            transform.GetChild(4).gameObject.SetActive(false);
            /*transform.GetChild(5).gameObject.SetActive(false);
            transform.GetChild(6).gameObject.SetActive(false);
            transform.GetChild(7).gameObject.SetActive(false);*/
        }
        else
        {
            transform.GetChild(4).gameObject.SetActive(true);
            /*transform.GetChild(5).gameObject.SetActive(true);
            transform.GetChild(6).gameObject.SetActive(true);
            transform.GetChild(7).gameObject.SetActive(true);*/
        }   
    }

    private void OnGameOver(GameEvents gameEvents)
    {
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
    void OnDestroy()
    {
        GameEvents.CameraMoving -= OnCameraMoving;
        GameEvents.CameraStop -= OnCameraStop;
        GameEvents.GameOver -= OnGameOver;
    }
}
