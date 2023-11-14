using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class AggroArea : MonoBehaviour
{
    /* ------------- 컴포넌트 변수 ------------- */
    private IAggroCheck aggroCheck;

    /* -------------- 이벤트 함수 -------------- */
    private void Awake()
    {
        aggroCheck = GetComponentInParent<IAggroCheck>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            aggroCheck.InAggro();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            aggroCheck.PlayerDir = transform.position.x - collision.transform.position.x > 0 ? -1 : 1; // -1 왼쪽, 1 오른쪽
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            aggroCheck.OutAggro();
    }
}
