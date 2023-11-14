using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeArea : MonoBehaviour
{
    /* ------------- 컴포넌트 변수 ------------- */
    private IAttackRangeCheck rangeCheck;

    /* -------------- 이벤트 함수 -------------- */
    private void Awake()
    {
        rangeCheck = GetComponentInParent<IAttackRangeCheck>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            rangeCheck.InAttackRange();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            rangeCheck.OutAttackRange();
    }
}
