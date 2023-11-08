using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    /* --------------- 핀 포인트 --------------- */
    private Transform leftPivot;
    private Transform centerLeftPivot;
    private Transform centerRightPivot;
    private Transform rightPivot;

    /* ------ 활성화된 오브젝트의 자식들 ------- */
    private Transform[] leftChildren;
    private Transform[] centerChildren;
    private Transform[] rightChildren;

    /* -------------- 현재 랜덤 맵 ------------- */
    private int nowLeftMap;
    private int nowCenterMap;
    private int nowRightMap;

    /* ---------------- 인스펙터 --------------- */
    [Header("오브젝트 연결")]
    [SerializeField]
    private List<Transform> leftMaps = new List<Transform>();
    [SerializeField]
    private List<Transform> centerMaps = new List<Transform>();
    [SerializeField]
    private List<Transform> rightMaps = new List<Transform>();

    private void Start()
    {
        RandomMapSpawn();
    }

    public void RandomMapSpawn()
    {
        // 맵 없으면 탈출
        if (leftMaps.Count <= 0 && centerMaps.Count <= 0 && rightMaps.Count <= 0)
        {
            Debug.Log("Map Null");
            return;
        }

        nowLeftMap = MapSelect(leftMaps);
        nowCenterMap = MapSelect(centerMaps);
        nowRightMap = MapSelect(rightMaps);

        leftChildren = leftMaps[nowLeftMap].gameObject.GetComponentsInChildren<Transform>();
        centerChildren = centerMaps[nowCenterMap].gameObject.GetComponentsInChildren<Transform>();
        rightChildren = rightMaps[nowRightMap].gameObject.GetComponentsInChildren<Transform>();

        /* 활성화된 오브젝트의 자식들 중 핀 포인트 찾기 */
        for (int i = 0; i < leftChildren.Length; i++)
        {
            if (leftChildren[i].name == "LeftToCenterPivot")
                leftPivot = leftChildren[i];
        }
        for (int i = 0; i < centerChildren.Length; i++)
        {
            if (centerChildren[i].name == "LeftToCenterPivot")
                centerLeftPivot = centerChildren[i];
        }
        for (int i = 0; i < centerChildren.Length; i++)
        {
            if (centerChildren[i].name == "CenterToRightPivot")
                centerRightPivot = centerChildren[i];
        }
        for (int i = 0; i < rightChildren.Length; i++)
        {
            if (rightChildren[i].name == "CenterToRightPivot")
                rightPivot = rightChildren[i];
        }

        // 왼쪽과 중앙 연결
        Vector3 centerObjPosition = centerMaps[nowCenterMap].gameObject.transform.position;
        centerObjPosition.x += leftPivot.position.x - centerLeftPivot.position.x + centerLeftPivot.localScale.x / 2 + leftPivot.localScale.x / 2;
        centerObjPosition.y += leftPivot.position.y - centerLeftPivot.position.y;
        centerMaps[nowCenterMap].gameObject.transform.position = centerObjPosition;

        // 중앙과 오른쪽 연결
        Vector3 rightObjPosition = rightMaps[nowRightMap].gameObject.transform.position;
        rightObjPosition.x += centerRightPivot.position.x - rightPivot.position.x + centerRightPivot.localScale.x / 2 + rightPivot.localScale.x / 2;
        rightObjPosition.y += centerRightPivot.position.y - rightPivot.position.y;
        rightMaps[nowRightMap].gameObject.transform.position = rightObjPosition;

        RemoveMapList(leftMaps, nowLeftMap);
        RemoveMapList(centerMaps, nowCenterMap);
        RemoveMapList(rightMaps, nowRightMap);
    }

    private int MapSelect(List<Transform> map)
    {
        int randomNum = Random.Range(0, map.Count);
        map[randomNum].gameObject.SetActive(true);

        return randomNum;
    }

    private void RemoveMapList(List<Transform> map, int index)
    {
        map.RemoveAt(index);
    }
}