using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    /* ---------------- 인스펙터 ---------------- */
    [SerializeField] private Image fadeImg; // 페이드 인에 사용할 이미지

    /* ---------------- 동작 변수 ---------------- */
    private float time = 0f; // 경과시간
    private float fadeTime = 1f; // 지속시간

    private void Start() { StartCoroutine(FadeInCoroutine()); } // 씬 시작하면서 페이드 인

    public IEnumerator FadeInCoroutine()
    {
        fadeImg.gameObject.SetActive(true); // 페이드 이미지 활성화
        Color alpha = fadeImg.color; // 이미지의 색상 정보 가져오기

        // 페이드 인 (서서히 투명해짐)
        while (alpha.a > 0f) // 이미지의 알파값이 0보다 크면 반복
        {
            time += Time.deltaTime / fadeTime; // 시간 증가
            alpha.a = Mathf.Lerp(1f, 0f, time); // 알파값을 서서히 감소시킴
            fadeImg.color = alpha; // 이미지의 색상 정보변경
            yield return null; // 한 프레임 대기
        }

        fadeImg.gameObject.SetActive(false); // 이미지 비활성화

        yield return null; // 코루틴 종료
    }
}