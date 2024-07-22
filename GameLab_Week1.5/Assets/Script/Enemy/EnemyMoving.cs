using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    public Vector3 RealPosition;
    private Vector3 TopPosition;
    private Vector3 SidePosition;
    private Vector3 curPos;
    private GameObject player;
    private float duration = 0.3f;
    public float speed = 0.1f;
    // Start is called before the first frame update
    private CamManager Cam;
    void Start()
    {
        Cam = GameObject.FindWithTag("MainCamera").GetComponent<CamManager>();
        GameEvents.CameraStop += OnCameraStop;
        player = GameObject.FindWithTag("Player");
        curPos = transform.position;
        float randZ = Random.Range(-19.0f, 19.0f);
        RealPosition = new Vector3(curPos.x, curPos.y, randZ);
        OnCameraStop(null);
    }

    void FixedUpdate()
    {
        curPos -= new Vector3(speed, 0, 0);
        transform.position = curPos;

        if(transform.position.x < -60)
        {
            Destroy(gameObject);
        }
    }

    private void OnCameraStop(GameEvents gameEvents)
    {
        Vector3 startPosition = transform.position;
        RealPosition = new Vector3(curPos.x, RealPosition.y, RealPosition.z);
        TopPosition = new Vector3(curPos.x, player.transform.position.y, RealPosition.z);
        SidePosition = new Vector3(curPos.x, RealPosition.y, player.transform.position.z);
        if(Cam.GetViewState() == CamManager.ViewState.Top)
        {
            StartCoroutine(MoveToPosition(startPosition, TopPosition, duration));
        }
        else if(Cam.GetViewState() == CamManager.ViewState.Side)
        {
            StartCoroutine(MoveToPosition(startPosition, SidePosition, duration));
        }
        else if(Cam.GetViewState() == CamManager.ViewState.Front)
        {
            StartCoroutine(MoveToPosition(startPosition, RealPosition, duration));
        }
    }

    private IEnumerator MoveToPosition(Vector3 startPosition, Vector3 endPosition, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = endPosition;
        curPos = transform.position;
    }

    void OnDestroy()
    {
        GameEvents.CameraStop -= OnCameraStop;
    }
    public void SetRealPosition(Vector3 position)
    {
        RealPosition = position;
    }
}
