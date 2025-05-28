using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class FadeManager : MonoBehaviour
{ 
    [Header("UI 이미지 (검은 배경)")]
    public Image fadeImage;

    [Header("페이드 시간")]
    public float fadeDuration = 1f;

    public bool acting;

    public void FadeIn()
    {
        if(!acting)
        {
            acting = true;
            StartCoroutine(Fade(1f, 0f)); // 검정 → 투명
        }
    }

    public void FadeOut()
    {
        if (!acting)
        {
            acting = true;
            StartCoroutine(Fade(0f, 1f)); // 투명 → 검정
        }
    }

    public void FadeOutAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndChangeScene(sceneName));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;
        color.a = startAlpha;
        fadeImage.color = color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            fadeImage.color = color;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;
    }

    private IEnumerator FadeOutAndChangeScene(string sceneName)
    {
        yield return Fade(0f, 1f); // 화면 어두워짐
        SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(0.1f); // 씬 로딩 대기
        FadeIn(); // 자동 페이드 인
    }
}
