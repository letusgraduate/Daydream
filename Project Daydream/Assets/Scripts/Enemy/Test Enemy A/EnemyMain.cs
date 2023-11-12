using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    /* ------------ 동작 확인 변수 ------------- */
    private bool isHit = false;

    /* ---------------- 인스펙터 --------------- */
    [Header("설정")]
    [SerializeField, Range(0, 10)]
    private int hp = 2;
    [SerializeField, Range(0f, 10f)]
    private float bouncPower = 3f;
    [SerializeField, Range(0f, 10f)]
    private float invulnTime = 0.5f;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
            OnHit(collision.transform.position);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player Attack")
            OnHit(collision.transform.position);
    }

    void OnHit(Vector2 targetPos)
    {
        isHit = true;
        gameObject.layer = 9; // Super Armor Layer
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int dir = transform.position.x - targetPos.x > 0 ? 1 : -1; // 피격시 튕겨나가는 방향 결정
        rigid.AddForce(new Vector2(dir, 1) * bouncPower, ForceMode2D.Impulse); // 튕겨나가기
        this.transform.Rotate(0, 0, dir * (-10)); // 회전

        HpDown();
    }

    public void HpDown()
    {
        hp--;
        if (hp == 0)
            StartCoroutine(Dead());

        Invoke("OffHit", invulnTime);
    }

    void OffHit()
    {
        gameObject.layer = 10; // Enemy Layer
        spriteRenderer.color = new Color(1, 1, 1, 1f); // 색 변경
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        isHit = false;
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.35f);
        Destroy(gameObject);
    }

    public bool IsHit()
    {
        return isHit;
    }

    public int getHp()
    {
        return hp;
    }
}