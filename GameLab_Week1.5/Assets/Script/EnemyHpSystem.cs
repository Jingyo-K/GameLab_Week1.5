using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpSystem : MonoBehaviour
{
    public float hp = 10.0f;

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
