using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    /* -------------- 싱글톤 변수 -------------- */
    public static SoundManager instance = null;

    /* ---------------- 인스펙터 --------------- */
    [Header("Audio Clip")]
    [SerializeField]
    private AudioSource[] sfxSources;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(string sourceName) // 효과음 재생
    {
        AudioSource targetSource = null; // 대상 효과음 소스

        for (int i = 0; i < sfxSources.Length; i++) // 원하는 효과음 소스 찾기
        {
            if (sfxSources[i].name == sourceName)
            {
                targetSource = sfxSources[i];
                break;
            }
        }

        if (targetSource != null) // 오디오 소스를 찾으면 실행
            targetSource.Play();
        else
            Debug.Log("해당 오디오 소스가 없습니다!");
    }
}