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
    private int maxHp;

    /* ---------------- 인스펙터 --------------- */
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

        HP_Gage_Control();
        DashStackUI_Control();
        CoinUI_Control();
        MoonRockUI_Control();
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
}
