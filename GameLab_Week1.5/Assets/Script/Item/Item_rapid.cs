using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_rapid : ItemMove
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            int rapidLv = other.GetComponent<BulletGen>().GetBulletRapid();
            if(rapidLv < 10)
            {
                other.GetComponent<BulletGen>().bulletRapidUp(0.9f);
            }
            else
            {
                score = 50;
            }
            Destroy(gameObject);
        }
    }
}
