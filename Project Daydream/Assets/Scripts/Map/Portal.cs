using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    /* ---------------- 인스펙터 --------------- */
    [Header("Scene Name")]
    [SerializeField]
    private string SceneName;

    /* -------------- 이벤트 함수 -------------- */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UIManager.instance.SubtitleUI.GetComponent<Text>().text = "";
            SceneManager.LoadScene(SceneName);
            GameManager.instance.PlayerPosReset();
        }
    }
}