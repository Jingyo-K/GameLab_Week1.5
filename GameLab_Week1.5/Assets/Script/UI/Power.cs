using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI text;
    BulletGen bulletGen;
    void Start()
    {
        GameEvents.ItemCollected += OnItemCollected;
        text = transform.GetComponent<TextMeshProUGUI>();
        bulletGen = GameObject.FindWithTag("Player").GetComponent<BulletGen>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnItemCollected(GameEvents gameEvents)
    {
        int power = bulletGen.GetBulletPower();
        text.text = $"Power       {power}" ;
    }

    void OnDestroy()
    {
        GameEvents.ItemCollected -= OnItemCollected;
    }
}
