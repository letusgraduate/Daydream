using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMain : MonoBehaviour
{
    private float direction;
    private Rigidbody2D rigid;
    [Header("설정")]
    [SerializeField, Range(0f, 10f)]
    private float bulletSpeed = 5f;

    public float Direction
    {
        set { direction = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = 0;
        Destroy(this.gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = new Vector2(bulletSpeed * direction, 0f);
    }
}
