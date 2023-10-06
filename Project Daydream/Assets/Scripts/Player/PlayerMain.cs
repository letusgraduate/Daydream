using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    /* ------------- 컴포넌트 변수 ------------- */
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer[] spriteRenderers;
    private PlayerController playerController;

    /* --------------- 스프라이트 -------------- */
    private int spriteLen = 0;

    /* ---------------- 인스펙터 --------------- */
    [Header("오브젝트 연결")]
    //[SerializeField]
    //private GameManager gameManager;

    [Header("설정")]
    [SerializeField, Range(0f, 100f)]
    private float knockBack = 10f;
    [SerializeField, Range(0f, 5f)]
    private float invulnTime = 0.8f;

    /* -------------- 이벤트 함수 -------------- */
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();

        spriteLen = spriteRenderers.Length;
        //OffHit();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            OnHit(collision.transform.position); // Enemy의 위치 정보 매개변수
        if (collision.gameObject.tag == "Bullet")
            OnHit(collision.transform.position);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy Attack")
            OnHit(collision.transform.position);
        if (collision.gameObject.tag == "Trap")
            OnHit(collision.transform.position);
    }

    /* --------------- 기능 함수 --------------- */
    void OnHit(Vector2 targetPos)
    {
        Debug.Log(playerController.IsHit);
        if (playerController.IsHit == true)
            return;
        
        gameObject.layer = 13; // Super Armor Layer
        playerController.IsHit = true;

        //spriteRenderer.color = new Color(1, 1, 1, 0.4f); // 피격당했을 때 색 변경
        for (int i = 0; i < spriteLen; i++)
            spriteRenderers[i].color = new Color(1, 1, 1, 0.4f);

        rigid.velocity = Vector2.zero; // 추가적인 속력 방지
        int dir = transform.position.x - targetPos.x > 0 ? 1 : -1; // 피격시 튕겨나가는 방향 결정
        rigid.AddForce(new Vector2(dir, 1) * knockBack, ForceMode2D.Impulse); // 튕겨나가기

        this.transform.Rotate(0, 0, dir * (-10)); // 회전
        Invoke("ReRotate", 0.4f);

        anim.SetTrigger("doHit"); // 애니메이션 트리거
        Invoke("OffHit", invulnTime); // invulnTime 후 무적 시간 끝

        //gameManager.HpDown();
    }

    void OffHit()
    {
        gameObject.layer = 7; // Player Layer
        //spriteRenderer.color = new Color(1, 1, 1, 1f); // 색 변경
        for (int i = 0; i < spriteLen; i++)
            spriteRenderers[i].color = new Color(1, 1, 1, 1f);
    }

    void ReRotate() // 회전 초기화, 다시 조작 가능
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        playerController.IsHit = false;
    }

    void OnDead()
    {
        gameObject.layer = 9;
        playerController.IsHit = true;

        //spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        for (int i = 0; i < spriteLen; i++)
            spriteRenderers[i].color = new Color(1, 1, 1, 0.4f);

        anim.SetTrigger("doDead");

        CancelInvoke("ReRotate");
        CancelInvoke("OffHit");
    }
}
