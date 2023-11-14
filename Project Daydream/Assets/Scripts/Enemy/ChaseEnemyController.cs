using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemyController : EnemyController, IAggroCheck
{
    /* ------------- 컴포넌트 변수 ------------- */
    protected AggroArea aggroArea;

    /* -------------- 어그로 관련 -------------- */
    protected bool isAggro;
    protected bool isEdge;
    protected int saveEdgeDir;

    /* -------------- 이벤트 함수 -------------- */
    protected override void Awake()
    {
        base.Awake();

        aggroArea = GetComponentInChildren<AggroArea>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        ChaseTarget();
    }

    /* --------------- 인터페이스 -------------- */
    public void InAggro() // 어그로 풀링
    {
        if (thinkCoroutine != null)
            StopCoroutine(thinkCoroutine);

        isAggro = true;
        Debug.Log(gameObject.name + " chase start");
    }

    public void OutAggro() // 어그로 해제
    {
        thinkCoroutine = StartCoroutine(Think(2f));

        isAggro = false;
        Debug.Log(gameObject.name + " chase stop");
    }

    public int PlayerDir { get; set; } // 어그로에서 방향 받아옴

    /* --------------- 추격 관련 --------------- */
    protected void ChaseTarget()
    {
        if (!isAggro) // 논어그로 시
            return;
        
        if (!isEdge) // 구석이 아닐 시
            moveDir = PlayerDir;
        else if (saveEdgeDir != PlayerDir) // 구석이고 플레이어와 방향이 다를 시
            isEdge = false;
    }

    protected override void EcountEdge()
    {
        if (!isAggro) // 논어그로 시 그대로 작동
        {
            base.EcountEdge();
        }
        else // 어그로 시 모서리에 멈춤
        {
            saveEdgeDir = moveDir; // 구석을 바라보던 방향
            moveDir = 0;
            isEdge = true;

            Debug.Log(gameObject.name + " hang in edge");
        }
    }
}
