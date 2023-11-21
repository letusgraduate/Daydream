using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain : MonoBehaviour
{
    /* ------------- 컴포넌트 변수 ------------- */
    protected Rigidbody2D rigid;
    protected Animator anim;
    protected SpriteRenderer spriteRenderer;
    protected SoundController soundController;

    /* --------------- 피격 관련 --------------- */
    protected bool isHit = false;
    protected int takeDamage = 0;

    /* ---------------- 인스펙터 --------------- */
    [Header("설정")]
    [SerializeField, Range(0, 1000)]
    protected int hp = 30;
    [SerializeField, Range(0f, 10f)]
    protected float knockBackPower = 3f;
    [SerializeField, Range(0f, 10f)]
    protected float superArmorTime = 0.5f;
    [SerializeField, Range(0, 1000)]
    protected int enemyScore = 10;

    /* ---------------- 프로퍼티 --------------- */
    public bool IsHit
    {
        get { return isHit; }
    }

    public int Hp
    {
        get { return hp; }
    }

    /* -------------- 이벤트 함수 -------------- */
    protected void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        soundController = GetComponent<SoundController>();
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player Attack"))
        {
            takeDamage = collision.gameObject.GetComponent<DamageMain>().Damage;
            OnHit(collision.transform.position, takeDamage);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Attack"))
        {
            takeDamage = collision.GetComponent<DamageMain>().Damage;
            OnHit(collision.transform.position, takeDamage);
        }
    }

    /* --------------- 기능 함수 --------------- */
    protected void OnHit(Vector2 targetPos, int damage)
    {
        isHit = true;
        gameObject.layer = 9; // Super Armor Layer
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        soundController.PlaySound(2);

        int dir = transform.position.x - targetPos.x > 0 ? 1 : -1; // 피격시 튕겨나가는 방향 결정
        rigid.AddForce(new Vector2(dir, 1) * knockBackPower, ForceMode2D.Impulse); // 튕겨나가기
        this.transform.Rotate(0, 0, dir * (-10)); // 회전

        hp -= damage;

        if (hp <= 0)
            StartCoroutine(Dead());

        StartCoroutine(OffHit(superArmorTime));
    }

    protected IEnumerator OffHit(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.layer = 10; // Enemy Layer
        spriteRenderer.color = new Color(1, 1, 1, 1f); // 색 변경
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        isHit = false;
    }

    protected IEnumerator Dead()
    {
        soundController.PlaySound(1);

        yield return new WaitForSeconds(0.35f);

        DropCurrency();
        GameManager.instance.PlayerScore += enemyScore;

        Destroy(gameObject);
    }

    protected virtual void DropCurrency()
    {
        Instantiate(GameManager.instance.CoinPrefab, transform.position, transform.rotation);
    }
}
