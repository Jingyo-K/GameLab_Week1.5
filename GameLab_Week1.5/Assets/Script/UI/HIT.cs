using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HIT : MonoBehaviour
{
    public Image[] image;
    Color Red = new Color(1, 0, 0, 1);
    void Start()
    {
        GameEvents.PlayerHPChange += OnPlayerHPChange;
    }

    // Update is called once per frame
    void OnPlayerHPChange(GameEvents gameEvents)
    {
        int i;
        for(i=0; i<image.Length;i++)
        {
            image[i].GetComponent<Image>().color = Red;
            StartCoroutine(FadeIn(image[i]));
        }
    }


    private IEnumerator FadeIn(Image _fadeImage)
    {
        Color curColor = _fadeImage.color;
        float _fadeTime = 0.5f;
        Color targetColor = new Color(0, 0, 0, 1);
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

    void OnDestroy()
    {
        GameEvents.PlayerHPChange -= OnPlayerHPChange;
    }
}
