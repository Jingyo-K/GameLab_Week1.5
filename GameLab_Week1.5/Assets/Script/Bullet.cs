using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletPower = 10.0f;
    private float bulletSpeed = 200.0f;

    void Update()
    {
        transform.position += transform.right * bulletSpeed * Time.deltaTime;

        if(transform.position.x > 80)
        {
            Destroy(gameObject);
        }
    }
    public void SetBullet(float power)
    {
        bulletPower = power;
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if(other.tag == "Enemy")
        {
            EnemyHpSystem enemyHpSystem = other.GetComponent<EnemyHpSystem>();
            enemyHpSystem.TakeDamage(bulletPower);
            Destroy(gameObject);
        }
    }
}
