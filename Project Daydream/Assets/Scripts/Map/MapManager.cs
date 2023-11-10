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
        firstInPivot = GetPivot(firstMaps[nowFirstMap], "MapPivotIn");
        secendOutPivot = GetPivot(secendMaps[nowSecendMap], "MapPivotOut");
        secendInPivot = GetPivot(secendMaps[nowSecendMap], "MapPivotIn");
        thirdOutPivot = GetPivot(thirdMaps[nowThirdMap], "MapPivotOut");

        /* 섹터들 위치 조정 */
        firstMaps[nowFirstMap].position = new Vector2(0, 0);
        ConnectMap(secendMaps[nowSecendMap], firstInPivot, secendOutPivot);
        ConnectMap(thirdMaps[nowThirdMap], secendInPivot, thirdOutPivot);

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

    private Transform GetPivot(Transform parent, string tagName)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>();

        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].CompareTag(tagName))
                return children[i];
        }
        return null;
    }

    private void ConnectMap(Transform map, Transform inPivot, Transform outPivot)
    {
        Vector3 pivotPos = map.position;

        pivotPos.x += inPivot.position.x - outPivot.position.x
            + outPivot.localScale.x / 2 + inPivot.localScale.x / 2; // 피벗 크기들의 절반씩 이동해 옆에 붙게
        pivotPos.y += inPivot.position.y - outPivot.position.y;

        map.position = pivotPos;
    }

    private void RemoveMapList(List<Transform> map, int index)
    {
        map.RemoveAt(index);
    }
}