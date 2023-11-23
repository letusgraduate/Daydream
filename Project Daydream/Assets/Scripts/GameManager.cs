using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /* -------------- 싱글톤 변수 -------------- */
    public static GameManager instance = null;

    /* ------------- 컴포넌트 변수 ------------- */
    private PlayerMain playerMain;
    private PlayerController playerController;
    private DataManager dataManager;
    private SkillManager skillManager;
    private ItemManager itemManager;

    /* ---------------- 인스펙터 --------------- */
    [Header("플레이어 프리팹")]
    [SerializeField]
    private GameObject player;

    [Header("재화 프리팹")]
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private GameObject moonRockPrefab;

    [Header("포탈 프리팹")]
    [SerializeField]
    private GameObject stage1Portal;
    [SerializeField]
    private GameObject stage1BossPortal;
    [SerializeField]
    private GameObject stage2Portal;
    [SerializeField]
    private GameObject stage2BossPortal;
    [SerializeField]
    private GameObject coinPortal;
    [SerializeField]
    private GameObject healPortal;
    [SerializeField]
    private GameObject storePortal;

    [Header("월석")]
    [SerializeField, Range(0, 1000)]
    private int maxMoonRock = 1000;
    [SerializeField, Range(0, 100)]
    private int moonRock = 0;

    [Header("점수")]
    [SerializeField, Range(0, 9999)]
    private int maxPlayerScore = 9999;
    [SerializeField, Range(0, 9999)]
    private int playerScore = 0;

    [Header("스테이지 관련")]
    [SerializeField, Range(0, 8)]
    private int stageClearCount = 0;

    /* ---------------- 프로퍼티 --------------- */
    public int MoonRock
    {
        get { return moonRock; }
        set
        {
            if (value <= 0)
                moonRock = 0;
            else if (value > maxMoonRock)
                moonRock = maxMoonRock;
            else
                moonRock = value;

            UIManager.instance.SetMoonRockUI();
        }
    }

    public int PlayerScore
    {
        get { return playerScore; }
        set
        {
            if (value <= 0)
                playerScore = 0;
            else if (value > maxPlayerScore)
                playerScore = maxPlayerScore;
            else
                playerScore = value;
        }
    }

    public GameObject Player { get { return player; } }
    public SkillManager SkillManager { get { return skillManager; } }
    public ItemManager ItemManager { get { return itemManager; } }
    public GameObject CoinPrefab { get { return coinPrefab; } }
    public GameObject MoonRockPrefab { get { return moonRockPrefab; } }

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

        dataManager = GetComponentInChildren<DataManager>();
        skillManager = GetComponentInChildren<SkillManager>();
        itemManager = GetComponentInChildren<ItemManager>();
        playerMain = player.GetComponent<PlayerMain>();
    }

    /* --------------- 기능 함수 --------------- */
    public void GameOver()
    {
        dataManager.Save(); // 세이브 파일 저장 (월석 저장)
        PlayerScore += playerMain.Coin; // 남은 코인 점수로
        UIManager.instance.ShowGameScore();
        // 점수 출력
        Debug.Log("GameOver");
        Debug.Log("Player Score : " + PlayerScore);

        PlayerScore = 0;
        playerMain.Coin = 0;
        playerMain.Hp = 100;
    }

    IEnumerator ReStart()
    {
        yield return new WaitForSeconds(5f);

        // 현재 씬 재시작 (나중에 수정)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StageClear()
    {
        PlayerScore += 100;
        stageClearCount += 1;

        //SpawnPortal(stageClearCount);
    }

    public void SpawnPortal(Vector3 spawnPos)
    {
        int bonus = Random.Range(0, 3); // 보너스 맵 포탈 스폰 확률 1/3

        switch (stageClearCount)
        {
            case 0:
            case 1:
            case 2:
                // 스테이지 1 포탈
                Instantiate(stage1Portal, spawnPos, Quaternion.identity);
                if (bonus == 2)
                    BonusPortal(spawnPos);
                Debug.Log("stage1Portal");
                break;
            case 3:
                // 스테이지 1 보스 포탈
                Instantiate(stage1BossPortal, spawnPos, Quaternion.identity);
                Debug.Log("stage1BossPortal");
                break;
            case 4:
            case 5:
            case 6:
                // 스테이지 2 포탈
                Instantiate(stage2Portal, spawnPos, Quaternion.identity);
                if (bonus == 2)
                    BonusPortal(spawnPos);
                Debug.Log("stage2Portal");
                break;
            case 7:
                // 스테이지 2 보스 포탈
                Instantiate(stage2BossPortal, spawnPos, Quaternion.identity);
                Debug.Log("stage2BossPortal");
                break;
            default:
                break;
        }
    }

    private void BonusPortal(Vector3 spawnPos)
    {
        int rand = Random.Range(0, 3);

        Vector3 bonusPortalPos = new Vector3(spawnPos.x + 3, spawnPos.y, spawnPos.z);

        switch (rand)
        {
            case 0:
                Instantiate(coinPortal, bonusPortalPos, Quaternion.identity);
                Debug.Log("coinPortal");
                break;
            case 1:
                Instantiate(healPortal, bonusPortalPos, Quaternion.identity);
                Debug.Log("healPortal");
                break;
            case 2:
                Instantiate(storePortal, bonusPortalPos, Quaternion.identity);
                Debug.Log("storePortal");
                break;
            default:
                break;
        }
    }
}
