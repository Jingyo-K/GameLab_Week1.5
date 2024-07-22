using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazerGen : MonoBehaviour
{
    // Start is called before the first frame update
    CamManager.ViewState viewState;
    public GameObject Yrazer;
    public GameObject Zrazer;
    public GameObject player;
    public float genCoolTime = 5.0f;
    void Start()
    {
        GameEvents.CameraStop += OnCameraStop;
        GameEvents.GameOver += OnGameOver;
        GameEvents.BossStart += OnBossStart;
        GameEvents.BossEnd += OnBossEnd;
        viewState = GameObject.FindWithTag("MainCamera").GetComponent<CamManager>().GetViewState();
        StartCoroutine(RoopMakeRazer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCameraStop(GameEvents gameEvents)
    {
        viewState = GameObject.FindWithTag("MainCamera").GetComponent<CamManager>().GetViewState();
    }

    IEnumerator RoopMakeRazer()
    {
        int randDir = Random.Range(0, 2);
        float randY;
        Vector3 razerPos;
        while(true)
        {
            if(viewState == CamManager.ViewState.Front)
            {
                randDir = Random.Range(0, 2);
                switch (randDir)
                {
                    case 0:
                        razerPos = new Vector3(300, 0, player.transform.position.z);
                        GameObject razer = Instantiate(Yrazer, razerPos, Quaternion.identity);
                        razer.transform.parent = transform;
                        break;
                    case 1:
                        razerPos = new Vector3(300, player.transform.position.y, 0);
                        razer = Instantiate(Zrazer, razerPos, Quaternion.identity);
                        razer.transform.parent = transform;
                        break;
                }
            }

            else if(viewState == CamManager.ViewState.Side)
            {
                randDir = Random.Range(0, 4);
                randY = Random.Range(-19.0f, 19.0f);
                switch(randDir)
                {
                    case 0:
                        razerPos = new Vector3(300, 0, player.transform.position.z);
                        GameObject razer = Instantiate(Yrazer, razerPos, Quaternion.identity);
                        razer.transform.parent = transform;
                        break;
                    default:
                        razerPos = new Vector3(300, randY, 0);
                        razer = Instantiate(Zrazer, razerPos, Quaternion.identity);
                        razer.transform.parent = transform;
                        break;
                }
            }

            else if(viewState == CamManager.ViewState.Top)
            {
                razerPos = new Vector3(300, player.transform.position.y, 0);
                GameObject razer = Instantiate(Zrazer, razerPos, Quaternion.identity);
                razer.transform.parent = transform;
            }
            yield return new WaitForSeconds(genCoolTime);
        }
    }

    void OnGameOver(GameEvents gameEvents)
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    void OnBossStart(GameEvents gameEvents)
    {
        StopAllCoroutines();
    }

    void OnBossEnd(GameEvents gameEvents)
    {
        StartCoroutine(RoopMakeRazer());
    }
    void OnDestroy()
    {
        GameEvents.CameraStop -= OnCameraStop;
        GameEvents.GameOver -= OnGameOver;
        GameEvents.BossStart -= OnBossStart;
        GameEvents.BossEnd -= OnBossEnd;
    }
}
