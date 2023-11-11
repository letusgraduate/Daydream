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

    /* ---------------- AI 관련 ---------------- */
    protected int nextMove;
    protected Vector2 frontVec;
    protected RaycastHit2D rayHit;

    /* ---------------- 인스펙터 --------------- */
    [Header("설정")]
    [SerializeField, Range(0, 10)]
    protected float maxSpeed = 2.5f;
    [SerializeField, Range(0, 10)]
    protected float raycastDistance = 1f;

    /* -------------- 이벤트 함수 -------------- */
    protected void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        enemyMain = GetComponent<EnemyMain>();
    }

    protected void Start()
    {
        StartCoroutine(Think(3f));
    }

    protected void FixedUpdate()
    {
        Move();
    }

    /* --------------- 기능 함수 --------------- */
    protected IEnumerator Think(float time)
    {
        Debug.Log("Start Think");

        yield return new WaitForSeconds(time);

        nextMove = Random.Range(-1, 2); // (-1, 0, 1)
        float nextThinkTime = Random.Range(2f, 5f); // 2.0 ~ 5.0

        StartCoroutine(Think(nextThinkTime));
    }

    protected void Move()
    {
        if (enemyMain.IsHit)
            return;

        rigid.velocity = new Vector2(nextMove * maxSpeed, rigid.velocity.y);

        //지형 체크
        frontVec = new Vector2(rigid.position.x + nextMove * raycastDistance, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, raycastDistance, 0));
        rayHit = Physics2D.Raycast(frontVec, Vector3.down, raycastDistance, LayerMask.GetMask("Ground"));
        if (rayHit.collider == null) // 바닥 감지가 없을 때
            TurnEdge();

        anim.SetInteger("runSpeed", nextMove);

        if (nextMove != 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -nextMove, transform.localScale.y, transform.localScale.z);
    }

    protected void TurnEdge()
    {
        Debug.Log("trun edge");
        nextMove *= -1;

        StopAllCoroutines();
        StartCoroutine(Think(2f));
    }
}
