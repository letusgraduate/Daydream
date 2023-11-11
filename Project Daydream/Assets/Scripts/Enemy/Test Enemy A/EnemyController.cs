using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected Rigidbody2D rigid;
    protected Animator anim;
    protected SpriteRenderer spriteRenderer;
    protected EnemyMain enemyMain;

    /* ---------------- AI 관련 ---------------*/
    private int nextMove; //행동지표
    private Vector2 frontVec; //바닥 감지
    private RaycastHit2D rayHit;

    private bool chase = false; //플레이어 추적

    /* ---------------- 인스펙터 ---------------*/
    [Header("설정")]
    [SerializeField, Range(0, 10)]
    private float maxSpeed = 2.5f;
    [SerializeField, Range(0, 10)]
    private float raycastDistance = 1f;

    [SerializeField]
    private Transform target; // 플레이어

    /* ---------------- 코루틴 ---------------*/
    IEnumerator enumerator; //선언하지 않으면 Stop,Start에 최종적으로 서로 다른 코루틴이 들어감

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        enemyMain = GetComponent<EnemyMain>();


        enumerator = PlayerChase();
        Move();
    }

    private void FixedUpdate()
    {
        // 이동
        rigid.velocity = new Vector2(nextMove * maxSpeed, rigid.velocity.y);

        // 바닥 감지
        frontVec = new Vector2(rigid.position.x + nextMove * raycastDistance, rigid.position.y);
        rayHit = Physics2D.Raycast(frontVec, Vector3.down, raycastDistance, LayerMask.GetMask("Ground"));

        if (rayHit.collider == null)// 바닥이 없을 때
        {
            Turn();
        }
    }

    void Move() //방향 전환
    {
        if (!enemyMain.IsHit() && chase == false)
        {
            nextMove = Random.Range(-1, 2);//-1, 0, 1
            Animate();

            float nextMoveTime = Random.Range(2f, 5f);
            Invoke("Move", nextMoveTime);
        }
    }

    /* ---------------- 플레이어 추적 ---------------*/
    private void OnTriggerEnter2D(Collider2D collision) //들어옴
    {
        if (collision.gameObject.tag == "Player")
        {
            chase = true;
            StartCoroutine(PlayerChase());
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //나감
    {
        if (collision.gameObject.tag == "Player")
        {
            chase = false;
            StopCoroutine(PlayerChase());
            Move();
        }
    }

    /* ---------------- 회전, 애니메이션(전환) ---------------*/
    public void Turn()
    {
        nextMove *= -1;
        Animate();
    }

    void Animate()
    {
        anim.SetInteger("runSpeed", nextMove);

        if (nextMove != 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -nextMove, transform.localScale.y, transform.localScale.z);
        }
    }


    /* ---------------- 플레이어 추적 ---------------*/
    IEnumerator PlayerChase()
    {
        while (chase == true)
        {
            yield return null;

            //플레이어 방향
            float dir2 = target.position.x - transform.position.x;
            nextMove = (dir2 < 0) ? -1 : 1;

            if (rayHit.collider == null)// 바닥이 없을 때 ( 없으면 그냥 떨어지는 현상 방지 )
            {
                Turn();
            }
            Animate();
        }
    }
}

