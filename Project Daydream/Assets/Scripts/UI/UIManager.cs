using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    private GameObject selectItem1;
    private GameObject selectItem2;
    private GameObject selectItem3;

    private GameObject skillA;
    private GameObject skillS;
    private GameObject skillD;

    private GameObject speedTrait;
    private GameObject maxHPTrait;
    private GameObject powerTrait;
    private GameObject skillTrait;
    private GameObject dashTrait;
    private GameObject itemTrait;

    /* --------------- 스킬 관련 --------------- */
    private float skillACoolTime;
    private float skillSCoolTime;
    private float skillDCoolTime;

    /* -------------- 아이템 관련 -------------- */
    private int itemSelect;

    /* -------------- 특성 관련 ---------------- */
    private int speedLevel = 2;
    private int maxHPLevel = 2;
    private int powerLevel = 2;
    private int skillLevel = 2;
    private int dashLevel = 2;
    private int itemLevel = 2;

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
    private GameObject ultimateSkill;
    [SerializeField]
    private GameObject normalSkill;
    [SerializeField]
    private GameObject itemUI;
    [SerializeField]
    private GameObject subtitleUI;
    [SerializeField]
    private GameObject settingUI;
    [SerializeField]
    private GameObject SoundToggleUI;
    [SerializeField]
    private GameObject volumeUI;
    [SerializeField]
    private GameObject gameQuitUI;
    [SerializeField]
    private GameObject gameScoreUI;
    [SerializeField]
    private GameObject traitUI;
    [SerializeField]
    private GameObject trait;
    [SerializeField]
    private GameObject sound;
    //[SerializeField]
    //private GameObject miniMap;

    [Header("아이템 초기 이미지 정보")]
    [SerializeField]
    protected Sprite initItemImage;

    /* ---------------- 프로퍼티 --------------- */
    public int ItemSelect { get { return itemSelect; } }
    public GameObject SubtitleUI { get { return subtitleUI; } }

    public int SpeedLevel { get { return speedLevel; } set { speedLevel = value; } }
    public int MaxHPLevel { get { return maxHPLevel; } set { maxHPLevel = value; } }
    public int PowerLevel { get { return powerLevel; } set { powerLevel = value; } }
    public int SkillLevel { get { return skillLevel; } set { skillLevel = value; } }
    public int DashLevel { get { return dashLevel; } set { dashLevel = value; } }
    public int ItemLevel { get { return itemLevel; } set { itemLevel = value; } }

    /* -------------- 이벤트 함수 -------------- */
    private void Awake()
    {
        if (instance == null) // instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; // 내자신을 instance로 넣어줍니다.
        }
        else
        {
            Destroy(UIManager.instance.gameObject);

            instance = this; // 내자신을 instance로 넣어줍니다.
        }
        //else
        //{
        //    if (instance != this) // instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
        //        Destroy(this.gameObject); // 둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        //}

        skillA = normalSkill.transform.GetChild(0).gameObject;
        skillS = normalSkill.transform.GetChild(1).gameObject;
        skillD = normalSkill.transform.GetChild(2).gameObject;

        item1 = itemUI.transform.GetChild(0).gameObject;
        item2 = itemUI.transform.GetChild(1).gameObject;
        item3 = itemUI.transform.GetChild(2).gameObject;

        selectItem1 = item1.transform.GetChild(0).GetChild(0).gameObject;
        selectItem2 = item2.transform.GetChild(0).GetChild(0).gameObject;
        selectItem3 = item3.transform.GetChild(0).GetChild(0).gameObject;

        speedTrait = traitUI.transform.GetChild(4).transform.GetChild(0).gameObject;
        maxHPTrait = traitUI.transform.GetChild(4).transform.GetChild(1).gameObject;
        powerTrait = traitUI.transform.GetChild(4).transform.GetChild(2).gameObject;
        skillTrait = traitUI.transform.GetChild(4).transform.GetChild(3).gameObject;
        dashTrait = traitUI.transform.GetChild(4).transform.GetChild(4).gameObject;
        itemTrait = traitUI.transform.GetChild(4).transform.GetChild(5).gameObject;
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
        gameQuitUI.SetActive(false);
        gameScoreUI.SetActive(false);

        SetHpUI();
        SetDashStackUI();
        SetCoinUI();
        SetMoonRockUI();
        ShowItemUI();
        ItemSelectUI();
        CloseSettingUI();
        CloseTraitUI();
        SetSkillImageUI();
        //miniMap.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowAndCloseSettingUI();
        }
    }

    /* ------------ UI 콜백 함수 ------------- */
    public void SetHpUI()
    {
        hpUI.GetComponent<Slider>().maxValue = (float)playerMain.MaxHp;
        hpUI.GetComponent<Slider>().value = (float)playerMain.Hp;
        hpUI.transform.GetChild(4).GetComponent<TMP_Text>().text = playerMain.Hp + " / " + playerMain.MaxHp;
    }

    public void SetCoinUI()
    {
        coinUI.transform.GetChild(0).GetComponent<TMP_Text>().text = " : " + playerMain.Coin;
    }

    public void SetMoonRockUI()
    {
        moonRockUI.transform.GetChild(0).GetComponent<TMP_Text>().text = " : " + GameManager.instance.MoonRock;
    }

    public void SetDashStackUI()
    {
        if (playerMain == null)
            return;

        for (int i = 0; i < 3; i++)
        {
            dashStackUI.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int j = 0; j < playerMain.DashStack; j++)
        {
            dashStackUI.transform.GetChild(j).gameObject.SetActive(true);
        }
    }

    /* --------------- 스킬 관련 --------------- */
    public void SetSkillACoolTimeUI()
    {
        StartCoroutine(SkillCoolTimeUI(skillA, skillACoolTime));
    }

    public void SetSkillSCoolTimeUI()
    {
        StartCoroutine(SkillCoolTimeUI(skillS, skillSCoolTime));
    }

    public void SetSkillDCoolTimeUI()
    {
        StartCoroutine(SkillCoolTimeUI(skillD, skillDCoolTime));
    }

    public void SetUltimateSkillCoolTimeUI()
    {
        StartCoroutine(SkillCoolTimeUI(ultimateSkill, skillManager.UltimateSkillCoolTime));
    }

    private IEnumerator SkillCoolTimeUI(GameObject skill, float coolTime)
    {
        Image skillImage = skill.transform.GetChild(0).GetComponent<Image>();
        float timer = 0f;

        while (timer < coolTime)
        {
            timer += Time.deltaTime;

            skillImage.color = Color.gray;
            skillImage.fillAmount = timer / coolTime;

            yield return null; // 1 프레임 지연
        }

        skillImage.color = Color.white;
    }

    private void SetSkillImageUI()
    {
        skillD.transform.GetChild(0).GetComponent<Image>().sprite = skillManager.SkillImages(0);
        skillS.transform.GetChild(0).GetComponent<Image>().sprite = skillManager.SkillImages(1);
        skillS.transform.GetChild(0).GetComponent<Image>().color = Color.black;
        skillA.transform.GetChild(0).GetComponent<Image>().sprite = skillManager.SkillImages(2);
        skillA.transform.GetChild(0).GetComponent<Image>().color = Color.black;
    }

    public void SetSkillImageColorUI(int num)
    {
        if (num == 1)
        {
            skillA.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        }
        else
        {
            skillS.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        }
    }

    public void SetUltimateSkillImageUI()
    {
        ultimateSkill.transform.GetChild(0).GetComponent<Image>().sprite = skillManager.UltimateSkillImage;
    }

    /* -------------- 아이템 관련 -------------- */
    public void ShowItemUI()
    {
        item1.SetActive(true);
        item1.transform.GetChild(0).GetChild(1).gameObject.SetActive(itemManager.GetIsActiveItem(0));
        item2.SetActive(true);
        item2.transform.GetChild(0).GetChild(1).gameObject.SetActive(itemManager.GetIsActiveItem(1));
        item3.SetActive(true);
        item3.transform.GetChild(0).GetChild(1).gameObject.SetActive(itemManager.GetIsActiveItem(2));
        SetItemMaxUI();
    }

    public void ItemSelectUI()
    {
        switch (itemSelect)
        {
            case 0:
                selectItem1.SetActive(true);
                selectItem2.SetActive(false);
                selectItem3.SetActive(false);
                break;
            case 1:
                selectItem1.SetActive(false);
                selectItem2.SetActive(true);
                selectItem3.SetActive(false);
                break;
            case 2:
                selectItem1.SetActive(false);
                selectItem2.SetActive(false);
                selectItem3.SetActive(true);
                break;
            default:
                break;
        }
    }

    /* ----------- 아이템 콜백 함수 ------------ */
    public void SetItemUI()
    {
        switch (itemManager.ItemStock)
        {
            case 0:
                item1.transform.GetChild(1).GetComponent<Image>().sprite = initItemImage;
                item2.transform.GetChild(1).GetComponent<Image>().sprite = initItemImage;
                item3.transform.GetChild(1).GetComponent<Image>().sprite = initItemImage;
                break;
            case 1:
                item1.transform.GetChild(1).GetComponent<Image>().sprite = itemManager.GetItemImages(0);
                item2.transform.GetChild(1).GetComponent<Image>().sprite = initItemImage;
                item3.transform.GetChild(1).GetComponent<Image>().sprite = initItemImage;
                break;
            case 2:
                item1.transform.GetChild(1).GetComponent<Image>().sprite = itemManager.GetItemImages(0);
                item2.transform.GetChild(1).GetComponent<Image>().sprite = itemManager.GetItemImages(1);
                item3.transform.GetChild(1).GetComponent<Image>().sprite = initItemImage;
                break;
            case 3:
                item1.transform.GetChild(1).GetComponent<Image>().sprite = itemManager.GetItemImages(0);
                item2.transform.GetChild(1).GetComponent<Image>().sprite = itemManager.GetItemImages(1);
                item3.transform.GetChild(1).GetComponent<Image>().sprite = itemManager.GetItemImages(2);
                break;
        }
    }

    public void SetItemMaxUI()
    {
        if (itemManager == null)
            return;

        switch (itemManager.MaxItemCount)
        {
            case 1:
                item1.SetActive(true);
                item2.SetActive(false);
                item3.SetActive(false);
                break;
            case 2:
                item1.SetActive(true);
                item2.SetActive(true);
                item3.SetActive(false);
                break;
            case 3:
                item1.SetActive(true);
                item2.SetActive(true);
                item3.SetActive(true);
                break;
        }
    }

    public void SetItemSelect()
    {
        itemSelect++;

        if (itemSelect >= itemManager.ItemStock)
            itemSelect = 0;

        ItemSelectUI();
    }

    /* ----------- 설정창 관련 함수 ------------ */
    public void ShowAndCloseSettingUI()
    {
        if (Time.timeScale == 1)
            ShowSettingUI();
        else
            CloseSettingUI();
    }

    public void ShowSettingUI()
    {
        settingUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseSettingUI()
    {
        settingUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void SoundOnOffSetting()
    {
        if (AudioListener.volume == 0)
            AudioListener.volume = volumeUI.transform.GetChild(3).GetComponent<Slider>().value;
        else
            AudioListener.volume = 0;
    }

    public void SetVolumeUI()
    {
        //sound.GetComponent<AudioSource>().volume = volumeUI.transform.GetChild(3).GetComponent<Slider>().value;
        AudioListener.volume = volumeUI.transform.GetChild(3).GetComponent<Slider>().value;

        if (AudioListener.volume == 0)
        {
            SoundToggleUI.transform.GetChild(0).gameObject.SetActive(false);
            SoundToggleUI.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            //SoundToggleUI.transform.GetChild(0).gameObject.name;
            SoundToggleUI.transform.GetChild(0).gameObject.SetActive(true);
            SoundToggleUI.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void SetGemeQuitUI()
    {
        if (gameQuitUI.activeSelf == false)
        {
            gameQuitUI.SetActive(true);
            CloseSettingUI();
        }
        else
        {
            gameQuitUI.SetActive(false);
        }
    }

    public void GameEndButton()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    /* ------- 플레이어 스코어 관현 함수 ------- */
    public void GameReStartButton()
    {
        Destroy(player);
        SceneManager.LoadScene("BaseCamp");
        //gameScoreUI.SetActive(false);
        //GameManager.instance.PlayerPosReset();
    }

    public void ShowGameScore()
    {
        gameScoreUI.SetActive(true);
        gameScoreUI.transform.GetChild(0).GetComponent<TMP_Text>().text = "Player Score : " + GameManager.instance.PlayerScore;
    }

    /* ------------ 특성 관련 함수 ------------- */
    public void ShowTraitUI()
    {
        traitUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseTraitUI()
    {
        traitUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void SpeedUpgrade()
    {
        if (speedLevel == speedTrait.transform.childCount || GameManager.instance.MoonRock < 1)
            return;

        GameManager.instance.MoonRock--;

        speedTrait.transform.GetChild(speedLevel).gameObject.GetComponent<Button>().interactable = false;
        speedTrait.transform.GetChild(speedLevel).gameObject.GetComponent<Image>().color = Color.yellow;

        if (speedLevel + 1 < speedTrait.transform.childCount)
            speedTrait.transform.GetChild(speedLevel + 1).gameObject.GetComponent<Button>().interactable = true;

        SpeedLevel++;

        trait.GetComponent<TraitController>().SpeedUP();
    }

    public void ForSpeedUpgrade(int num)
    {
        if (num == 2)
            return;

        for (int i = 2; i < num; i++)
        {
            speedTrait.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = false;
            speedTrait.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.yellow;

            if (i + 1 < speedTrait.transform.childCount)
                speedTrait.transform.GetChild(i + 1).gameObject.GetComponent<Button>().interactable = true;

            SpeedLevel++;

            trait.GetComponent<TraitController>().SpeedUP();
        }
    }

    public void MaxHPUpgrade()
    {
        if (maxHPLevel == maxHPTrait.transform.childCount || GameManager.instance.MoonRock < 1)
            return;

        GameManager.instance.MoonRock--;

        maxHPTrait.transform.GetChild(maxHPLevel).gameObject.GetComponent<Button>().interactable = false;
        maxHPTrait.transform.GetChild(maxHPLevel).gameObject.GetComponent<Image>().color = Color.yellow;

        if (maxHPLevel + 1 < maxHPTrait.transform.childCount)
            maxHPTrait.transform.GetChild(maxHPLevel + 1).gameObject.GetComponent<Button>().interactable = true;

        maxHPLevel++;

        trait.GetComponent<TraitController>().MaxHpUP();
    }

    public void ForMaxHPUpgrade(int num)
    {
        if (num == 2)
            return;

        for (int i = 2; i < num; i++)
        {
            maxHPTrait.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = false;
            maxHPTrait.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.yellow;

            if (i + 1 < maxHPTrait.transform.childCount)
                maxHPTrait.transform.GetChild(i + 1).gameObject.GetComponent<Button>().interactable = true;

            MaxHPLevel++;

            trait.GetComponent<TraitController>().MaxHpUP();
        }
    }

    public void PowerUpgrade()
    {
        if (powerLevel == powerTrait.transform.childCount || GameManager.instance.MoonRock < 1)
            return;

        GameManager.instance.MoonRock--;

        powerTrait.transform.GetChild(powerLevel).gameObject.GetComponent<Button>().interactable = false;
        powerTrait.transform.GetChild(powerLevel).gameObject.GetComponent<Image>().color = Color.yellow;

        if (powerLevel + 1 < powerTrait.transform.childCount)
            powerTrait.transform.GetChild(powerLevel + 1).gameObject.GetComponent<Button>().interactable = true;

        powerLevel++;

        trait.GetComponent<TraitController>().PowerUP();
    }

    public void ForPowerUpgrade(int num)
    {
        if (num == 2)
            return;

        for (int i = 2; i < num; i++)
        {
            powerTrait.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = false;
            powerTrait.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.yellow;

            if (i + 1 < powerTrait.transform.childCount)
                powerTrait.transform.GetChild(i + 1).gameObject.GetComponent<Button>().interactable = true;

            PowerLevel++;

            trait.GetComponent<TraitController>().PowerUP();
        }
    }

    public void SkillUpgrade()
    {
        if (skillLevel == skillTrait.transform.childCount || GameManager.instance.MoonRock < 1)
            return;

        GameManager.instance.MoonRock--;

        skillTrait.transform.GetChild(skillLevel).gameObject.GetComponent<Button>().interactable = false;
        skillTrait.transform.GetChild(skillLevel).gameObject.GetComponent<Image>().color = Color.yellow;

        if (skillLevel + 1 < skillTrait.transform.childCount)
            skillTrait.transform.GetChild(skillLevel + 1).gameObject.GetComponent<Button>().interactable = true;

        skillLevel++;

        trait.GetComponent<TraitController>().SkillUP();
    }

    public void ForSkillUpgrade(int num)
    {
        if (num == 2)
            return;

        for (int i = 2; i < num; i++)
        {
            skillTrait.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = false;
            skillTrait.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.yellow;

            if (i + 1 < skillTrait.transform.childCount)
                skillTrait.transform.GetChild(i + 1).gameObject.GetComponent<Button>().interactable = true;

            SkillLevel++;

            trait.GetComponent<TraitController>().SkillUP();
        }
    }

    public void DashUpgrade()
    {
        if (dashLevel == dashTrait.transform.childCount || GameManager.instance.MoonRock < 1)
            return;

        GameManager.instance.MoonRock--;

        dashTrait.transform.GetChild(dashLevel).gameObject.GetComponent<Button>().interactable = false;
        dashTrait.transform.GetChild(dashLevel).gameObject.GetComponent<Image>().color = Color.yellow;

        if (dashLevel + 1 < dashTrait.transform.childCount)
            dashTrait.transform.GetChild(dashLevel + 1).gameObject.GetComponent<Button>().interactable = true;

        dashLevel++;

        trait.GetComponent<TraitController>().DashUP();
    }

    public void ForDashUpgrade(int num)
    {
        if (num == 2)
            return;

        for (int i = 2; i < num; i++)
        {
            Debug.Log(num);
            dashTrait.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = false;
            dashTrait.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.yellow;

            if (i + 1 < dashTrait.transform.childCount)
                dashTrait.transform.GetChild(i + 1).gameObject.GetComponent<Button>().interactable = true;

            DashLevel++;

            trait.GetComponent<TraitController>().DashUP();
        }
    }

    public void ItemUpgrade()
    {
        if (itemLevel == itemTrait.transform.childCount || GameManager.instance.MoonRock < 1)
            return;

        GameManager.instance.MoonRock--;

        itemTrait.transform.GetChild(itemLevel).gameObject.GetComponent<Button>().interactable = false;
        itemTrait.transform.GetChild(itemLevel).gameObject.GetComponent<Image>().color = Color.yellow;

        if (itemLevel + 1 < itemTrait.transform.childCount)
            itemTrait.transform.GetChild(itemLevel + 1).gameObject.GetComponent<Button>().interactable = true;

        itemLevel++;

        trait.GetComponent<TraitController>().ItemUP();
    }

    public void ForItemUpgrade(int num)
    {
        if (num == 2)
            return;

        for (int i = 2; i < num; i++)
        {
            itemTrait.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = false;
            itemTrait.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.yellow;

            if (i + 1 < itemTrait.transform.childCount)
                itemTrait.transform.GetChild(i + 1).gameObject.GetComponent<Button>().interactable = true;

            ItemLevel++;

            trait.GetComponent<TraitController>().ItemUP();
        }
    }
}
