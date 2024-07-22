using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    Rigidbody rb;
    GameObject player;
    GameEvents gameEvents;
    public Vector3 RealPosition;
    private Vector3 SidePosition;
    private Vector3 curPos;
    private float duration = 0.05f;
    private CamManager Cam;
    public bool isMagnet = false;
    public bool goTo = false;
    public int score = 0;
    void Start()
    {
        curPos = transform.position;
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.right * 20;
        gameEvents = GameObject.Find("GameEventManager").GetComponent<GameEvents>();
        gameEvents.CallOnItemCollected();
        GameEvents.CameraStop += OnCameraStop;
        Cam = GameObject.FindWithTag("MainCamera").GetComponent<CamManager>();
        OnCameraStop(null);
    }

    private void OnCameraStop(GameEvents gameEvents)
    {
        Vector3 startPosition = transform.position;
        RealPosition = new Vector3(curPos.x, RealPosition.y, RealPosition.z);
        SidePosition = new Vector3(curPos.x, RealPosition.y, player.transform.position.z);
        if(Cam.GetViewState() == CamManager.ViewState.Side)
        {
            StartCoroutine(MoveToPosition(startPosition, SidePosition, duration));
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
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * 100 * Time.deltaTime);
        curPos = transform.position;
        if(!isMagnet)
        {
            rb.AddForce(Vector3.right * -20);
        }
        else if (!goTo)
        {
            rb.velocity = Vector3.zero;
            goTo = true;
        }
        else
        {
            rb.velocity = (player.transform.position - transform.position).normalized * 40;
        }
        if(transform.position.x < -60)
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    void OnDestroy()
    {
        Score.score += score;
        gameEvents.CallOnItemCollected();
        GameEvents.CameraStop -= OnCameraStop;
    }
    public void SetRealPosition(Vector3 position)
    {
        RealPosition = position;
    }
}
