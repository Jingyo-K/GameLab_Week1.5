using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    TextMeshProUGUI text;
    BulletGen bulletGen;
    void Start()
    {
        GameEvents.ItemCollected += OnItemCollected;
        text = transform.GetComponent<TextMeshProUGUI>();
        bulletGen = GameObject.FindWithTag("Player").GetComponent<BulletGen>();
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);
        transform.GetChild(5).gameObject.SetActive(false);
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(7).gameObject.SetActive(false);
        transform.GetChild(8).gameObject.SetActive(false);
        transform.GetChild(9).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnItemCollected(GameEvents gameEvents)
    {
        int power = bulletGen.GetBulletCount();
        switch (power)
        {
            case 1:
                transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 2:
                transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 3:
                transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 4:
                transform.GetChild(3).gameObject.SetActive(true);
                break;
            case 5:
                transform.GetChild(4).gameObject.SetActive(true);
                break;
            case 6:
                transform.GetChild(5).gameObject.SetActive(true);
                break;
            case 7:
                transform.GetChild(6).gameObject.SetActive(true);
                break;
            case 8:
                transform.GetChild(7).gameObject.SetActive(true);
                break;
            case 9:
                transform.GetChild(8).gameObject.SetActive(true);
                break;
            case 10:
                transform.GetChild(9).gameObject.SetActive(true);
                break;
        }
    }

    void OnDestroy()
    {
        GameEvents.ItemCollected -= OnItemCollected;
    }
}
