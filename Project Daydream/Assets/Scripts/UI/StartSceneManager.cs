using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartSceneManager : MonoBehaviour
{
    /* ---------------- 인스펙터 --------------- */
    [Header("Score UI")]
    [SerializeField]
    private TMP_Text scoreUI;

    /* -------------- 이벤트 함수 -------------- */
    private void Start()
    {
        if (scoreUI == null)
            return;

        scoreUI.text = "Player Score : " + GameManager.instance.getGameClearScore();
    }

    /* ------------ UI 버튼 함수 ------------- */
    public void GameStartButton()
    {
        SceneManager.LoadScene("BaseCamp");
    }

    public void GameEndButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
