using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitController : MonoBehaviour
{
    /* ------------- 컴포넌트 변수 ------------- */
    private GameObject player;
    private PlayerMain playerMain;
    private PlayerController playerController;
    private ItemManager itemManager;

    /* -------------- 이벤트 함수 -------------- */
    private void Start()
    {
        player = GameManager.instance.Player;
        itemManager = GameManager.instance.ItemManager;
        playerMain = player.GetComponent<PlayerMain>();
        playerController = player.GetComponent<PlayerController>();
    }
    
    /* ------------- 특성 업글 함수 ------------ */
    public void SpeedUP()
    {
        playerController.MoveSpeed += 1;
    }

    public void MaxHpUP()
    {
        playerMain.MaxHp += 10;
    }

    public void PowerUP()
    {

    }

    public void CoinUP()
    {

    }

    public void DashUP()
    {
        playerMain.MaxDashStack += 1;
        playerMain.DashStack += 1;
    }

    public void ItemUP()
    {
        itemManager.MaxItemCount += 1;
    }
}
