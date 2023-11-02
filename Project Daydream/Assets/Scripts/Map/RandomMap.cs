using System.Collections.Generic;
using UnityEngine;

public class RandomMap : MonoBehaviour
{
    /* ---------------- 활성화 오브젝트 ---------------- */
    private GameObject leftObj;
    private GameObject centerObj;
    private GameObject rightObj;

    /* ---------------- 핀 포인트 ---------------- */
    Transform leftPivot;
    Transform centerLeftPivot;
    Transform centerRightPivot;
    Transform rightPivot;

    /* ---------------- 활성화된 오브젝트의 자식들 ---------------- */
    Transform[] leftChildren;
    Transform[] centerChildren;
    Transform[] rightChildren;

    /* ---------------- 인스펙터 ---------------- */
    [SerializeField] private Transform[] left;
    [SerializeField] private GameObject[] leftG;
    [SerializeField] private Transform[] center;
    [SerializeField] private GameObject[] centerG;
    [SerializeField] private Transform[] right;
    [SerializeField] private GameObject[] rightG;

    void Start() { ActivateRandomObject(); }

    private void ActivateRandomObject()
    {
        int randomNumLeft, randomNumCenter, randomNumRight;
        int preL = 0, preC = 2, preR = 4; // 이 변수는 나중에 GameManager쪽으로 변경

        // 오브젝트 랜덤 활성화
        if (left.Length > 0 && center.Length > 0 && right.Length > 0)
        {
            do
            {
                randomNumLeft = Random.Range(0, left.Length);
                randomNumCenter = Random.Range(0, center.Length);
                randomNumRight = Random.Range(0, right.Length);
            } while (!(randomNumLeft != preL && randomNumCenter != preC && randomNumRight != preR));

            preL = randomNumLeft;
            preC = randomNumCenter;
            preR = randomNumRight;

            Debug.Log(left[randomNumLeft]);

            leftObj = leftG[randomNumLeft];
            centerObj = centerG[randomNumCenter];
            rightObj = rightG[randomNumRight];

            leftObj.SetActive(true);
            centerObj.SetActive(true);
            rightObj.SetActive(true);

            /*left[randomNumLeft].gameObject.SetActive(true);
            center[randomNumCenter].gameObject.SetActive(true);
            right[randomNumRight].gameObject.SetActive(true);

            leftChildren = left[randomNumLeft].gameObject.GetComponentsInChildren<Transform>();
            centerChildren = center[randomNumCenter].gameObject.GetComponentsInChildren<Transform>();
            rightChildren = right[randomNumRight].gameObject.GetComponentsInChildren<Transform>();*/

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
            Vector3 centerObjPosition = center[randomNumCenter].gameObject.transform.position;
            // centerObjPosition.x += centerLeftPoint.position.x - leftPoint.position.x;
            float a = center[randomNumCenter].localScale.x/2 + left[randomNumCenter].localScale.x/2;
            // x좌표 a만큼 이동
            centerObjPosition.y += leftPivot.position.y - centerLeftPivot.position.y;
            center[randomNumCenter].gameObject.transform.position = centerObjPosition;

            // 중앙과 오른쪽 연결
            Vector3 rightObjPosition = right[randomNumRight].gameObject.transform.position;
            rightObjPosition.y += centerRightPivot.position.y - rightPivot.position.y;
            right[randomNumRight].gameObject.transform.position = rightObjPosition;
        }
    }
}

/*using System.Collections.Generic;
using UnityEngine;

public class RandomMap : MonoBehaviour
{
    *//* ---------------- 활성화 오브젝트 ---------------- *//*
    private GameObject leftObj;
    private GameObject centerObj;
    private GameObject rightObj;

    *//* ---------------- 활성화된 오브젝트의 자식들 ---------------- *//*
    Transform[] leftChildren;
    Transform[] centerChildren;
    Transform[] rightChildren;

    *//* ---------------- 핀 포인트 ---------------- *//*
    Transform leftPoint;
    Transform centerLeftPoint;
    Transform centerRightPoint;
    Transform rightPoint;

    *//* ---------------- 인스펙터 ---------------- *//*
    [SerializeField] private List<GameObject> left;
    [SerializeField] private List<GameObject> center;
    [SerializeField] private List<GameObject> right;

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

        *//* 활성화된 오브젝트의 자식들 중 핀 포인트 찾기 *//*
        for (int i = 0; i < leftChildren.Length; i++)
        {
            if (leftChildren[i].name == "LeftToCenter")
                leftPoint = leftChildren[i];
        }
        for (int i = 0; i < centerChildren.Length; i++)
        {
            if (centerChildren[i].name == "LeftToCenter")
                centerLeftPoint = centerChildren[i];
        }
        for (int i = 0; i < centerChildren.Length; i++)
        {
            if (centerChildren[i].name == "CenterToRight")
                centerRightPoint = centerChildren[i];
        }
        for (int i = 0; i < rightChildren.Length; i++)
        {
            if (rightChildren[i].name == "CenterToRight")
                rightPoint = rightChildren[i];
        }

        // 왼쪽과 중앙 연결
        Vector3 centerObjPosition = centerObj.transform.position;
        // centerObjPosition.x += leftPoint.position.x - centerLeftPoint.position.x;
        // centerObjPosition.x += centerLeftPoint.position.x - leftPoint.position.x;
        centerObjPosition.y += leftPoint.position.y - centerLeftPoint.position.y;
        centerObj.transform.position = centerObjPosition;

        // 중앙과 오른쪽 연결
        Vector3 rightObjPosition = rightObj.transform.position;
        rightObjPosition.y += centerRightPoint.position.y - rightPoint.position.y;
        rightObj.transform.position = rightObjPosition;
    }
}*/