using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Power : ItemMove
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<BulletGen>().bulletPowerUp(1);
            Destroy(gameObject);
        }
    }
}
