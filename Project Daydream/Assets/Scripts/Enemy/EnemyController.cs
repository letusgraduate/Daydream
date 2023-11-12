using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /* ------------- 컴포넌트 변수 ------------- */
    protected Rigidbody2D rigid;
    protected Animator anim;
    protected SpriteRenderer spriteRenderer;
    protected EnemyMain enemyMain;

    /* ---------------- AI 변수 ---------------- */
    protected int moveDir;
    protected float thinkTime;
    protected bool canMove = true;

    /* --------------- 레이 변수 --------------- */
    protected Vector2 rayPos;
    protected RaycastHit2D rayHit;

    /* ---------------- 인스펙터 --------------- */
    [Header("설정")]
    [SerializeField, Range(0f, 10f)]
    protected float moveSpeed = 2.5f;
    [SerializeField, Range(0f, 10f)]
    protected float rayDis = 1f;

    /* -------------- 이벤트 함수 -------------- */
    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        enemyMain = GetComponent<EnemyMain>();
    }

    protected void Start()
    {
        StartCoroutine(Think(2f));
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    /* --------------- 기능 함수 --------------- */
    protected IEnumerator Think(float time)
    {
        yield return new WaitForSeconds(time);

        moveDir = Random.Range(-1, 2); // 이동 방향 (-1, 0, 1)
        thinkTime = Random.Range(2f, 5f); // 다음 생각 시간 (2.0 ~ 5.0)

        StartCoroutine(Think(thinkTime));
    }

    protected void Move()
    {
        if (enemyMain.IsHit || !canMove)
            return;

        rigid.velocity = new Vector2(moveDir * moveSpeed, rigid.velocity.y);

        /* 지형 체크 */
        rayPos = new Vector2(rigid.position.x + moveDir * rayDis, rigid.position.y);
        Debug.DrawRay(rayPos, Vector3.down, new Color(0, rayDis, 0));
        rayHit = Physics2D.Raycast(rayPos, Vector3.down, rayDis, LayerMask.GetMask("Ground"));
        if (rayHit.collider == null) // 바닥 감지가 없을 때
            EcountEdge();

        anim.SetInteger("runSpeed", moveDir);

        if (moveDir != 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -moveDir, transform.localScale.y, transform.localScale.z);
    }

    protected virtual void EcountEdge()
    {
        moveDir *= -1;

        StopAllCoroutines();
        StartCoroutine(Think(2f));
    }
}
