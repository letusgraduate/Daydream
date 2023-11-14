using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemyController : ChaseEnemyController, IAttackRangeCheck
{
    /* --------------- 공격 변수 --------------- */
    protected bool isAttackRange;
    protected Coroutine attackCoroutine;

    /* ---------------- 인스펙터 --------------- */
    [Header("공격")]
    [SerializeField]
    protected GameObject attackArea;

    [Space(10)]
    [SerializeField, Range(0f, 10f)]
    protected float AttackActiveTime = 0.45f;
    [SerializeField, Range(0f, 10f)]
    protected float AttackCoolTime = 3f;
    [SerializeField, Range(0, 100)]
    protected int Damage = 20;

    /* --------------- 인터페이스 -------------- */
    public void InAttackRange() // 공격 범위 내
    {
        isAttackRange = true;
        
        if (attackCoroutine == null) // 공격 중이 아닐 때
            attackCoroutine = StartCoroutine(Attack(AttackCoolTime));
    }

    public void OutAttackRange() // 공격 범위 밖
    {
        isAttackRange = false;
    }

    /* --------------- 공격 관련 --------------- */
    protected IEnumerator Attack(float time)
    {
        canMove = false;
        anim.SetBool("isAttack", true);
        attackArea.SetActive(true);

        yield return new WaitForSeconds(AttackActiveTime); // 공격 활성화 시간
        anim.SetBool("isAttack", false);
        attackArea.SetActive(false);
        canMove = true;

        yield return new WaitForSeconds(time);
        if (isAttackRange)
            attackCoroutine = StartCoroutine(Attack(AttackCoolTime));
        else
            attackCoroutine = null; // 코루틴 초기화
    }
}
