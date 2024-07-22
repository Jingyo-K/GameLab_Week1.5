using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rankingText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void SetRanking(int rank, string name, int score)
    {
        _rankingText.text = rank.ToString();
        _nameText.text = name.ToString();
        _scoreText.text = score.ToString();
    }
}
