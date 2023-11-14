using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* ------------- 컴포넌트 변수 ------------- */
    private Rigidbody2D rigid;
    private Animator anim;
    private PlayerMain playerMain;
    private GameObject platformObject = null;
    private SkillController skillController;

    /* ----------- 입력 값 저장 변수 ----------- */
    private Vector2 moveInput;
    private bool jumpInput;
    private bool dashInput;
    private bool passPlatformInput;

    private bool attackInputs;
    private bool ultimateSkillInput;
    private bool skillAInput;
    private bool skillSInput;
    private bool skillDInput;

    /* ------------ 동작 확인 변수 ------------- */
    private bool isMove = false;
    private bool isJump = false;
    private bool isDash = false;
    private bool isAttack = false;
    private bool isHit = false;
    private bool isDead = false;

    /* ------------ 박스 레이 조절 ------------- */
    private Vector2 boxCastSize = new Vector2(0.5f, 0.05f);
    private float boxCastMaxDistance = 1.0f;

    /* ---------------- 인스펙터 --------------- */
    [Header("설정")]
    [SerializeField, Range(0f, 10f)]
    private float moveSpeed = 5f;
    [SerializeField, Range(0f, 100f)]
    private float jumpPower = 20f;

    [Space(10f)]
    [SerializeField, Range(0f, 100f)]
    private float dashPower = 20f;
    [SerializeField, Range(0f, 1f)]
    private float dashTime = 0.2f;
    [SerializeField, Range(0f, 10f)]
    private float dashChargeTime = 3f;

    /* ---------------- 프로퍼티 --------------- */
    public bool IsHit
    {
        get { return isHit; }
        set { isHit = value; }
    }

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    public bool IsAttack
    {
        get { return isAttack; }
        set { isAttack = value; }
    }

    /* -------------- 이벤트 함수 -------------- */
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerMain = GetComponent<PlayerMain>();
        skillController = GetComponent<SkillController>();
    }

    private void Update()
    {
        /* 입력 값 저장 */
        GetInput();

        /* 바닥 체크 */
        GroundCheck();

        /* 플레이어 이동 조작 */
        Move();
        Jump();
        PassPlatform();
        Dash();

        /* 공격 함수 */
        Attack();
    }

    private void OnDrawGizmos() // 사각 레이 기즈모
    {
        RaycastHit2D rayHit = Physics2D.BoxCast(transform.position, boxCastSize, 0f, Vector2.down, boxCastMaxDistance, LayerMask.GetMask("Ground", "Platform Pass"));

        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, Vector2.down * rayHit.distance);
        Gizmos.DrawWireCube(transform.position + Vector3.down * rayHit.distance, boxCastSize);
    }

    /* --------------- 입력 함수 --------------- */
    private void GetInput()
    {
        if (IsDead == true)
            return;

        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        jumpInput = Input.GetButtonDown("Jump");
        dashInput = Input.GetButtonDown("Dash");
        passPlatformInput = Input.GetButtonDown("Jump") && Input.GetAxisRaw("Vertical") < 0f;

        ultimateSkillInput = Input.GetButtonDown("Ultimate Skill");
        skillAInput = Input.GetButtonDown("Skill A");
        skillSInput = Input.GetButtonDown("Skill S");
        skillDInput = Input.GetButtonDown("Skill D");
        attackInputs = skillDInput || skillSInput || skillAInput || ultimateSkillInput;
    }

    /* ------------- 바닥 체크 함수 ------------- */
    private void GroundCheck()
    {
        // 추락이 아닐 때
        if (rigid.velocity.y > 0)
            return;

        // 점프 없이 낙하
        anim.SetBool("isFalling", true);

        // 바닥 저장(Ground, Platform Pass)
        RaycastHit2D rayHit = Physics2D.BoxCast(transform.position, boxCastSize, 0f, Vector2.down, boxCastMaxDistance, LayerMask.GetMask("Ground", "Platform Pass"));

        if (rayHit.collider != null) // 바닥 체크 될 때
        {
            if (rayHit.distance < 0.9f)
            {
                isJump = false;

                // Platform 태크 체크
                if (platformObject == null && rayHit.collider.tag == "Platform")
                {
                    platformObject = rayHit.collider.gameObject;
                    platformObject.layer = 6; // Ground 레이어
                }
                anim.SetBool("isJumping", false);
                anim.SetBool("isFalling", false);
            }
        }
        else // 공중에 있을 때
        {
            // 플랫폼에서 벗어날 때
            if (platformObject != null)
            {
                platformObject.layer = 12; // Platform Pass 레이어
                platformObject = null;
            }
        }
    }

    /* --------------- 이동 함수 --------------- */
    private void Move()
    {
        if (isDash || isHit)
            return;

        isMove = moveInput.x != 0;
        //isMove = moveInput.magnitude != 0;

        if (isMove)
        {
            // 움직임 조작
            rigid.velocity = new Vector2(moveInput.x * moveSpeed, rigid.velocity.y);

            // 방향 전환
            transform.localScale = new Vector3(moveInput.x, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            // 미끄러짐 방지
            rigid.velocity = new Vector2(0f, rigid.velocity.y);
        }

        anim.SetBool("isRunning", isMove);
    }

    private void Jump()
    {
        if (!jumpInput || passPlatformInput || isJump || isDash || isAttack || isHit)
            return;

        isJump = true;

        // Y축 속도 초기화
        rigid.velocity = new Vector2(rigid.velocity.x, 0);

        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        anim.SetBool("isJumping", true);
    }

    private void PassPlatform()
    {
        if (!passPlatformInput || platformObject == null || isDash || isAttack || isHit)
            return;

        //Debug.Log("아래점프 ON " + platformObject.name); 
        platformObject.layer = 12; // Pass Ground 레이어
    }

    private void Dash()
    {
        // 방향키 입력이 있을 때 대시 가능
        if (playerMain.DashStack <= 0 || !dashInput || moveInput.magnitude == 0 || isDash || isAttack || isHit)
            return;

        isDash = true;
        playerMain.DashStack -= 1;

        /* 대시 중 이동, 포물선, 가속 방지 */
        moveSpeed = 0f;
        rigid.gravityScale = 0f;
        rigid.velocity = new Vector2(0f, 0f);

        rigid.AddForce(moveInput.normalized * dashPower, ForceMode2D.Impulse);
        anim.SetBool("isFalling", false);
        anim.SetBool("isDashing", true);

        StartCoroutine(DashCharge(dashChargeTime));
        StartCoroutine(DashOut(dashTime));
    }

    private IEnumerator DashCharge(float second) // 대시 충전
    {
        yield return new WaitForSeconds(second);
        playerMain.DashStack += 1;

    }

    private IEnumerator DashOut(float second)
    {
        /* 대시 탈출 */
        yield return new WaitForSeconds(second);
        isDash = false;
        rigid.velocity = new Vector2(0f, -1f);

        /* 대시 탈출 후 회복 */
        yield return new WaitForSeconds(0.1f);
        moveSpeed = 5f;
        rigid.gravityScale = 1f;
        anim.SetBool("isDashing", false);
    }

    /* --------------- 공격 함수 --------------- */
    private void Attack()
    {
        if (!attackInputs || isAttack || isDash || isHit)
            return;

        Debug.Log("Attack");
        isAttack = true;

        if (skillDInput)
        {
            skillController.SkillD();
            anim.SetTrigger("doAttack");
        }
            
        if (skillSInput)
        {
            skillController.SkillS();
            anim.SetTrigger("doAttack");
        }
            
        if (skillAInput)
        {
            skillController.SkillA();
            anim.SetTrigger("doAttack");
        }
            
        if (ultimateSkillInput)
        {
            skillController.UltimateSkill();
            anim.SetTrigger("doAttack");
        }
    }

    public void EndAttack() // 애니메이션 끝에 호출
    {
        isAttack = false;
        Debug.Log("End Attack");
    }
}
