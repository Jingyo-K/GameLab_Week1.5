using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternMaker : MonoBehaviour
{
    public GameObject[] patternPrefab;
    public GameObject[] epicPatternPrefab;
    public GameObject Boss;
    public static int stageLevel = 1;
    public static int patternCount = 0;
    private bool coolTime = false;

    public float genCoolTime = 5.0f;

    GameObject player;
    GameEvents gameEvents;
    void Start()
    {
        gameEvents = GameManager.instance.gameEvents;
        GameEvents.GameOver += OnGameOver;
        GameEvents.BossEnd += OnBossEnd;
        
        player = GameObject.FindGameObjectWithTag("Player");
        stageLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!coolTime && patternCount < 10 && patternCount%2 == 0)
        {
            StartCoroutine(GenerateEpicPattern());
        }
        else if(!coolTime && patternCount < 10 && patternCount%2 == 1)
        {
            StartCoroutine(GeneratePattern());
        }
        if(patternCount == 10)
        {
            gameEvents = GameManager.instance.gameEvents;
            patternCount = 11;
            gameEvents.CallBossStart();
            StartCoroutine(BossGen());
        }
        if(transform.childCount > 1)
        {
            if(transform.GetChild(0).childCount == 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
        }

    }
    IEnumerator GeneratePattern()
    {
        int rand = Random.Range(0, patternPrefab.Length);
        float randY = Random.Range(-10.0f, 10.0f);
        coolTime = true;
        Vector3 patternPos = new Vector3(transform.localPosition.x, randY, player.transform.position.z);
        switch(rand)
        {
            case 0:
            case 1:
                patternPos = new Vector3(transform.localPosition.x, randY, player.transform.position.z);
                break;
            case 2:
                patternPos = new Vector3(transform.localPosition.x, transform.position.y, randY);
                break;
        }
        GameObject pattern = Instantiate(patternPrefab[rand], patternPos, Quaternion.identity);
        pattern.transform.parent = transform;
        yield return new WaitForSeconds(genCoolTime);
        patternCount++;
        coolTime = false;
    }

    IEnumerator GenerateEpicPattern()
    {
        int rand = Random.Range(0, epicPatternPrefab.Length);
        float randY = Random.Range(-10.0f, 10.0f);
        coolTime = true;
        Vector3 patternPos = new Vector3(transform.localPosition.x, randY, player.transform.position.z);
        switch(rand)
        {
            case 0:
            case 1:
                patternPos = new Vector3(transform.localPosition.x, randY, player.transform.position.z);
                break;
            case 2:
                patternPos = new Vector3(transform.localPosition.x, transform.position.y, randY);
                break;
        }
        GameObject pattern = Instantiate(epicPatternPrefab[rand], patternPos, Quaternion.identity);
        pattern.transform.parent = transform;
        yield return new WaitForSeconds(genCoolTime);
        patternCount++;
        coolTime = false;
    }
    IEnumerator BossGen()
    {
        yield return new WaitForSeconds(5.0f);
        GameObject boss = Instantiate(Boss, transform.position, Quaternion.identity);
        boss.transform.parent = transform;
    }

    void OnGameOver(GameEvents gameEvents)
    {
        StopAllCoroutines();
        stageLevel = 1;
        Time.timeScale = 1.0f;
    }

    void OnBossEnd(GameEvents gameEvents)
    {
        stageLevel++;
        patternCount = 0;
        Time.timeScale *=1.2f;
    }


    void OnDestroy()
    {
        GameEvents.GameOver -= OnGameOver;
        GameEvents.BossEnd -= OnBossEnd;
    }
}
