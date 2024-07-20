using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    private bool coolTime = false;
    public float genCoolTime = 1.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!coolTime)
        {
            StartCoroutine(GenerateEnemy());
        }
    }

    IEnumerator GenerateEnemy()
    {
        float randZ = Random.Range(-19.0f, 19.0f);
        float randY = Random.Range(-19.0f, 19.0f);
        coolTime = true;
        Vector3 enemyPos = new Vector3(transform.localPosition.x, randY, randZ);
        GameObject enemy = Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
        enemy.GetComponent<EnemyMoving>().SetRealPosition(enemyPos);
        enemy.transform.parent = transform;
        yield return new WaitForSeconds(genCoolTime);
        coolTime = false;
    }
}
