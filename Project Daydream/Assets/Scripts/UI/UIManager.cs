using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    /* ------------- 컴포넌트 변수 ------------- */
    private PlayerMain playerMain;
    private PlayerController playerController;


    //-------------아이템 관련 변수--------------------------------
    [Header("아이템 관련 테스트 변수")]
    [SerializeField, Range(0f, 3f)]
    private int itemCount; // 현재 들고 있는 아이템 수
    [SerializeField]
    private bool availableItem1; // 아이템1의 사용가능 유무
    [SerializeField]
    private bool availableItem2; // 아이템2의 사용가능 유무
    [SerializeField]
    private bool availableItem3; // 아이템3의 사용가능 유무

    private GameObject Item1;
    private GameObject Item2;
    private GameObject Item3;
    //-------------스킬 관련 변수--------------------------------
    private GameObject ASkill;
    private GameObject SSkill;
    private GameObject DSkill;
    private float aSkillTimer;
    private float sSkillTimer;
    private float dSkillTimer;
    private float ultimateSkillTimer;
    [Space(10f)]
    [Header("스킬 관련 테스트 변수")]
    [SerializeField]
    private bool isASkill; // 임시 A스킬 사용 유무 변수 
    [SerializeField]
    private bool isSSkill; // 임시 S스킬 사용 유무 변수 
    [SerializeField]
    private bool isDSkill; // 임시 D스킬 사용 유무 변수 
    [SerializeField]
    private bool isUltimateSkill; // 임시 궁스킬 사용 유무 변수 

    //임시 스킬 쿨타임 변수
    [SerializeField, Range(0f, 100f)]
    private float aSkillColltime = 3f;
    [SerializeField, Range(0f, 100f)]
    private float sSkillColltime = 3f;
    [SerializeField, Range(0f, 100f)]
    private float dSkillColltime = 3f;
    [SerializeField, Range(0f, 100f)]
    private float ultimateSkillColltime = 5f;

    /* ---------------- 인스펙터 --------------- */
    [Space(10f)]
    [Header("오브젝트 연결")]
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject HPUI;
    [SerializeField]
    private GameObject CoinUI;
    [SerializeField]
    private GameObject MoonRockUI;
    [SerializeField]
    private GameObject DashStackUI;
    [SerializeField]
    private GameObject UltimitSkill;
    [SerializeField]
    private GameObject Skill;
    [SerializeField]
    private GameObject Item;
    [SerializeField]
    private GameObject MiniMap;

    /* -------------- 싱글톤 변수 -------------- */
    public static UIManager instance = null;

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
        Item1 = Item.transform.GetChild(0).gameObject;
        Item2 = Item.transform.GetChild(1).gameObject;
        Item3 = Item.transform.GetChild(2).gameObject;

        ASkill = Skill.transform.GetChild(0).gameObject;
        SSkill = Skill.transform.GetChild(1).gameObject;
        DSkill = Skill.transform.GetChild(2).gameObject;

        HP_Gage_Control();
        DashStackUI_Control();
        CoinUI_Control();
        MoonRockUI_Control();
        MiniMap.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            MiniMap.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.M))
        {
            MiniMap.SetActive(false);
        }

    }
    private void FixedUpdate()
    {
        ShowItemUI();
        if (isASkill)
        {
            aSkillTimer += Time.deltaTime;
            ASkillUI();
            if (aSkillTimer >= aSkillColltime)
            {
                isASkill = false;
                aSkillTimer = 0;
                ASkill.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
                return;
            }
        }
        if (isSSkill)
        {
            sSkillTimer += Time.deltaTime;
            SSkillUI();
            if (sSkillTimer >= sSkillColltime)
            {
                isSSkill = false;
                sSkillTimer = 0;
                SSkill.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
                return;
            }
        }
        if (isDSkill)
        {
            dSkillTimer += Time.deltaTime;
            DSkillUI();
            if (dSkillTimer >= dSkillColltime)
            {
                isDSkill = false;
                dSkillTimer = 0;
                DSkill.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
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
                UltimitSkill.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
                return;
            }
        }
    }

    public void HP_Gage_Control()
    {

        HPUI.transform.GetChild(1).GetComponent<Image>().fillAmount = (float)playerMain.Hp / (float)playerMain.MaxHp;
        HPUI.transform.GetChild(2).GetComponent<TMP_Text>().text = playerMain.Hp + " / " + playerMain.MaxHp;
    }


    public void CoinUI_Control()
    {
        CoinUI.transform.GetChild(0).GetComponent<TMP_Text>().text = " : " + playerMain.Coin;
    }

    public void MoonRockUI_Control()
    {
        MoonRockUI.transform.GetChild(0).GetComponent<TMP_Text>().text = " : " + playerMain.MoonRock;
    }

    public void DashStackUI_Control()
    {
        for (int i = 0; i < 3; i++)
        {
            DashStackUI.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int j = 0; j < playerMain.DashStack; j++)
        {
            DashStackUI.transform.GetChild(j).gameObject.SetActive(true);
        }
    }

    public void ShowItemUI()
    {
        switch (itemCount)
        {
            case 0:
                Item1.SetActive(false);
                Item2.SetActive(false);
                Item3.SetActive(false);
                break;
            case 1:
                Item1.SetActive(true);
                Item1.transform.GetChild(0).gameObject.SetActive(!availableItem1);
                Item1.transform.GetChild(1).gameObject.SetActive(availableItem1);
                Item2.SetActive(false);
                Item3.SetActive(false);
                break;
            case 2:
                Item1.SetActive(true);
                Item1.transform.GetChild(0).gameObject.SetActive(!availableItem1);
                Item1.transform.GetChild(1).gameObject.SetActive(availableItem1);
                Item2.SetActive(true);
                Item2.transform.GetChild(0).gameObject.SetActive(!availableItem2);
                Item2.transform.GetChild(1).gameObject.SetActive(availableItem2);
                Item3.SetActive(false);
                break;
            case 3:
                Item1.SetActive(true);
                Item1.transform.GetChild(0).gameObject.SetActive(!availableItem1);
                Item1.transform.GetChild(1).gameObject.SetActive(availableItem1);
                Item2.SetActive(true);
                Item2.transform.GetChild(0).gameObject.SetActive(!availableItem2);
                Item2.transform.GetChild(1).gameObject.SetActive(availableItem2);
                Item3.SetActive(true);
                Item3.transform.GetChild(0).gameObject.SetActive(!availableItem3);
                Item3.transform.GetChild(1).gameObject.SetActive(availableItem3);
                break;
        }
    }

    public void ASkillUI()
    {
        ASkill.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.gray;
        ASkill.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = aSkillTimer / aSkillColltime;
    }
    public void SSkillUI()
    {
        SSkill.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.gray;
        SSkill.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = sSkillTimer / sSkillColltime;
    }
    public void DSkillUI()
    {
        DSkill.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.gray;
        DSkill.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = dSkillTimer / dSkillColltime;
    }
    public void UltimateSkillUI()
    {
        UltimitSkill.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.gray;
        UltimitSkill.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = ultimateSkillTimer / ultimateSkillColltime;
    }
}
