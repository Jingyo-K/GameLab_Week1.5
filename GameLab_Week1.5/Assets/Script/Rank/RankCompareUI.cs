using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankCompareUI : MonoBehaviour
{
    /*[SerializeField] private float[] bestScore = new float[7];
    [SerializeField] private string[] bestName = new string[7];*/

    [SerializeField] private int nextRank = 7;

    [SerializeField] private TextMeshProUGUI _nextRankNameText;
    [SerializeField] private TextMeshProUGUI _nextRankScoreText;
    [SerializeField] private TextMeshProUGUI _nextRankText;

    /*void Start()
    {
        // Get Saved Rank
        for (int i = 0; i < 7; i++)
        {
            if (PlayerPrefs.HasKey(i + "BestName"))
            {
                bestScore[i] = PlayerPrefs.GetFloat(i + "BestScore");
                bestName[i] = PlayerPrefs.GetString(i + "BestName");
            }
            else
            {
                bestScore[i] = 0;
                bestName[i] = "NAN";
            }
        }
    }*/

    void Update()
    {
        GetNextRank();
        DrawNextRank();
    }

    private void GetNextRank()
    {
        for (int i = 11; i >= 0; i--)
        {
            if (Score.score < RankManager.Instance.bestScore[i])
                return;

            nextRank = i;
        }
    }

    private void DrawNextRank()
    {
        int rank = Math.Clamp(nextRank - 1, 0, RankManager.Instance.bestName.Length - 1);
        if (nextRank == 0)
        {
            _nextRankNameText.text = "";
            _nextRankScoreText.text = "";
            _nextRankText.text = "You Are 1st!";
        }
        else
        {
            _nextRankNameText.text = RankManager.Instance.bestName[rank];
            _nextRankScoreText.text = string.Format("{0:D6}", (int)RankManager.Instance.bestScore[rank]);
        }
    }
}
