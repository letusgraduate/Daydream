using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemyController : EnemyController, IAggroCheck
{
    /* ------------- 컴포넌트 변수 ------------- */
    protected AggroArea aggroArea;

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
    public void InAggro()
    {
        StopAllCoroutines();
        isAggro = true;
        Debug.Log(gameObject.name + " chase start");
    }

    public void OutAggro()
    {
        StartCoroutine(Think(2f));
        isAggro = false;
        Debug.Log(gameObject.name + " chase stop");
    }

    public int PlayerDir { get; set; }

    /* --------------- 추격 관련 --------------- */
    protected void ChaseTarget()
    {
        if (!isAggro)
            return;
        
        if (!isEdge)
        {
            moveDir = PlayerDir;
        }
        else if (saveEdgeDir != PlayerDir)
        {
            isEdge = false;
        }
    }

    protected override void EcountEdge()
    {
        if (!isAggro) // 논어그로 시 그대로 작동
        {
            base.EcountEdge();
        }
        else // 어그로 시 모서리에 멈춤
        {
            saveEdgeDir = moveDir;
            moveDir = 0;
            isEdge = true;

            Debug.Log(gameObject.name + " hang in edge");
        }
    }
}
