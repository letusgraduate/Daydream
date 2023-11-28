using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTriggerTest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GetComponent<SoundController>().PlaySound(0); // 소리 재생 함수 실행
    }
}