using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    /* ---------------- 인스펙터 ---------------- */
    [SerializeField] private string scene; // 씬 이름
    [SerializeField] private Image fadeImg; // 페이드 아웃에 사용할 이미지

    /* ---------------- 동작 변수 ---------------- */
    private float time = 0f; // 경과시간
    private float fadeTime = 1f; // 지속시간

    public void Fade() { StartCoroutine(FadeOutCoroutine()); } // 페이드 아웃 실행 함수

    public IEnumerator FadeOutCoroutine()
    {
        fadeImg.gameObject.SetActive(true); // 이미지 활성화
        Color alpha = fadeImg.color; // 색상값

        // 페이드 아웃
        while (alpha.a < fadeTime)
        {
            time += Time.deltaTime / fadeTime; // 시간 계산
            alpha.a = Mathf.Lerp(0f, 1f, time); // 알파값을 서서히 증가시킴(서서히 어두워짐)
            fadeImg.color = alpha; // 이미지의 색상 정보변경
            yield return null; // 한 프레임 대기
        }

        SceneManager.LoadScene(scene); // 씬 전환
    }
}