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

    /* ---------------- 인스펙터 --------------- */
    [Header("오브젝트 연결")]
    [SerializeField]
    private GameObject player;

    [Header("설정")]
    [SerializeField, Range(0, 1000)]
    private int maxMoonRock = 1000;
    [SerializeField, Range(0, 100)]
    private int moonRock = 0;

    [Header("포탈")]
    [SerializeField]
    private GameObject normalPortalPrefab;
    [SerializeField]
    private GameObject bonusPortalPrefab;

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
        }
    }

    public GameObject Player { get { return player; } }

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

        dataManager = GetComponent<DataManager>();

        playerMain = player.GetComponent<PlayerMain>();
        playerController = player.GetComponent<PlayerController>();
    }

    /* --------------- 기능 함수 --------------- */
    public void GameOver()
    {
        dataManager.Save(); // 세이브 파일 저장

        StartCoroutine(ReStart());
    }

    IEnumerator ReStart()
    {
        yield return new WaitForSeconds(5f);

        // 현재 씬 재시작 (나중에 수정)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StageClear()
    {
        Instantiate(normalPortalPrefab, new Vector3(-4, 0, 0), Quaternion.identity); // 나중에 생성 위치 바꾸겠음

        int probability = Random.Range(0, 101); // 보너스 맵 포탈 스폰 확률

        if (probability <= 60)
        {
            Instantiate(bonusPortalPrefab, new Vector3(4, 0, 0), Quaternion.identity);
        }
    }
}