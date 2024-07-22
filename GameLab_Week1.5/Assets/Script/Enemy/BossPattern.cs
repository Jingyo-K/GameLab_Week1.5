using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject[] Patterns;
    public float genCoolTime = 5.0f;

    void Start()
    {
        StartCoroutine(Pattern());
    }


    IEnumerator Pattern()
    {
        while (true)
        {
            yield return new WaitForSeconds(genCoolTime);
            int rand = Random.Range(0, Patterns.Length);
            GameObject pattern = Instantiate(Patterns[rand], transform.position, Quaternion.identity);
            pattern.transform.parent = transform;
        }
    }

}
