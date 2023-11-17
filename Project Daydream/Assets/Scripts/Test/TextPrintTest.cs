using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class TextPrintTest : MonoBehaviour
{
    /* ------- 텍스트 저장 변수 ------- */
    string filePathValue;

    /* ----------- 인스펙터 ----------- */
    [SerializeField]
    private Text textUI;

    private void Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "C:/UnityProject/Daydream/Project Daydream/Assets/Etc/testTextFile.txt");
        filePathValue = ReadTxt(filePath).ToString();
        Debug.Log(filePath);
        Debug.Log(ReadTxt(filePath));
        Debug.Log(ReadTxt(filePath).GetType());
        Debug.Log(filePathValue);
    }

    private object ReadTxt(string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        string value = "";

        if (fileInfo.Exists)
        {
            StreamReader reader = new StreamReader(filePath);
            value = reader.ReadToEnd();
            reader.Close();
        }

        else
            value = "파일이 없습니다.";

        return value;
    }

    private void OnTriggerEnter2D(Collider2D collision) // 나중에 다른 스크립트에서 충돌 계산하고 여기 코루틴만 호출하는 식으로 변경
    {
        if (collision.gameObject.CompareTag("Player"))
            StartCoroutine(TextPrint(textUI, filePathValue));
    }

    IEnumerator TextPrint(Text textUI, string textValue)
    {
        int typingCount = 0; // 줄당 글자 수

        string writeText = "";  // 이번 턴에 출력할 텍스트
        string currentText = ""; // 현재 출력된 텍스트

        for (int i = 0; i < textValue.Length; i++)
        {
            writeText = ""; // 줄 바뀔 때마다 초기화

            // 한 글자씩 타이핑 되는 듯한 효과 
            writeText += textValue[i]; // 문자열 하나씩 끊어서 적기
            textUI.text = currentText + writeText; // 텍스트 UI에 텍스트 표시
            yield return null;
            // yield return new WaitForSeconds(0.075f); // 글자마다 딜레이

            typingCount++;

            currentText += writeText;

            if (typingCount >= 30/* || writeText.Contains("\n")*/)
            {
                currentText += "\n"; // 한 줄 완성 후 줄 바꿈
                typingCount = 0;
            }

            yield return new WaitForSeconds(0.15f); // 줄 바꾼 후 딜레이
        }
    }
}