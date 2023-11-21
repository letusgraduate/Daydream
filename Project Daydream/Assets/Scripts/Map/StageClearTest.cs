using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearTest : MonoBehaviour
{
    /* ---------------- 인스펙터 --------------- */
    [Header("포탈 스포너")]
    [SerializeField]
    private GameObject portalSpawner;

    /* -------------- 이벤트 함수 -------------- */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.StageClear(); // 스테이지 클리어
            portalSpawner.SetActive(true);
        }
    }
}