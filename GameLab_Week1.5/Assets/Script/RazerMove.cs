using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazerMove : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(-speed, 0, 0);

        if(transform.position.x < -400)
        {
            Destroy(gameObject);
        }
    }
}
