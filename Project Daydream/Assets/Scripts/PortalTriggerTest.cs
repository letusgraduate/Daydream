using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTriggerTest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GameManager.instance.StageClear(); // 스테이지 클리어
    }
}