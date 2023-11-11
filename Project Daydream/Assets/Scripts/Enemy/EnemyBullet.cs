using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D bulletRgidbody;
    Transform playerPos;
    Vector2 dir;

    void Start()
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        dir = playerPos.position - transform.position; //플레이어 위치 - 몹 위치

        GetComponent<Rigidbody2D>().AddForce(dir.normalized * Time.deltaTime * 100000);

        Destroy(gameObject, 3f); // 오브젝트에 부딪히면 없어지게 수정 예정 *****
    }
}
