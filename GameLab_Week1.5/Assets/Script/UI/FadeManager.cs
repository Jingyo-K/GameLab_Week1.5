using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeTime = 1.0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            ChangeScene("Q");
    }
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }


    private IEnumerator FadeOut(string sceneName)
    {
        Color curColor = _fadeImage.color;
        Color targetColor = new Color(0, 0, 0, 1);
        float time = 0;

        while (time < _fadeTime)
        {
            _fadeImage.color = Color.Lerp(curColor, targetColor, time / _fadeTime);
            time += Time.deltaTime;
            yield return null;
        }
        _fadeImage.color = targetColor;
        SceneManager.LoadScene(sceneName);
        StartCoroutine(FadeIn());
    }


    private IEnumerator FadeIn()
    {
        Color curColor = _fadeImage.color;
        Color targetColor = new Color(0, 0, 0, 0);
        float time = 0;

        while (time < _fadeTime)
        {
            _fadeImage.color = Color.Lerp(curColor, targetColor, time / _fadeTime);
            time += Time.deltaTime;
            yield return null;
        }
        _fadeImage.color = targetColor;
        yield break;
    }

}
