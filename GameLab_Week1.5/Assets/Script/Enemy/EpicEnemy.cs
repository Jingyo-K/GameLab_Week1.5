using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicEnemy : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * 100 * Time.deltaTime);
    }


}
