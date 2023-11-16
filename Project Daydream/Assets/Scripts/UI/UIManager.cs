using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    /* -------------- 싱글톤 변수 -------------- */
    public static UIManager instance = null;

    /* ------------- 컴포넌트 변수 ------------- */
    private GameObject player;
    private PlayerMain playerMain;
    private PlayerController playerController;
    private SkillController skillController;
    private SkillManager skillManager;
    private ItemManager itemManager;

    private GameObject item1;
    private GameObject item2;
    private GameObject item3;
    private GameObject itemPick1;
    private GameObject itemPick2;
    private GameObject itemPick3;

    private GameObject skillA;
    private GameObject skillS;
    private GameObject skillD;

    /* ---------------- 인스펙터 --------------- */
    private float skillATimer;
    private float skillSTimer;
    private float skillDTimer;
    private float ultimateSkillTimer;
    private int itemPickCount;

    /* ---------------- 인스펙터 --------------- */
    [Header("오브젝트 연결")]
    [SerializeField]
    private GameObject hpUI;
    [SerializeField]
    private GameObject coinUI;
    [SerializeField]
    private GameObject moonRockUI;
    [SerializeField]
    private GameObject dashStackUI;
    [SerializeField]
    private GameObject ultimitSkill;
    [SerializeField]
    private GameObject normalSkill;
    [SerializeField]
    private GameObject itemUI;
    //[SerializeField]
    //private GameObject miniMap;

    [Header("아이템 관련 테스트 변수")]
    [SerializeField, Range(0f, 3f)]
    private int itemCount; // 현재 들고 있는 아이템 수
    [SerializeField]
    private bool[] isActiveItem = new bool[3]; // 아이템의 사용가능 유무

    [Header("스킬 쿨타임")]
    [SerializeField, Range(0f, 100f)]
    private float skillACoolTime;
    [SerializeField, Range(0f, 100f)]
    private float skillSCoolTime;
    [SerializeField, Range(0f, 100f)]
    private float skillDCoolTime;

    /* -------------- 이벤트 함수 -------------- */
    private void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }

        item1 = itemUI.transform.GetChild(0).gameObject;
        item2 = itemUI.transform.GetChild(1).gameObject;
        item3 = itemUI.transform.GetChild(2).gameObject;
        itemPick1 = item1.transform.GetChild(3).gameObject;
        itemPick2 = item2.transform.GetChild(3).gameObject;
        itemPick3 = item3.transform.GetChild(3).gameObject;

        skillA = normalSkill.transform.GetChild(0).gameObject;
        skillS = normalSkill.transform.GetChild(1).gameObject;
        skillD = normalSkill.transform.GetChild(2).gameObject;
    }

    private void Start()
    {
        player = GameManager.instance.Player;
        skillManager = GameManager.instance.SkillManager;
        itemManager = GameManager.instance.ItemManager;

        playerMain = player.GetComponent<PlayerMain>();
        playerController = player.GetComponent<PlayerController>();
        skillController = player.GetComponent<SkillController>();

        skillACoolTime = skillManager.SkillACoolTime;
        skillSCoolTime = skillManager.SkillSCoolTime;
        skillDCoolTime = skillManager.SkillDCoolTime;

        SetHpUI();
        SetDashStackUI();
        SetCoinUI();
        SetMoonRockUI();
        ShowItemUI();
        SETItemPick();
        //miniMap.SetActive(false);
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.M))
    //    {
    //        miniMap.SetActive(true);
    //    }
    //    else if (Input.GetKeyUp(KeyCode.M))
    //    {
    //        miniMap.SetActive(false);
    //    }
    //}
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ItemPickCountSettings();
            SETItemPick();
        }
    }
    private void FixedUpdate()
    {
        //ShowItemUI();
        ShowSkillCoolTime();
    }

    /* --------------- 기능 함수 --------------- */
    public void ShowItemUI()
    {
        switch (itemManager.ItemCount)
        {
            case 0:
                item1.SetActive(false);
                item2.SetActive(false);
                item3.SetActive(false);
                break;
            case 1:
                item1.SetActive(true);
                item1.transform.GetChild(0).gameObject.SetActive(!isActiveItem[0]);
                item1.transform.GetChild(1).gameObject.SetActive(isActiveItem[0]);
                item2.SetActive(false);
                item3.SetActive(false);
                break;
            case 2:
                item1.SetActive(true);
                item1.transform.GetChild(0).gameObject.SetActive(!isActiveItem[0]);
                item1.transform.GetChild(1).gameObject.SetActive(isActiveItem[0]);
                item2.SetActive(true);
                item2.transform.GetChild(0).gameObject.SetActive(!isActiveItem[1]);
                item2.transform.GetChild(1).gameObject.SetActive(isActiveItem[1]);
                item3.SetActive(false);
                break;
            case 3:
                item1.SetActive(true);
                item1.transform.GetChild(0).gameObject.SetActive(!isActiveItem[0]);
                item1.transform.GetChild(1).gameObject.SetActive(isActiveItem[0]);
                item2.SetActive(true);
                item2.transform.GetChild(0).gameObject.SetActive(!isActiveItem[1]);
                item2.transform.GetChild(1).gameObject.SetActive(isActiveItem[1]);
                item3.SetActive(true);
                item3.transform.GetChild(0).gameObject.SetActive(!isActiveItem[2]);
                item3.transform.GetChild(1).gameObject.SetActive(isActiveItem[2]);
                break;
        }
    }

    private void ShowSkillCoolTime()
    {
        if (skillController.IsSkillA)
        {
            skillATimer += Time.deltaTime;
            skillAUI();
        }
        else
        {
            skillATimer = 0;
            skillA.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
        }

        if (skillController.IsSkillS)
        {
            skillSTimer += Time.deltaTime;
            skillSUI();
        }
        else
        {
            skillSTimer = 0;
            skillS.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
        }

        if (skillController.IsSkillD)
        {
            skillDTimer += Time.deltaTime;
            DSkillUI();
        }
        else
        {
            skillDTimer = 0;
            skillD.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
        }

        if (skillController.IsUltimateSkill)
        {
            ultimateSkillTimer += Time.deltaTime;
            UltimateSkillUI();
        }
        else
        {
            ultimateSkillTimer = 0;
            ultimitSkill.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
        }
    }

    /* --------------- 콜백 함수 --------------- */
    private void skillAUI()
    {
        skillA.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.gray;
        skillA.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = skillATimer / skillACoolTime;
    }

    private void skillSUI()
    {
        skillS.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.gray;
        skillS.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = skillSTimer / skillSCoolTime;
    }

    private void DSkillUI()
    {
        skillD.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.gray;
        skillD.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = skillDTimer / skillDCoolTime;
    }

    private void UltimateSkillUI()
    {
        ultimitSkill.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.gray;
        ultimitSkill.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = ultimateSkillTimer / skillManager.UltimateSkillCoolTime;
    }

    private void ItemPickCountSettings()
    {
        itemPickCount++;
        if (itemPickCount >= 3)
        {
            itemPickCount = 0;
        }
    }
    private void SETItemPick()
    {
        switch (itemPickCount)
        {
            case 0:
                itemPick1.SetActive(true);
                itemPick2.SetActive(false);
                itemPick3.SetActive(false);
                break;
            case 1:
                itemPick1.SetActive(false);
                itemPick2.SetActive(true);
                itemPick3.SetActive(false);
                break;
            case 2:
                itemPick1.SetActive(false);
                itemPick2.SetActive(false);
                itemPick3.SetActive(true);
                break;
        }
    }
    /* --------------- 콜백 함수 --------------- */
    public void SetHpUI()
    {

        hpUI.transform.GetChild(1).GetComponent<Image>().fillAmount = (float)playerMain.Hp / (float)playerMain.MaxHp;
        hpUI.transform.GetChild(2).GetComponent<TMP_Text>().text = playerMain.Hp + " / " + playerMain.MaxHp;
    }

    public void SetCoinUI()
    {
        coinUI.transform.GetChild(0).GetComponent<TMP_Text>().text = " : " + playerMain.Coin;
    }

    public void SetMoonRockUI()
    {
        moonRockUI.transform.GetChild(0).GetComponent<TMP_Text>().text = " : " + playerMain.MoonRock;
    }

    public void SetDashStackUI()
    {
        for (int i = 0; i < 3; i++)
        {
            dashStackUI.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int j = 0; j < playerMain.DashStack; j++)
        {
            dashStackUI.transform.GetChild(j).gameObject.SetActive(true);
        }
    }

    /* --------------- 콜백 함수 --------------- */
    public void SetActiveItemBool(int index, bool active)
    {
        isActiveItem[index] = active;
    }
    public void SetItemUI(int index, Sprite image)
    {
        switch (index)
        {
            case 0:
                item1.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = image;
                break;
            case 1:
                item2.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = image;
                break;
            case 2:
                item3.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = image;
                break;
        }
    }
}
