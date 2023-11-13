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
    private PlayerMain playerMain;
    private PlayerController playerController;
    private SkillController skillController;
    private SkillManager skillManager;

    private GameObject item1;
    private GameObject item2;
    private GameObject item3;

    private GameObject skillA;
    private GameObject skillS;
    private GameObject skillD;

    /* ---------------- 인스펙터 --------------- */
    private float skillATimer;
    private float skillSTimer;
    private float skillDTimer;
    private float ultimateSkillTimer;

    /* ---------------- 인스펙터 --------------- */
    [Space(10f)]
    [Header("오브젝트 연결")]
    [SerializeField]
    private GameObject player;
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
    private GameObject item;
    [SerializeField]
    private GameObject skillManagerObject;
    //[SerializeField]
    //private GameObject miniMap;

    [Header("아이템 관련 테스트 변수")]
    [SerializeField, Range(0f, 3f)]
    private int itemCount; // 현재 들고 있는 아이템 수
    [SerializeField]
    private bool isActiveItem1; // 아이템1의 사용가능 유무
    [SerializeField]
    private bool isActiveItem2; // 아이템2의 사용가능 유무
    [SerializeField]
    private bool isActiveItem3; // 아이템3의 사용가능 유무

    [Header("스킬 쿨타임")]
    [SerializeField, Range(0f, 100f)]
    private float skillACoolTime;
    [SerializeField, Range(0f, 100f)]
    private float skillSCoolTime;
    [SerializeField, Range(0f, 100f)]
    private float skillDCoolTime;
    /* -------------- 이벤트 함수 -------------- */
    void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
            DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }

        playerMain = player.GetComponent<PlayerMain>();
        playerController = player.GetComponent<PlayerController>();
        skillController = player.GetComponent<SkillController>();
        skillManager = skillManagerObject.GetComponent<SkillManager>();

        item1 = item.transform.GetChild(0).gameObject;
        item2 = item.transform.GetChild(1).gameObject;
        item3 = item.transform.GetChild(2).gameObject;

        skillA = normalSkill.transform.GetChild(0).gameObject;
        skillS = normalSkill.transform.GetChild(1).gameObject;
        skillD = normalSkill.transform.GetChild(2).gameObject;

        skillACoolTime = skillManager.SkillACoolTime;
        skillSCoolTime = skillManager.SkillSCoolTime;
        skillDCoolTime = skillManager.SkillDCoolTime;

        SetHpUI();
        SetDashStackUI();
        SetCoinUI();
        SetMoonRockUI();
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

    private void FixedUpdate()
    {
        ShowItemUI();

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

    /* --------------- 기능 함수 --------------- */
    public void ShowItemUI()
    {
        switch (itemCount)
        {
            case 0:
                item1.SetActive(false);
                item2.SetActive(false);
                item3.SetActive(false);
                break;
            case 1:
                item1.SetActive(true);
                item1.transform.GetChild(0).gameObject.SetActive(!isActiveItem1);
                item1.transform.GetChild(1).gameObject.SetActive(isActiveItem1);
                item2.SetActive(false);
                item3.SetActive(false);
                break;
            case 2:
                item1.SetActive(true);
                item1.transform.GetChild(0).gameObject.SetActive(!isActiveItem1);
                item1.transform.GetChild(1).gameObject.SetActive(isActiveItem1);
                item2.SetActive(true);
                item2.transform.GetChild(0).gameObject.SetActive(!isActiveItem2);
                item2.transform.GetChild(1).gameObject.SetActive(isActiveItem2);
                item3.SetActive(false);
                break;
            case 3:
                item1.SetActive(true);
                item1.transform.GetChild(0).gameObject.SetActive(!isActiveItem1);
                item1.transform.GetChild(1).gameObject.SetActive(isActiveItem1);
                item2.SetActive(true);
                item2.transform.GetChild(0).gameObject.SetActive(!isActiveItem2);
                item2.transform.GetChild(1).gameObject.SetActive(isActiveItem2);
                item3.SetActive(true);
                item3.transform.GetChild(0).gameObject.SetActive(!isActiveItem3);
                item3.transform.GetChild(1).gameObject.SetActive(isActiveItem3);
                break;
        }
    }

    public void skillAUI()
    {
        skillA.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.gray;
        skillA.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = skillATimer / skillACoolTime;
    }

    public void skillSUI()
    {
        skillS.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.gray;
        skillS.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = skillSTimer / skillSCoolTime;
    }

    public void DSkillUI()
    {
        skillD.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.gray;
        skillD.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = skillDTimer / skillDCoolTime;
    }

    public void UltimateSkillUI()
    {
        ultimitSkill.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.gray;
        ultimitSkill.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = ultimateSkillTimer / skillManager.UltimateSkillCoolTime;
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
}
