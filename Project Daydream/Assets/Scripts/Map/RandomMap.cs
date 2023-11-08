using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMap : MonoBehaviour
{
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
    [SerializeField] private Transform[] center;
    [SerializeField] private Transform[] right;

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

            left[randomNumLeft].gameObject.SetActive(true);
            center[randomNumCenter].gameObject.SetActive(true);
            right[randomNumRight].gameObject.SetActive(true);

            leftChildren = left[randomNumLeft].gameObject.GetComponentsInChildren<Transform>();
            centerChildren = center[randomNumCenter].gameObject.GetComponentsInChildren<Transform>();
            rightChildren = right[randomNumRight].gameObject.GetComponentsInChildren<Transform>();

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
            centerObjPosition.x += leftPivot.position.x - centerLeftPivot.position.x + centerLeftPivot.localScale.x / 2 + leftPivot.localScale.x / 2;
            centerObjPosition.y += leftPivot.position.y - centerLeftPivot.position.y;
            center[randomNumCenter].gameObject.transform.position = centerObjPosition;

            // 중앙과 오른쪽 연결
            Vector3 rightObjPosition = right[randomNumRight].gameObject.transform.position;
            rightObjPosition.x += centerRightPivot.position.x - rightPivot.position.x + centerRightPivot.localScale.x / 2 + rightPivot.localScale.x / 2;
            rightObjPosition.y += centerRightPivot.position.y - rightPivot.position.y;
            right[randomNumRight].gameObject.transform.position = rightObjPosition;
        }
    }
}