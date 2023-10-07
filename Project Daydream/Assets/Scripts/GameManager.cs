using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* ------------- 컴포넌트 변수 ------------- */
    private PlayerMain playerMain;
    private PlayerController playerController;

    /* ---------------- 인스펙터 --------------- */
    [Header("오브젝트 연결")]
    [SerializeField]
    private GameObject player;

    /* -------------- 이벤트 함수 -------------- */
    void Awake()
    {
        playerMain = player.GetComponent<PlayerMain>();
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        //Debug.Log(playerMain.Hp);
    }
}
