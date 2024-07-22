using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    private bool coolTime = false;
    public float genCoolTime = 1.0f;
    private bool _epicCoolTime = false;
    public float epicCoolTime = 3.0f;
    void Start()
    {
        GameEvents.GameOver += OnGameOver;
    }

    // Update is called once per frame
    void Update()
    {
        if(!coolTime)
        {
            StartCoroutine(GenerateEnemy());
        }

        if(!_epicCoolTime)
        {
            StartCoroutine(GenerateEpicEnemy());
        }

    }

    IEnumerator GenerateEnemy()
    {
        float randZ = Random.Range(-19.0f, 19.0f);
        float randY = Random.Range(-19.0f, 19.0f);
        coolTime = true;
        Vector3 enemyPos = new Vector3(transform.localPosition.x, randY, randZ);
        GameObject enemy = Instantiate(enemyPrefab[0], enemyPos, Quaternion.identity);
        enemy.GetComponent<EnemyMoving>().SetRealPosition(enemyPos);
        enemy.transform.parent = transform;
        yield return new WaitForSeconds(genCoolTime);
        coolTime = false;
    }

    IEnumerator GenerateEpicEnemy()
    {
        _epicCoolTime = true;
        yield return new WaitForSeconds(epicCoolTime);
        float randZ = Random.Range(-19.0f, 19.0f);
        float randY = Random.Range(-19.0f, 19.0f);
        Vector3 enemyPos = new Vector3(transform.localPosition.x, randY, randZ);
        GameObject enemy = Instantiate(enemyPrefab[1], enemyPos, Quaternion.identity);
        enemy.GetComponent<EnemyMoving>().SetRealPosition(enemyPos);
        enemy.transform.parent = transform;
        _epicCoolTime = false;
    }

    void OnGameOver(GameEvents gameEvents)
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
    void OnDestroy()
    {
        GameEvents.GameOver -= OnGameOver;
    }
}
