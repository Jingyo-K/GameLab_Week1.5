using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_ammo : ItemMove
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            int count = other.GetComponent<BulletGen>().GetBulletCount();
            if(count < 10)
            {
                other.GetComponent<BulletGen>().bulletCountUp(1);
            }
            else
            {
                score = 500;
            }
            Destroy(gameObject);
        }
    }
}

