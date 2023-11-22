using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage2BossController : ChaseEnemyController, IAttackRangeCheck, IAnimCheck
{
    /* --------------- 공격 변수 --------------- */
    private bool isAttackRange;
    private Coroutine attackCoroutine;
    private int attackThink;

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

        anim.SetBool("isDoubleSlash", false);
        anim.SetBool("isDodgeAttack", false);
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
        attackThink = Random.Range(0, 2);

        switch (attackThink)
        {
            case 0:
                DoubleSlash();
                break;
            case 1:
                DodgeAttack();
                break;
        }

        yield return new WaitForSeconds(time);
        if (isAttackRange)
            attackCoroutine = StartCoroutine(Attack(AttackCoolTime));
        else
            attackCoroutine = null; // 코루틴 초기화
    }

    private void DoubleSlash()
    {
        anim.SetBool("isDoubleSlash", true);
    }

    private void DodgeAttack()
    {
        anim.SetBool("isDodgeAttack", true);
    }
}
