using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpSystem : MonoBehaviour
{
    public static float commonMaxHP = 3.0f;
    public static float epicMaxHP = 7.0f;
    public static float BossMaxHP = 1000.0f;
    public float curHP;
    public GameObject[] itemPrefab;
    private bool isDead = false;
    private bool isBoss = false;
    public Slider hpBar;
    GameEvents gameManager;

    void Start()
    {
        if(gameObject.tag == "EpicEnemy")
        {
            curHP = epicMaxHP;
            hpBar.maxValue = epicMaxHP;
        }
        else if(gameObject.tag == "Enemy")
        {
            curHP = commonMaxHP;
            hpBar.maxValue = commonMaxHP;
        }
        else if(gameObject.tag == "Boss")
        {
            curHP = BossMaxHP;
            hpBar.maxValue = BossMaxHP;
        }
        hpBar.value = curHP;
        gameManager = GameManager.instance.gameEvents;
        GameEvents.GameOver += OnGameOver;
        GameEvents.BossEnd += OnBossEnd;
    }
    void FixedUpdate()
    {
        if(gameObject.tag != "Boss")
            hpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, -2.0f, 0));
    }
    public void TakeDamage(int damage)
    {
        curHP -= damage;
        hpBar.value = curHP;
        if(gameObject.tag == "Boss")
        {
            Score.score += damage*10;
        }
        if(curHP <= 0 && !isDead)
        {
            isDead = true;
            {
                if(gameObject.tag == "EpicEnemy")
                {
                    int rand = UnityEngine.Random.Range(0, itemPrefab.Length);
                    GameObject item = Instantiate(itemPrefab[rand], transform.position, Quaternion.identity);
                    item.GetComponent<ItemMove>().SetRealPosition(transform.position);
                    Destroy(gameObject);
                }
                else if(gameObject.tag == "Enemy")
                {
                    GameObject item = Instantiate(itemPrefab[0], transform.position, Quaternion.identity);
                    item.GetComponent<ItemMove>().SetRealPosition(transform.position);
                    Destroy(gameObject);
                }
                else if(gameObject.tag == "Boss")
                {
                    if(!isBoss)
                    {
                        isBoss = true;
                        StartCoroutine(BossEnd());
                    }
                }

            }
        }
    }
    IEnumerator BossEnd()
    {
        int count = 0;
        while(count < 100)
        {
            float randY = UnityEngine.Random.Range(-19.0f, 19.0f);
            float randZ = UnityEngine.Random.Range(-19.0f, 19.0f);
            count++;
            yield return new WaitForSeconds(0.05f);
            Vector3 pos = new Vector3(transform.position.x, randY, randZ);
            GameObject coin = Instantiate(itemPrefab[0], pos, Quaternion.identity);
            coin.GetComponent<ItemMove>().SetRealPosition(pos);
        }
        gameManager.CallBossEnd();
        Destroy(gameObject);
    }
    void OnGameOver(GameEvents gameEvents)
    {
        commonMaxHP = 3.0f;
        epicMaxHP = 7.0f;
        BossMaxHP = 10000.0f;
    }
    void OnBossEnd(GameEvents gameEvents)
    {
        commonMaxHP = 3 * Mathf.Pow(2, PatternMaker.stageLevel) + PatternMaker.patternCount * 0.1f;
        epicMaxHP = 7 * Mathf.Pow(2, PatternMaker.stageLevel) + PatternMaker.patternCount * 1.5f;
        BossMaxHP = 1000 * Mathf.Pow(2, PatternMaker.stageLevel);
    }
    void OnDestroy()
    {
        GameEvents.GameOver -= OnGameOver;
        GameEvents.BossEnd -= OnBossEnd;
    }
}
