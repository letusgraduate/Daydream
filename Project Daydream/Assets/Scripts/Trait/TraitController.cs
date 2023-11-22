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
    private SkillManager skillManager;
    private SkillController skillController;

    /* -------------- 이벤트 함수 -------------- */
    private void Start()
    {
        player = GameManager.instance.Player;
        itemManager = GameManager.instance.ItemManager;
        skillManager = GameManager.instance.SkillManager;
        playerMain = player.GetComponent<PlayerMain>();
        playerController = player.GetComponent<PlayerController>();
        skillController = player.GetComponent<SkillController>();
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
        skillController.DamageMultiple += 1;
    }

    public void SkillUP()
    {
        skillManager.SkillUpgrade += 1;
        UIManager.instance.SetSkillImageColorUI(skillManager.SkillUpgrade);
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
