using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerADD : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private EnemyMain enemyMain;

    /* -------------=--- AI관련 =--------------- */
    private int nextMove; // 행동 지표를 결정할 변수 하나 생성
    private int saveDir = 1;

    //추가
    private bool chase = false;

    /* ---------------- 인스펙터 --------------- */
    [Header("설정")]
    [SerializeField, Range(0, 10)]
    private float maxSpeed = 2.5f;
    [SerializeField, Range(0, 10)]
    private float raycastDistance = 1f;

    //추가
    [SerializeField]
    private Transform target;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        enemyMain = GetComponent<EnemyMain>();

        Invoke("Think", 3);
    }

    void FixedUpdate()
    {
        if (!enemyMain.IsHit())
        {
            //지형 체크
            Vector2 frontVec = new Vector2(rigid.position.x + nextMove * raycastDistance, rigid.position.y);


            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, raycastDistance, LayerMask.GetMask("Ground"));


            //추가-----------------------------------------
            float dis = Vector2.Distance(transform.position, target.position); // 타겟과 위치 거리
            if (dis <=5 && rayHit.collider !=null)
            {
                Playerchase();
            }
            else
            {
                rigid.velocity = new Vector2(nextMove * maxSpeed, rigid.velocity.y); // 기존 코드
                chase = false;
            }
            // --------------------------------------------

            //Debug.DrawRay(frontVec, Vector3.down, new Color(0, raycastDistance, 0)); // 에디터 상에서만 레이를 그려준다
            // RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, raycastDistance, LayerMask.GetMask("Ground"));
            if (rayHit.collider == null) // 바닥 감지가 없을 때
                Turn();


        }
    }

    void Think()
    {
        if (!enemyMain.IsHit())
        {
            Move();

            float nextThinkTime = Random.Range(2f, 5f); // 2.0 ~ 5.0
            Invoke("Think", nextThinkTime);
        }
    }

    void Move() // 이동 방향 결정
    {
        nextMove = Random.Range(-1, 2); // -1, 0, 1

        if (nextMove == 1)  // 방향 저장용
            saveDir = 1;
        else if (nextMove == -1)
            saveDir = -1;

        Animate();
   
    }

    public void Turn() // 속도는 유지한채로 방향만 전환 -> 다음 액션까지 2초 연장
    {
        nextMove *= -1;
        Animate();

        CancelInvoke();
        Invoke("Think", 2);

    }

    void Animate() // 스프라이트 애니메이션 관련
    {
        anim.SetInteger("runSpeed", nextMove);

        if (nextMove != 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -nextMove, transform.localScale.y, transform.localScale.z);
        //spriteRenderer.flipX = nextMove == 1;
    }

    public void lookBack()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public int getThink()
    {
        return nextMove;
    }

    public int getDir()
    {
        return saveDir;
    }

    //추가
    public void Playerchase() //플레이어 추적
    {
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        transform.Translate(new Vector2(dir, 0) * maxSpeed *Time.deltaTime);

        chase = true;
    }
}
