using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    /* ---------------- 인스펙터 --------------- */
    [Header("Audio Source")]
    [SerializeField]
    private AudioSource[] sfxSources;

    /* ---------------- 실행함수 --------------- */
    public void PlaySound(int index)
    {
        // 오디오 파일 재생
        if (sfxSources[index] != null)
            sfxSources[index].Play();
        else
            Debug.Log("해당 오디오 소스가 없습니다!");
    }
}