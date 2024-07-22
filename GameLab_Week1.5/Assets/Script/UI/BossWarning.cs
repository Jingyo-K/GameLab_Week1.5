using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossWarning : MonoBehaviour
{
    public static FadeManager Instance;
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeTime = 1.0f;
    Color Red = new Color(1, 0, 0, 0.5f);
    Color transparent = new Color(1, 0, 0, 0);
    void Start()
    {
        GameEvents.BossStart += OnBossStart;
    }

    // Update is called once per frame
    void OnBossStart(GameEvents gameEvents)
    {
        StartCoroutine(FadeIn());
    }
    private IEnumerator FadeIn()
    {
        Color curColor = _fadeImage.color;
        float time = 0;

        while (time < _fadeTime)
        {
            _fadeImage.color = Color.Lerp(transparent, Red, time / _fadeTime);
            time += Time.deltaTime;
            yield return null;
        }
        time = 0;
        while (time < _fadeTime)
        {
            _fadeImage.color = Color.Lerp(Red, transparent, time / _fadeTime);
            time += Time.deltaTime;
            yield return null;
        }
        time = 0;
        while (time < _fadeTime)
        {
            _fadeImage.color = Color.Lerp(transparent, Red, time / _fadeTime);
            time += Time.deltaTime;
            yield return null;
        }
        time = 0;
        while (time < _fadeTime)
        {
            _fadeImage.color = Color.Lerp(Red, transparent, time / _fadeTime);
            time += Time.deltaTime;
            yield return null;
        }
        _fadeImage.color = transparent;
        yield break;
    }
    void OnDestroy()
    {
        GameEvents.BossStart -= OnBossStart;
    }
}
