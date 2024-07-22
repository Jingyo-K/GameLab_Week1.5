using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletGen : MonoBehaviour
{
    public GameObject bulletPrefab;
    static float bulletRapid = 0.3f;
    static int bulletPower = 1;
    static int bulletCount = 1;
    static int rapidLV = 1;
    static int powerLV = 1;
    static int countLV = 1;

    public float spreadAngle = 15.0f;
    private CamManager camManager;
    private CamManager.ViewState viewState;
    private Coroutine _runningCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        _runningCoroutine = StartCoroutine(GenerateBullet());
        camManager = GameObject.FindWithTag("MainCamera").GetComponent<CamManager>();
        viewState = camManager.GetViewState();
        GameEvents.CameraStop += OnCameraStop;
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator GenerateBullet()
    {
            while(true)
            {
                if(viewState == CamManager.ViewState.Side)
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
                }

                else
                {
                    if(bulletCount == 1)
                    {
                        Vector3 bulletPos = new Vector3(transform.GetChild(3).position.x, transform.GetChild(3).position.y-2f, transform.GetChild(3).position.z);
                        GameObject bullet = Instantiate(bulletPrefab, bulletPos, transform.rotation);
                        bullet.GetComponent<Bullet>().SetBullet(bulletPower);
                        bullet.transform.LookAt(transform.GetChild(4).position);
                        bullet.transform.Rotate(0, -90, 0);
                    }
                    else if (bulletCount >= 2)
                    {
                        Vector3 bulletPos = new Vector3(transform.GetChild(3).position.x, transform.GetChild(3).position.y-2f, transform.GetChild(3).position.z-2f);
                        GameObject bullet = Instantiate(bulletPrefab, bulletPos, transform.rotation);
                        bullet.GetComponent<Bullet>().SetBullet(bulletPower);
                        bullet.transform.LookAt(transform.GetChild(4).position);
                        bullet.transform.Rotate(0, -90, 0);
                        bulletPos = new Vector3(transform.GetChild(3).position.x, transform.GetChild(3).position.y-2f, transform.GetChild(3).position.z+2f);
                        GameObject bullet2 = Instantiate(bulletPrefab, bulletPos, transform.rotation);
                        bullet2.GetComponent<Bullet>().SetBullet(bulletPower);
                        bullet2.transform.LookAt(transform.GetChild(4).position);
                        bullet2.transform.Rotate(0, -90, 0);
                    }
                    else
                    {
                        Vector3 bulletPos = new Vector3(transform.GetChild(3).position.x, transform.GetChild(3).position.y-2f, transform.GetChild(3).position.z-4f);
                        Vector3 bulletPos2 = new Vector3(bulletPos.x, bulletPos.y, bulletPos.z);
                        GameObject bullet = Instantiate(bulletPrefab, bulletPos, transform.rotation);
                        bullet.GetComponent<Bullet>().SetBullet(bulletPower);
                        bullet.transform.LookAt(transform.GetChild(4).position);
                        bullet.transform.Rotate(0, -90, 0);
                        for (int i = 0; i < bulletCount-1; i++)
                        {
                            bulletPos2.z += 8.0f / (bulletCount - 1);
                            bullet = Instantiate(bulletPrefab, bulletPos2, transform.rotation);
                            bullet.GetComponent<Bullet>().SetBullet(bulletPower);
                            bullet.transform.LookAt(transform.GetChild(4).position);
                            bullet.transform.Rotate(0, -90, 0);
                        }
                    }

                }
                yield return new WaitForSeconds(bulletRapid);
            }
    }

    
    public void bulletPowerUp(int power)
    {
        bulletPower += power;
        powerLV++;
    }
    public void bulletRapidUp(float rapid)
    {
        bulletRapid *= rapid;
        rapidLV++;
    }
    public void bulletCountUp(int count)
    {
        bulletCount += count;
        countLV++;
        spreadAngle += 2.5f;
        if (spreadAngle > 30.0f)
        {
            spreadAngle = 30.0f;
        }
    }

    public int GetBulletCount()
    {
        return countLV;
    }

    public int GetBulletPower()
    {
        return powerLV;
    }

    public int GetBulletRapid()
    {
        return rapidLV;
    }
    public void OnCameraStop(object sender)
    {
        viewState = camManager.GetViewState();
    }
    public void stop()
    {
        StopCoroutine(_runningCoroutine);
    }
}

