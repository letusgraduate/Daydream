using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    /* ---------------- 컴포넌트 --------------- */
    private Rigidbody2D rigid;

    /* ---------------- 인스펙터 --------------- */
    [Header("오브젝트 연결")]
    [SerializeField]
    private GameObject explosionPrefab;

    [Header("설정")]
    [SerializeField]
    private bool isReflect = true;
    [SerializeField]
    private float forceX = 4f;
    [SerializeField]
    private float forceY = 0f; // 설정하면 포물선 이동

    /* ---------------- 프로퍼티 --------------- */
    public float ForceX
    {
        get { return forceX; }
        set { forceX = value; }
    }

    public float ForceY
    {
        get { return forceY; }
        set { forceY = value; }
    }

    /* --------------- 이벤트 함수 -------------- */
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        StartCoroutine(LifeSpan(5f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explosion(); // 피격 혹은 투사체 파괴
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Attack") && isReflect)
        {
            if (isReflect)
                Reflect(collision.transform.position); // 투사체 반사
            else
                Explosion(); // 투사체 파괴
        }
    }

    /* --------------- 투사체 관련 -------------- */
    private void Reflect(Vector2 targetPos)
    {
        gameObject.layer = 8; // Player Attack
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

        int dir = transform.position.x - targetPos.x > 0 ? 1 : -1; // 충돌 반대로
        
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        rigid.AddForce(new Vector2(ForceX * dir, ForceY), ForceMode2D.Impulse);
    }

    private void Explosion()
    {
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab);
            explosion.transform.position = this.transform.position;
            explosion.transform.localScale = this.transform.localScale;
        }

        Destroy(gameObject);
    }

    private IEnumerator LifeSpan(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    /* --------------- 외부 호출 --------------- */
    public void Shot(int dir)
    {
        transform.localScale = new Vector2(transform.localScale.x * dir, transform.localScale.y);
        rigid.AddForce(new Vector2(ForceX * dir, ForceY), ForceMode2D.Impulse);
    }
}
