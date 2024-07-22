using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    public static RankManager Instance;

    public float[] bestScore = new float[12];      // ���̽��ھ� ����(7�����)
    public string[] bestName = new string[12];     // ���̽��ھ� �̸�

    public InputField InputName;
    public InputField InputScore;

    public GameObject parentObject;
    public GameObject[] rankingInfos;
    public GameObject[] rankingChildren = new GameObject[3];

    private void Awake()
    {
        if (Instance == null)
            RankManager.Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        // Get Saved Rank
        ReadRankData();
    }

    void Update()
    {

    }

    // ��ŷ �ҷ�����
    public void ReadRankData()
    {
        for (int i = 0; i < 12; i++)
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
    }

    // ��ŷ �����ϱ�
    public void WriteRankData()
    {
        for (int i = 0; i < 12; i++)
        {
            PlayerPrefs.SetFloat(i + "BestScore", bestScore[i]);
            PlayerPrefs.SetString(i + "BestName", bestName[i]);
        }

        //UpdateRankUI();
    }

    public void UpdateRankUI()
    {
        //fetch the info from bestScore and bestName arrays
        //access the children of GameObject -> access rank, name and score to update the ranking info

        rankingInfos = new GameObject[parentObject.transform.childCount];

        for(int i=0; i<12; i++)
        {
            rankingInfos[i] = parentObject.transform.GetChild(i).gameObject;
            for(int j=0; j<=2; j++)
            {
                rankingChildren[j] = rankingInfos[i].transform.GetChild(j).gameObject;

            }

            TextMeshProUGUI rankText = rankingChildren[0].GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI nameText = rankingChildren[1].GetComponent<TextMeshProUGUI>(); 
            TextMeshProUGUI scoreText = rankingChildren[2].GetComponent<TextMeshProUGUI>();
            
            rankText.text = (i + 1).ToString();
            nameText.text = bestName[i].ToString();
            scoreText.text = string.Format("{0:D6}", (int)bestScore[i]); 
        }

        
    }

    // �÷��̾��� ������ ��ŷ �� �� ��ŷ ����
    public void CompareRankScore(string name, float score)
    {
        float tmpScore;
        string tmpName;

        bestScore[11] = score;
        bestName[11] = name;

        for (int i = 11; i > 0; i--)
        {
            if (bestScore[i] > bestScore[i - 1])
            {
                tmpName = bestName[i - 1];
                tmpScore = bestScore[i - 1];

                bestName[i - 1] = bestName[i];
                bestScore[i - 1] = bestScore[i];

                bestName[i] = tmpName;
                bestScore[i] = tmpScore;
            }
        }
    }

    public void ResetData()
    {
        Debug.Log("Reset Data");
        for (int i = 0; i < 12; i++)
        {
            PlayerPrefs.SetFloat(i + "BestScore", 0);
            PlayerPrefs.SetString(i + "BestName", "NAN");
        }
    }

    public void test()
    {
        ReadRankData();
        CompareRankScore(InputName.text,float.Parse(InputScore.text));
        WriteRankData();
        UpdateRankUI();
    }
}
