using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMain : EnemyMain
{
    /* ---------------- 인스펙터 --------------- */
    [Header("포탈 스포너")]
    [SerializeField]
    private GameObject portalSpawner;

    /* --------------- 기능 함수 --------------- */
    protected override IEnumerator Dead()
    {
        soundController.PlaySound(1);

        yield return new WaitForSeconds(0.35f);

        DropCurrency();
        GameManager.instance.PlayerScore += enemyScore;

        GameManager.instance.StageClear(); // 스테이지 클리어
        portalSpawner.SetActive(true);

        Destroy(gameObject);
    }

    protected override void DropCurrency()
    {
        Instantiate(GameManager.instance.MoonRockPrefab, transform.position, transform.rotation);
    }
}
