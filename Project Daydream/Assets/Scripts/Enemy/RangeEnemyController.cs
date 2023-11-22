using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangeEnemyController : ChaseEnemyController, IAttackRangeCheck
{
    /* --------------- 공격 변수 --------------- */
    protected bool isAttackRange;
    protected Coroutine attackCoroutine;

    /* ---------------- 인스펙터 --------------- */
    [Header("공격")]
    [SerializeField]
    protected Transform bulletPort;
    [SerializeField]
    protected GameObject bulletPrefab;

    [Space(10)]
    [SerializeField, Range(0f, 10f)]
    protected float shotTime = 0.2f;
    [SerializeField, Range(0f, 10f)]
    protected float shotEndTime = 0.45f;
    [SerializeField, Range(0f, 10f)]
    protected float AttackCoolTime = 2f;

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

        yield return new WaitForSeconds(shotTime); // 투사체 발사 대기 시간
        soundController.PlaySound(0);

        /* 투사체 발사 */
        GameObject bullet = Instantiate(bulletPrefab, bulletPort.position, bulletPort.rotation);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.Shot(moveDir);

        yield return new WaitForSeconds(shotEndTime - shotTime); // 발사 애니메이션 끝
        anim.SetBool("isAttack", false);
        canMove = true;

        yield return new WaitForSeconds(time - shotEndTime - shotTime);
        if (isAttackRange)
            attackCoroutine = StartCoroutine(Attack(AttackCoolTime));
        else
            attackCoroutine = null; // 코루틴 초기화
    }
}
