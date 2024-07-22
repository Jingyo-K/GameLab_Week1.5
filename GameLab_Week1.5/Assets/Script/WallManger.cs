using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject transParent;
    [SerializeField] private float wallSize = 1.0f;
    [SerializeField] private  GameObject mainCamera;
    [SerializeField] private Camera camSys;
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        camSys = mainCamera.GetComponent<Camera>();
        mainCamera.transform.position = new Vector3(0, 0, -wallSize*16f);
        camSys.orthographicSize = wallSize*10;
        MakeWall();
        MakeTransparent();
        StartCoroutine(RoopMakeWall());
        GameEvents.GameOver += OnGameOver;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeWall()
    {
        Vector3 wallPos = new Vector3(wallSize*10f, 0, 0);

        /*GameObject PxWall = Instantiate(transParent, wallPos, Quaternion.Euler(0,0,90));
        PxWall.transform.localScale = new Vector3(wallSize, 1, wallSize);
        PxWall.transform.parent = transform;
        PxWall.tag = "Wall";*/

        /*wallPos = new Vector3(-wallSize*5, 0, 0);
        GameObject NxWall = Instantiate(wallPrefab, wallPos, Quaternion.Euler(0,0,-90));
        NxWall.transform.localScale = new Vector3(wallSize, 1, wallSize);
        NxWall.transform.parent = transform;
        */

        wallPos = new Vector3(0, 0, wallSize*5);
        GameObject PzWall = Instantiate(wallPrefab, wallPos, Quaternion.Euler(-90,0,0));
        PzWall.transform.localScale = new Vector3(wallSize*10, 1, wallSize);
        PzWall.transform.parent = transform;

        wallPos = new Vector3(0, 0, -wallSize*5);
        GameObject NzWall = Instantiate(wallPrefab, wallPos, Quaternion.Euler(90,0,0));
        NzWall.transform.localScale = new Vector3(wallSize*10, 1, wallSize);
        NzWall.transform.parent = transform;

        wallPos = new Vector3(0, wallSize*5, 0);
        GameObject PyWall = Instantiate(wallPrefab, wallPos, Quaternion.Euler(180,0,0));
        PyWall.transform.localScale = new Vector3(wallSize*10, 1, wallSize);
        PyWall.transform.parent = transform;

        wallPos = new Vector3(0, -wallSize*5, 0);
        GameObject NyWall = Instantiate(wallPrefab, wallPos, Quaternion.Euler(0,0,0));
        NyWall.transform.localScale = new Vector3(wallSize*10, 1, wallSize);
        NyWall.transform.parent = transform;

    }

    void MakeTransparent()
    {
        Vector3 wallPos; 

        wallPos = new Vector3(- wallSize*2, 0, 0);
        GameObject PxWall2 = Instantiate(transParent, wallPos, Quaternion.Euler(0,0,90));
        PxWall2.transform.localScale = new Vector3(wallSize, 1, wallSize);
        PxWall2.transform.parent = transform;

        wallPos = new Vector3(-wallSize*4.5f - wallSize*2, 0, 0);
        GameObject NxWall2 = Instantiate(transParent, wallPos, Quaternion.Euler(0,0,-90));
        NxWall2.transform.localScale = new Vector3(wallSize, 1, wallSize);
        NxWall2.transform.parent = transform;

        wallPos = new Vector3(-wallSize*2, 0, wallSize*4.5f);
        GameObject PzWall2 = Instantiate(transParent, wallPos, Quaternion.Euler(-90,0,0));
        PzWall2.transform.localScale = new Vector3(wallSize*10, 1, wallSize);
        PzWall2.transform.parent = transform;

        wallPos = new Vector3(-wallSize*2, 0, -wallSize*4.5f);
        GameObject NzWall2 = Instantiate(transParent, wallPos, Quaternion.Euler(90,0,0));
        NzWall2.transform.localScale = new Vector3(wallSize*10, 1, wallSize);
        NzWall2.transform.parent = transform;

        wallPos = new Vector3(-wallSize*2, wallSize*4.5f, 0);
        GameObject PyWall2 = Instantiate(transParent, wallPos, Quaternion.Euler(180,0,0));
        PyWall2.transform.localScale = new Vector3(wallSize*10, 1, wallSize);
        PyWall2.transform.parent = transform;

        wallPos = new Vector3(-wallSize*2, -wallSize*4.5f, 0);
        GameObject NyWall2 = Instantiate(transParent, wallPos, Quaternion.Euler(0,0,0));
        NyWall2.transform.localScale = new Vector3(wallSize*10, 1, wallSize);
        NyWall2.transform.parent = transform;
    }

    IEnumerator RoopMakeWall()
    {
        Vector3 wallPos;
        while(true)
        {
        wallPos = new Vector3(wallSize*100, 0, wallSize*5);
        GameObject PzWall = Instantiate(wallPrefab, wallPos, Quaternion.Euler(-90,0,0));
        PzWall.transform.localScale = new Vector3(wallSize*10, 1, wallSize);
        PzWall.transform.parent = transform;

        wallPos = new Vector3(wallSize*100, 0, -wallSize*5);
        GameObject NzWall = Instantiate(wallPrefab, wallPos, Quaternion.Euler(90,0,0));
        NzWall.transform.localScale = new Vector3(wallSize*10, 1, wallSize);
        NzWall.transform.parent = transform;

        wallPos = new Vector3(wallSize*100, wallSize*5, 0);
        GameObject PyWall = Instantiate(wallPrefab, wallPos, Quaternion.Euler(180,0,0));
        PyWall.transform.localScale = new Vector3(wallSize*10, 1, wallSize);
        PyWall.transform.parent = transform;

        wallPos = new Vector3(wallSize*100, -wallSize*5, 0);
        GameObject NyWall = Instantiate(wallPrefab, wallPos, Quaternion.Euler(0,0,0));
        NyWall.transform.localScale = new Vector3(wallSize*10, 1, wallSize);
        NyWall.transform.parent = transform;
        yield return new WaitForSeconds(wallSize*1.999f);
        }
    }

    void OnGameOver(GameEvents gameEvents)
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
    void OnDestroy()
    {
        GameEvents.GameOver -= OnGameOver;
    }
}
