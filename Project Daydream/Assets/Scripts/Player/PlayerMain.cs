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
    [SerializeField]
    private GameObject hitArea;

    [Header("설정")]
    [SerializeField, Range(0, 100)]
    private int maxHp = 100;
    [SerializeField, Range(0, 100)]
    private int hp = 100;
    [SerializeField, Range(0, 10)]
    private int dashStack = 3; // 아이템/특성 추가 후 1로 수정

    [Space(10f)]
    [SerializeField, Range(0, 10000)]
    private int maxCoin = 0;
    [SerializeField, Range(0, 10000)]
    private int coin = 0;

    [Space(10f)]
    [SerializeField, Range(0f, 100f)]
    private float knockBack = 10f;
    [SerializeField, Range(0f, 5f)]
    private float superArmorTime = 0.8f;

    /* ---------------- 프로퍼티 --------------- */
    public int MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }

    public int Hp
    {
        get { return hp; }
        set // hp 변동 값 입력
        {
            if (value <= 0)
                hp = 0;
            else if (value > maxHp)
                hp = maxHp;
            else
                hp = value;
        }
    }

    public int DashStack
    {
        get { return dashStack; }
        set { dashStack = value; }
    }

    public int Coin
    {
        get { return coin; }
        set
        {
            if (value <= 0)
                coin = 0;
            else if (value > maxCoin)
                coin = maxCoin;
            else
                coin = value;
        }
    }

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

        if (collision.gameObject.layer == 15) // Currency
        {
            GetCurrency(collision.gameObject);
        }
            
    }

    /* --------------- 피격 관련 --------------- */
    void OnHit(Vector2 targetPos)
    {
        if (playerController.IsHit == true)
            return;
        
        hitArea.layer = 13; // Super Armor Layer
        playerController.IsHit = true;

        //spriteRenderer.color = new Color(1, 1, 1, 0.4f); // 피격당했을 때 색 변경
        for (int i = 0; i < spriteLen; i++)
            spriteRenderers[i].color = new Color(1, 1, 1, 0.4f);

        rigid.velocity = Vector2.zero; // 추가적인 속력 방지
        int dir = transform.position.x - targetPos.x > 0 ? 1 : -1; // 피격시 튕겨나가는 방향 결정
        rigid.AddForce(new Vector2(dir, 1) * knockBack, ForceMode2D.Impulse); // 튕겨나가기

        this.transform.Rotate(0, 0, dir * (-10)); // 회전
        StartCoroutine(ReRotate(0.4f));

        anim.SetTrigger("doHit"); // 애니메이션 트리거
        StartCoroutine(OffHit(superArmorTime)); // superArmorTime 후 무적 시간 끝

        Hp -= 10; // 차후 공격력 받아와 변수 대입
    }

    IEnumerator ReRotate(float second) // 회전 초기화, 다시 조작 가능
    {
        yield return new WaitForSeconds(second);
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        playerController.IsHit = false;
    }

    IEnumerator OffHit(float second)
    {
        yield return new WaitForSeconds(second);
        hitArea.layer = 7; // Player Layer
        for (int i = 0; i < spriteLen; i++)
            spriteRenderers[i].color = new Color(1, 1, 1, 1f);
        //spriteRenderer.color = new Color(1, 1, 1, 1f); // 색 변경
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

    /* --------------- 피격 관련 --------------- */
    void GetCurrency(GameObject gameObject)
    {
        GameManager.instance.MoonRock += 1;
        Debug.Log(GameManager.instance.MoonRock);

        Destroy(gameObject);
    }

}
