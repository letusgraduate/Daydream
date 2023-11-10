using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    /* --------------- 핀 포인트 --------------- */
    private Transform firstInPivot;
    private Transform secendOutPivot;
    private Transform secendInPivot;
    private Transform thirdOutPivot;

    /* ---------- 현재 랜덤 맵 인덱스 ---------- */
    private int nowFirstMap;
    private int nowSecendMap;
    private int nowThirdMap;

    /* ---------------- 인스펙터 --------------- */
    [Header("오브젝트 연결")]
    [SerializeField]
    private List<Transform> firstMaps = new List<Transform>();
    [SerializeField]
    private List<Transform> secendMaps = new List<Transform>();
    [SerializeField]
    private List<Transform> thirdMaps = new List<Transform>();

    /* -------------- 이벤트 함수 -------------- */
    private void Start()
    {
        RandomMapSpawn();
    }

    /* --------------- 기능 함수 --------------- */
    public void RandomMapSpawn()
    {
        /* 맵 없으면 탈출 */
        if (firstMaps.Count <= 0 && secendMaps.Count <= 0 && thirdMaps.Count <= 0)
        {
            Debug.Log("Map Null");
            return;
        }

        /* 랜덤 맵 선택 */
        nowFirstMap = SelectMap(firstMaps);
        nowSecendMap = SelectMap(secendMaps);
        nowThirdMap = SelectMap(thirdMaps);

        /* 피벗 참조 */
        firstInPivot = getPivot(firstMaps[nowFirstMap], "MapPivotIn");
        secendOutPivot = getPivot(secendMaps[nowSecendMap], "MapPivotOut");
        secendInPivot = getPivot(secendMaps[nowSecendMap], "MapPivotIn");
        thirdOutPivot = getPivot(thirdMaps[nowThirdMap], "MapPivotOut");

        /* 1섹터 위치 초기화 */
        firstMaps[nowFirstMap].position = new Vector2(0, 0);

        /* 1섹터 2섹터 연결 */
        Vector3 secendPivotPos = secendMaps[nowSecendMap].position;
        secendPivotPos.x += firstInPivot.position.x - secendOutPivot.position.x
            + secendOutPivot.localScale.x / 2 + firstInPivot.localScale.x / 2; // 피벗 크기들의 절반씩 이동해 옆에 붙게
        secendPivotPos.y += firstInPivot.position.y - secendOutPivot.position.y;
        secendMaps[nowSecendMap].position = secendPivotPos;

        /* 2섹터 3섹터 연결 */
        Vector3 thirdPivotPos = thirdMaps[nowThirdMap].position;
        thirdPivotPos.x += secendInPivot.position.x - thirdOutPivot.position.x
            + secendInPivot.localScale.x / 2 + thirdOutPivot.localScale.x / 2;
        thirdPivotPos.y += secendInPivot.position.y - thirdOutPivot.position.y;
        thirdMaps[nowThirdMap].position = thirdPivotPos;

        /* 리스트에서 맵 삭제 */
        RemoveMapList(firstMaps, nowFirstMap);
        RemoveMapList(secendMaps, nowSecendMap);
        RemoveMapList(thirdMaps, nowThirdMap);
    }

    private int SelectMap(List<Transform> map)
    {
        int randomNum = Random.Range(0, map.Count);
        map[randomNum].gameObject.SetActive(true);

        return randomNum;
    }

    private Transform getPivot(Transform parent, string tagName)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>();

        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].CompareTag(tagName))
                return children[i];
        }
        return null;
    }

    private void RemoveMapList(List<Transform> map, int index)
    {
        map.RemoveAt(index);
    }
}