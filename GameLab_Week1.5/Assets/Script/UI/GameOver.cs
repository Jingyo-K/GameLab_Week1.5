using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerScore;
    [SerializeField] private InputField _nameInput;
    [SerializeField] private GameObject _saveButton;
    // Start is called before the first frame update
    void OnEnable()
    {
        RankManager.Instance.UpdateRankUI();
    }

    public void SavePlayerRank()
    {
        RankManager.Instance.CompareRankScore(_nameInput.text, Score.score);
        RankManager.Instance.WriteRankData();
        RankManager.Instance.UpdateRankUI();
        _saveButton.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void ResetRankData()
    {
        StartCoroutine(Reset());
    }
    public void SceneRestart()
    {
        FadeManager.Instance.ChangeScene("TitleScene");
    }
    IEnumerator Reset()
    {
        RankManager.Instance.ResetData();
        yield return new WaitForSeconds(0.5f);
        RankManager.Instance.UpdateRankUI();
    }
}
