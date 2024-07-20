using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletGen : MonoBehaviour
{
    public GameObject bulletPrefab;
    private float bulletRapid = 0.3f;
    private float bulletPower = 1.0f;
    public int bulletCount = 1;

    public float spreadAngle = 15.0f;
    private CamManager camManager;
    private CamManager.ViewState viewState;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateBullet());
        camManager = GameObject.FindWithTag("MainCamera").GetComponent<CamManager>();
        viewState = camManager.GetViewState();
        GameEvents.CameraStop += OnCameraStop;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            bulletRapidUp(0.9f);
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            bulletCountUp(1);
        }
    }

    IEnumerator GenerateBullet()
    {
        while(true)
        {
            if(bulletCount == 1)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.GetChild(3).position, transform.rotation);
                bullet.GetComponent<Bullet>().SetBullet(bulletPower);
            }
            else if (bulletCount == 2)
            {
                Vector3 bulletPos = new Vector3(transform.GetChild(3).position.x, transform.GetChild(3).position.y+0.5f, transform.GetChild(3).position.z);
                GameObject bullet = Instantiate(bulletPrefab, bulletPos, transform.rotation);
                bullet.GetComponent<Bullet>().SetBullet(bulletPower);
                bulletPos = new Vector3(transform.GetChild(3).position.x, transform.GetChild(3).position.y-0.5f, transform.GetChild(3).position.z);
                GameObject bullet2 = Instantiate(bulletPrefab, bulletPos, transform.rotation);
                bullet2.GetComponent<Bullet>().SetBullet(bulletPower);
            }
            else{
            float angleStep = spreadAngle / (bulletCount - 1);
            float angle = -spreadAngle / 2; // 부채꼴의 시작 각도

                for (int i = 0; i < bulletCount; i++)
                {
                    Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                    if(viewState == CamManager.ViewState.Side)
                    {
                        rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                    }
                    else if(viewState == CamManager.ViewState.Top)
                    {
                        rotation = Quaternion.Euler(new Vector3(0, angle, 0));
                    }
                    GameObject bullet = Instantiate(bulletPrefab, transform.GetChild(3).position, rotation);
                    bullet.GetComponent<Bullet>().SetBullet(bulletPower);
                    bullet.GetComponent<Bullet>().SetBullet(bulletPower);

                    angle += angleStep;
                }
            }
            
            yield return new WaitForSeconds(bulletRapid);
        }
    }
    
    public void bulletPowerUp(float power)
    {
        bulletPower += power;
    }
    public void bulletRapidUp(float rapid)
    {
        bulletRapid *= rapid;
    }
    public void bulletCountUp(int count)
    {
        bulletCount += count;
        spreadAngle += 2.5f;
        if (spreadAngle > 30.0f)
        {
            spreadAngle = 30.0f;
        }
    }

    public void OnCameraStop(object sender)
    {
        viewState = camManager.GetViewState();
    }
}
