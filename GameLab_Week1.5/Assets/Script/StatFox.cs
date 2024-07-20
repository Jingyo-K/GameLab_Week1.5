using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatFox : MonoBehaviour
{
    public float speed = 10f; // 이동 속도
    public float tiltAmount = 15f; // 기체가 기울어지는 정도
    public float rotationSpeed = 4f; // 기체 회전 속도

    private float horizontalInput;
    private float verticalInput;

    CamManager.ViewState viewState;
    void Start()
    {
        viewState = GameObject.FindWithTag("MainCamera").GetComponent<CamManager>().GetViewState();
        GameEvents.CameraStop += OnCameraStop;
        OnCameraStop(null);
    }
    void Update()
    {
        if(viewState == CamManager.ViewState.Front)
        {
            // 입력 값 가져오기
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            // 기체 이동

            // 기체 회전
            Quaternion targetRotation = Quaternion.Euler(-horizontalInput * tiltAmount, 0, verticalInput * tiltAmount);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void OnCameraStop(GameEvents gameEvents)
    {
        viewState = GameObject.FindWithTag("MainCamera").GetComponent<CamManager>().GetViewState();
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}