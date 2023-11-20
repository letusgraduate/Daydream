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


    private GameObject speed;
    private GameObject maxHP;
    private GameObject power;
    private GameObject coin;
    private GameObject dash;
    private GameObject item;

    /* --------------- 스킬 관련 --------------- */
    private float skillACoolTime;
    private float skillSCoolTime;
    private float skillDCoolTime;

    /* -------------- 아이템 관련 -------------- */
    private int itemSelect;

    /* -------------- 특성 관련 ---------------- */
    [SerializeField]
    private int speedNum = 2;
    private int maxHPNum = 2;
    private int powerNum = 2;
    private int coinNum = 2;
    private int dashNum = 2;
    private int itemNum = 2;

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

        skillA = normalSkill.transform.GetChild(0).gameObject;
        skillS = normalSkill.transform.GetChild(1).gameObject;
        skillD = normalSkill.transform.GetChild(2).gameObject;

        item1 = itemUI.transform.GetChild(0).gameObject;
        item2 = itemUI.transform.GetChild(1).gameObject;
        item3 = itemUI.transform.GetChild(2).gameObject;

        selectItem1 = item1.transform.GetChild(0).GetChild(0).gameObject;
        selectItem2 = item2.transform.GetChild(0).GetChild(0).gameObject;
        selectItem3 = item3.transform.GetChild(0).GetChild(0).gameObject;

        speed = traitUI.transform.GetChild(4).transform.GetChild(0).gameObject;
        maxHP = traitUI.transform.GetChild(4).transform.GetChild(1).gameObject;
        power = traitUI.transform.GetChild(4).transform.GetChild(2).gameObject;
        coin = traitUI.transform.GetChild(4).transform.GetChild(3).gameObject;
        dash = traitUI.transform.GetChild(4).transform.GetChild(4).gameObject;
        item = traitUI.transform.GetChild(4).transform.GetChild(5).gameObject;
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
        {
            ShowSettingUI();
        }
        else
        {
            CloseSettingUI();
        }
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
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }
    public void SetVolumeUI()
    {
        sound.GetComponent<AudioSource>().volume = volumeUI.transform.GetChild(3).GetComponent<Slider>().value;
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
    /* ----------- 플레이어 스코어 관현 함수 ------------ */
    public void GameReStartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameScoreUI.SetActive(false);
    }

    public void ShowGameScore()
    {
        gameScoreUI.SetActive(true);
        gameScoreUI.transform.GetChild(0).GetComponent<TMP_Text>().text = "Player Score : " + GameManager.instance.PlayerScore;
    }
    /* ----------- 특성 관현 함수 ------------ */

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
        if (speedNum == speed.transform.childCount)
        {
            return;
        }

        speed.transform.GetChild(speedNum).gameObject.GetComponent<Button>().interactable = false;
        speed.transform.GetChild(speedNum).gameObject.GetComponent<Image>().color = Color.yellow;
        if (speedNum + 1 < speed.transform.childCount)
            speed.transform.GetChild(speedNum + 1).gameObject.GetComponent<Button>().interactable = true;

        speedNum++;

        trait.GetComponent<TraitController>().SpeedUP();
    }

    public void MaxHPUpgrade()
    {
        if (maxHPNum == maxHP.transform.childCount)
        {
            return;
        }

        maxHP.transform.GetChild(maxHPNum).gameObject.GetComponent<Button>().interactable = false;
        maxHP.transform.GetChild(maxHPNum).gameObject.GetComponent<Image>().color = Color.yellow;
        if (maxHPNum + 1 < maxHP.transform.childCount)
            maxHP.transform.GetChild(maxHPNum + 1).gameObject.GetComponent<Button>().interactable = true;

        maxHPNum++;

        trait.GetComponent<TraitController>().MaxHpUP();
    }

    public void PowerUpgrade()
    {
        if (powerNum == power.transform.childCount)
        {
            return;
        }

        power.transform.GetChild(powerNum).gameObject.GetComponent<Button>().interactable = false;
        power.transform.GetChild(powerNum).gameObject.GetComponent<Image>().color = Color.yellow;
        if (powerNum + 1 < power.transform.childCount)
            power.transform.GetChild(powerNum + 1).gameObject.GetComponent<Button>().interactable = true;

        powerNum++;

        trait.GetComponent<TraitController>().PowerUP();
    }

    public void CoinUpgrade()
    {
        if (coinNum == coin.transform.childCount)
        {
            return;
        }

        coin.transform.GetChild(coinNum).gameObject.GetComponent<Button>().interactable = false;
        coin.transform.GetChild(coinNum).gameObject.GetComponent<Image>().color = Color.yellow;
        if (coinNum + 1 < coin.transform.childCount)
            coin.transform.GetChild(coinNum + 1).gameObject.GetComponent<Button>().interactable = true;

        coinNum++;

        trait.GetComponent<TraitController>().CoinUP();
    }

    public void DashUpgrade()
    {
        if (dashNum == dash.transform.childCount)
        {
            return;
        }

        dash.transform.GetChild(dashNum).gameObject.GetComponent<Button>().interactable = false;
        dash.transform.GetChild(dashNum).gameObject.GetComponent<Image>().color = Color.yellow;
        if (dashNum + 1 < dash.transform.childCount)
            dash.transform.GetChild(dashNum + 1).gameObject.GetComponent<Button>().interactable = true;

        dashNum++;

        trait.GetComponent<TraitController>().DashUP();
    }

    public void ItemUpgrade()
    {
        if (itemNum == item.transform.childCount)
        {
            return;
        }

        item.transform.GetChild(itemNum).gameObject.GetComponent<Button>().interactable = false;
        item.transform.GetChild(itemNum).gameObject.GetComponent<Image>().color = Color.yellow;
        if (itemNum + 1 < item.transform.childCount)
            item.transform.GetChild(itemNum + 1).gameObject.GetComponent<Button>().interactable = true;

        itemNum++;

        trait.GetComponent<TraitController>().ItemUP();
    }
}
