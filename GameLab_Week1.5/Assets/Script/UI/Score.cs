using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System; // Add this line

public class Score : MonoBehaviour
{
    [SerializeField] public static int score = 0;
    [SerializeField] private int curscore = 0;
    private TextMeshProUGUI text;

    void Start()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        DrawScore();
        GameManager.instance.gameEvents.CallOnItemCollected();
    }

    void Update()
    {
        if (curscore != score)
        {
           curscore = score;
           DrawScore();
        }
    }

    void DrawScore()
    {
        text.text = $"Score    :    {curscore:D6}";
    }


}
