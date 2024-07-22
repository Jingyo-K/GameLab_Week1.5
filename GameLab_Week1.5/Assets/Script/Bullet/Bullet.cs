using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int bulletPower = 1;
    private float bulletSpeed = 200.0f;

    void Update()
    {
        transform.position += transform.right * bulletSpeed * Time.deltaTime;

        if(transform.position.x > 60)
        {
            Destroy(gameObject);
        }
    }
    public void SetBullet(int power)
    {
        bulletPower = power;
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if(other.tag == "Enemy" || other.tag == "EpicEnemy" || other.tag == "Boss")
        {
            if(other.tag == "Boss")
            {
                EnemyHpSystem enemyHpSystem = other.transform.parent.GetComponent<EnemyHpSystem>();
                enemyHpSystem.TakeDamage(bulletPower);
            }
            else{
                EnemyHpSystem enemyHpSystem = other.GetComponent<EnemyHpSystem>();
                enemyHpSystem.TakeDamage(bulletPower);
            }
            
            Destroy(gameObject);
        }
    }
}
