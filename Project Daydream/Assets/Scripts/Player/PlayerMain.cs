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
    private SkillManager skillManager;
    private ItemManager itemManager;
    private ItemMain itemMain;

    /* --------------- 피격 관련 --------------- */
    private int spriteLen = 0; // 하위 스프라이트 목록

    /* ---------------- 인스펙터 --------------- */
    [Header("오브젝트 연결")]
    [SerializeField]
    private GameObject hitArea;
    [SerializeField]
    private Transform ultimateSkillAnchor;

    [Header("설정")]
    [SerializeField, Range(0, 100)]
    private int maxHp = 100;
    [SerializeField, Range(0, 100)]
    private int hp = 50;
    [SerializeField, Range(0, 10)]
    private int maxDashStack = 1;
    [SerializeField, Range(0, 10)]
    private int dashStack = 1;

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
        set
        {
            maxHp = value;

            UIManager.instance.SetHpUI();
        }
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

            UIManager.instance.SetHpUI();
        }
    }

    public int DashStack
    {
        get { return dashStack; }
        set
        {
            if (value > maxDashStack)
                dashStack = maxDashStack;
            else
                dashStack = value;

            UIManager.instance.SetDashStackUI();
        }
    }

    public int MaxDashStack
    {
        get { return maxDashStack; }
        set
        {
            maxDashStack = value;
            UIManager.instance.SetDashStackUI();
        }
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

            UIManager.instance.SetCoinUI();
        }
    }

    public Transform UltimateSkillAnchor { get { return ultimateSkillAnchor; } }

    /* -------------- 이벤트 함수 -------------- */
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();

        spriteLen = spriteRenderers.Length;
        //OffHit();
    }

    private void Start()
    {
        skillManager = GameManager.instance.SkillManager;
        itemManager = GameManager.instance.ItemManager;
    }

    /* --------------- 피격 관련 --------------- */
    public void OnHit(Vector2 targetPos, int damage)
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

        Hp -= damage;

        if (Hp <= 0)
        {
            OnDead();
            return;
        }

        this.transform.Rotate(0, 0, dir * (-10)); // 회전
        anim.SetTrigger("doHit"); // 애니메이션 트리거

        StartCoroutine(ReRotate(0.4f));
        StartCoroutine(OffHit(superArmorTime)); // superArmorTime 후 무적 시간 끝
    }

    private IEnumerator ReRotate(float second) // 회전 초기화, 다시 조작 가능
    {
        yield return new WaitForSeconds(second);
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        playerController.IsHit = false;
    }

    private IEnumerator OffHit(float second)
    {
        yield return new WaitForSeconds(second);
        hitArea.layer = 7; // Player Layer
        for (int i = 0; i < spriteLen; i++)
            spriteRenderers[i].color = new Color(1, 1, 1, 1f);
        //spriteRenderer.color = new Color(1, 1, 1, 1f); // 색 변경
    }

    public void OnDead()
    {
        if (itemManager.Resurrection)
        {
            itemManager.UseResurrectionItem();
            Hp = 100;
            return;
        }

        anim.SetTrigger("doDie");
        playerController.IsDead = true;
        rigid.velocity = new Vector2(0f, rigid.velocity.y);

        GameManager.instance.GameOver();
    }

    /* -------------- 아이템 관련 -------------- */
    public void GetCurrency(GameObject gameObject)
    {
        if (gameObject.tag == "Moon Rock")
            GameManager.instance.MoonRock += 1;
        if (gameObject.tag == "Coin")
            Coin += 1;

        Destroy(gameObject);
    }

    public void GetUltimateSkill(GameObject gameObject)
    {
        if (ultimateSkillAnchor.childCount != 0) //현재 가지고 있는 궁스킬 삭제
            Destroy(ultimateSkillAnchor.GetChild(0).gameObject);

        int skillNum = gameObject.GetComponent<UltimateSkillMain>().UltimateSkillNum; // 먹은 스킬 아이템의 종류 파악
        skillManager.GetComponent<SkillManager>().UltimateSkillCoolTime = gameObject.GetComponent<UltimateSkillMain>().UltimateSkillCoolTime; //궁스킬 쿨타임 설정
        GameObject skill = Instantiate(skillManager.GetUltimateSkill(skillNum), ultimateSkillAnchor); // 궁스킬 프리팹 소환
        skill.transform.localPosition = Vector3.zero;

        Destroy(gameObject); //스킬 아이템 삭제
    }

    public void GetItem(GameObject gameObject)
    {
        if (itemManager.ItemStock >= itemManager.MaxItemCount) // 아이템이 3보다 적으면 아이템 스톡 1 증가
            return;

        itemMain = gameObject.GetComponent<ItemMain>();
        itemManager.ItemList(itemManager.ItemStock, itemMain.ItemImage, itemMain.IsActiveItem, itemMain.ItemNum);
        itemManager.ItemStock++;

        if (!itemMain.IsActiveItem)
            itemManager.PassiveItem(itemMain.ItemNum);

        UIManager.instance.SetItemUI();

        Destroy(gameObject); //아이템 삭제
    }
}
