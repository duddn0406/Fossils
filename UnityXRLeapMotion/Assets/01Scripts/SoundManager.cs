using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("🔊 Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("🎵 Audio Clips")]
    public AudioClip[] bgmClips;
    public AudioClip[] sfxClips;

    private Dictionary<string, AudioClip> bgmDict = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 넘어가도 유지
            InitDictionaries();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitDictionaries()
    {
        foreach (var clip in bgmClips)
        {
            if (clip != null && !bgmDict.ContainsKey(clip.name))
                bgmDict[clip.name] = clip;
        }

        foreach (var clip in sfxClips)
        {
            if (clip != null && !sfxDict.ContainsKey(clip.name))
                sfxDict[clip.name] = clip;
        }
    }

    public void PlayBGM(string name)
    {
        if (bgmDict.TryGetValue(name, out var clip))
        {
            if (bgmSource.clip != clip)
            {
                bgmSource.clip = clip;
                bgmSource.loop = true;
                bgmSource.Play();
            }
        }
        else
        {
            Debug.LogWarning($"🎵 BGM '{name}' not found.");
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySFX(string name)
    {
        if (sfxDict.TryGetValue(name, out var clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"🔊 SFX '{name}' not found.");
        }
    }
}