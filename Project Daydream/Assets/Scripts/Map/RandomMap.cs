using System.Collections.Generic;
using UnityEngine;

public class RandomMap : MonoBehaviour
{
    /* ---------------- 활성화 오브젝트 ---------------- */
    private GameObject leftObj;
    private GameObject centerObj;
    private GameObject rightObj;

    /* ---------------- 활성화된 오브젝트의 자식들 ---------------- */
    Transform[] leftChildren;
    Transform[] centerChildren;
    Transform[] rightChildren;

    /* ---------------- 핀 포인트 ---------------- */
    Transform leftPoint;
    Transform centerLeftPoint;
    Transform centerRightPoint;
    Transform rightPoint;

    /* ---------------- 인스펙터 ---------------- */
    [SerializeField] private List<GameObject> left = new List<GameObject>();
    [SerializeField] private List<GameObject> center = new List<GameObject>();
    [SerializeField] private List<GameObject> right = new List<GameObject>();

    void Start() { ActivateRandomObject(); }

    private void ActivateRandomObject()
    {
        int randomNumLeft, randomNumCenter, randomNumRight;
        int preL = 0, preC = 2, preR = 4; // 이 변수는 나중에 GameManager쪽으로 변경

        // 오브젝트 랜덤 활성화
        if (left.Count > 0 && center.Count > 0 && right.Count > 0)
        {
            do
            {
                randomNumLeft = Random.Range(0, left.Count);
                randomNumCenter = Random.Range(0, center.Count);
                randomNumRight = Random.Range(0, right.Count);
            } while (!(randomNumLeft != preL && randomNumCenter != preC && randomNumRight != preR));

            preL = randomNumLeft;
            preC = randomNumCenter;
            preR = randomNumRight;

            leftObj = left[randomNumLeft];
            centerObj = center[randomNumCenter];
            rightObj = right[randomNumRight];

            leftObj.SetActive(true);
            centerObj.SetActive(true);
            rightObj.SetActive(true);
        }

        leftChildren = leftObj.GetComponentsInChildren<Transform>();
        centerChildren = centerObj.GetComponentsInChildren<Transform>();
        rightChildren = rightObj.GetComponentsInChildren<Transform>();

        /* 활성화된 오브젝트의 자식들 중 핀 포인트 찾기 */
        foreach (Transform child in leftChildren)
        {
            if (child.name == "LeftToCenter")
            {
                leftPoint = child;
            }
            // else { leftPoint = leftObj.transform.GetChild(0); }
        }
        foreach (Transform child in centerChildren)
        {
            if (child.name == "LeftToCenter")
            {
                centerLeftPoint = child;
            }
            if (child.name == "CenterToRight")
            {
                centerRightPoint = child;
            }
        }
        foreach (Transform child in rightChildren)
        {
            if (child.name == "CenterToRight")
            {
                rightPoint = child;
            }
        }

        // 왼쪽과 중앙 연결
        Vector3 centerObjPosition = centerObj.transform.position;
        // centerObjPosition.x += leftPoint.position.x - centerLeftPoint.position.x;
        centerObjPosition.y += leftPoint.position.y - centerLeftPoint.position.y;
        centerObj.transform.position = centerObjPosition;

        // 중앙과 오른쪽 연결
        Vector3 rightObjPosition = rightObj.transform.position;
        // rightObjPosition.x += centerRightPoint.position.x - rightPoint.position.x;
        rightObjPosition.y += centerRightPoint.position.y - rightPoint.position.y;
        rightObj.transform.position = rightObjPosition;
    }
}