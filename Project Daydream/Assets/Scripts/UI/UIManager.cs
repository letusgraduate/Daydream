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

    private GameObject item1;
    private GameObject item2;
    private GameObject item3;

    private GameObject aSkill;
    private GameObject sSkill;
    private GameObject dSkill;

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
    private float SkillACoolTime = 3f;
    [SerializeField, Range(0f, 100f)]
    private float SkillSCoolTime = 3f;
    [SerializeField, Range(0f, 100f)]
    private float SkillDCoolTime = 3f;
    [SerializeField, Range(0f, 100f)]
    private float ultimateSkillColltime = 5f;

    [Header("스킬 관련 테스트 변수")]
    [SerializeField]
    private bool isSkillA; // 임시 A스킬 사용 유무 변수 
    [SerializeField]
    private bool isSkillS; // 임시 S스킬 사용 유무 변수 
    [SerializeField]
    private bool isSkillD; // 임시 D스킬 사용 유무 변수 
    [SerializeField]
    private bool isUltimateSkill; // 임시 궁스킬 사용 유무 변수 

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
        item1 = item.transform.GetChild(0).gameObject;
        item2 = item.transform.GetChild(1).gameObject;
        item3 = item.transform.GetChild(2).gameObject;

        aSkill = normalSkill.transform.GetChild(0).gameObject;
        sSkill = normalSkill.transform.GetChild(1).gameObject;
        dSkill = normalSkill.transform.GetChild(2).gameObject;

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

        if (isSkillA)
        {
            skillATimer += Time.deltaTime;
            ASkillUI();
            if (skillATimer >= SkillACoolTime)
            {
                isSkillA = false;
                skillATimer = 0;
                aSkill.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
                return;
            }
        }
        if (isSkillS)
        {
            skillSTimer += Time.deltaTime;
            SSkillUI();
            if (skillSTimer >= SkillSCoolTime)
            {
                isSkillS = false;
                skillSTimer = 0;
                sSkill.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
                return;
            }
        }
        if (isSkillD)
        {
            skillDTimer += Time.deltaTime;
            DSkillUI();
            if (skillDTimer >= SkillDCoolTime)
            {
                isSkillD = false;
                skillDTimer = 0;
                dSkill.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
                return;
            }
        }
        if (isUltimateSkill)
        {
            ultimateSkillTimer += Time.deltaTime;
            UltimateSkillUI();
            if (ultimateSkillTimer >= ultimateSkillColltime)
            {
                isUltimateSkill = false;
                ultimateSkillTimer = 0;
                ultimitSkill.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
                return;
            }
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

    public void ASkillUI()
    {
        aSkill.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.gray;
        aSkill.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = skillATimer / SkillACoolTime;
    }

    public void SSkillUI()
    {
        sSkill.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.gray;
        sSkill.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = skillSTimer / SkillSCoolTime;
    }

    public void DSkillUI()
    {
        dSkill.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.gray;
        dSkill.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = skillDTimer / SkillDCoolTime;
    }

    public void UltimateSkillUI()
    {
        ultimitSkill.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.gray;
        ultimitSkill.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = ultimateSkillTimer / ultimateSkillColltime;
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
