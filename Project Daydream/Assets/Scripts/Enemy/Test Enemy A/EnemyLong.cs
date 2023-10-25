using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLong : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    private Transform target;

    //시간
    [SerializeField]
    private float attackRate = 2f; // 공격 주기
    private float timeAfterAttack; // 공격 시간 체크

    void Start()
    {
        timeAfterAttack = 0f;
    }

    void Update()
    {
        timeAfterAttack += Time.deltaTime;

        if(timeAfterAttack >= attackRate)
        {
            timeAfterAttack = 0f;
            //프리팹을 주기마다 생성
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }
}
