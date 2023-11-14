using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    /* ---------------- 인스펙터 --------------- */
    [Header("Scene Name")]
    [SerializeField]
    private string normalMap;
    [SerializeField]
    private string coinMap;
    [SerializeField]
    private string healingMap;
    [SerializeField]
    private string storeMap;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* 포탈에 플레이어가 접촉하면 맵 이동 */
        if (collision.gameObject.CompareTag("Player"))
        {
            // 일반 포탈에 접촉 시 다음 일반맵으로 이동
            if (this.CompareTag("PortalNormal")) // 프리팹으로 포탈을 생성해서 (Clone) 붙임
            {
                SceneManager.LoadScene(normalMap);
                // SceneManager.LoadScene(3); // 씬 번호로 이동
            }
            // 보너스 포탈에 접촉 시 랜덤한 맵으로 이동
            else if (this.CompareTag("PortalBonus"))
            {
                int randomNumber = Random.Range(1, 100);

                if (randomNumber <= 33)
                    SceneManager.LoadScene(coinMap);
                else if (randomNumber <= 66)
                    SceneManager.LoadScene(healingMap);
                else
                    SceneManager.LoadScene(storeMap);
            }
        }
    }
}