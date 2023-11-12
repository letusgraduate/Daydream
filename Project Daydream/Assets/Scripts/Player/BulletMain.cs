using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMain : MonoBehaviour
{
    /* --------------컴포넌트 변수-------------- */
    private Rigidbody2D rigid;
    /* --------------불렛 변수-------------- */
    private float bulletSpeed = 5f;
    private float direction;
    /* ---------------- 인스펙터 --------------- */
    [Header("설정")]
    [SerializeField, Range(0f, 10f)]

    /* ---------------- 프로퍼티 --------------- */
    public float Direction
    {
        set { direction = value; }
    }

    /* -------------- 이벤트 함수 -------------- */
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = 0;
        Destroy(this.gameObject, 1.0f);
    }

    void Update()
    {
        //불렛 이동
        rigid.velocity = new Vector2(bulletSpeed * direction, 0f);
    }
}
