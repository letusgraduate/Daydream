using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1BossController : ChaseEnemyController, IAttackRangeCheck, IAnimCheck
{
    /* --------------- 공격 변수 --------------- */
    private bool isAttackRange;
    private Coroutine attackCoroutine;

    /* ---------------- 인스펙터 --------------- */
    [SerializeField]
    private Collider2D hitbox;
    
    [Header("공격")]
    [SerializeField, Range(0f, 10f)]
    private float AttackCoolTime = 2f;

    /* --------------- 인터페이스 -------------- */
    /* 공격 범위 */
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

    /* 애니메이션 체크 */
    public void AnimEnd()
    {
        canMove = true;

        anim.SetBool("isAttack", false);
    }

    public void AnimShot()
    {

    }

    public void AnimSound()
    {
        soundController.PlaySound(0);
    }

    /* --------------- 공격 관련 --------------- */
    private IEnumerator Attack(float time)
    {
        canMove = false;
        anim.SetBool("isAttack", true);

        yield return new WaitForSeconds(time);
        if (isAttackRange)
            attackCoroutine = StartCoroutine(Attack(AttackCoolTime));
        else
            attackCoroutine = null; // 코루틴 초기화
    }
}