using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMoving : MonoBehaviour
{
    // Start is called before the first frame update
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
