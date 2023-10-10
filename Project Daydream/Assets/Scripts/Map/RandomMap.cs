using System.Collections.Generic;
using UnityEngine;

public class RandomMap : MonoBehaviour
{
    /* ---------------- 활성화 오브젝트 ---------------- */
    private GameObject leftObj;
    private GameObject centerObj;
    private GameObject rightObj;
    
    /* ---------------- 인스펙터 ---------------- */
    [SerializeField] private List<GameObject> left = new List<GameObject>();
    [SerializeField] private List<GameObject> center = new List<GameObject>();
    [SerializeField] private List<GameObject> right = new List<GameObject>();

    void Start() { ActivateRandomObject(); }

    private void ActivateRandomObject()
    {
        /* 오브젝트 랜덤 활성화 */
        if (left.Count > 0)
        {
            leftObj = left[Random.Range(0, left.Count)];
            leftObj.SetActive(true);
        }
        if (center.Count > 0)
        {
            centerObj = center[Random.Range(0, center.Count)];
            centerObj.SetActive(true);
        }
        if (right.Count > 0)
        {
            rightObj = right[Random.Range(0, right.Count)];
            rightObj.SetActive(true);
        }

        // 왼쪽과 중앙이 연결되게
        Vector3 centerObjPosition = centerObj.transform.position;
        centerObjPosition.y += leftObj.transform.Find("LeftToCenter").position.y - centerObj.transform.Find("LeftToCenter").position.y;
        centerObj.transform.position = centerObjPosition;

        // 중앙과 오른쪽이 연결되게
        Vector3 rightObjPosition = rightObj.transform.position;
        rightObjPosition.y += centerObj.transform.Find("CenterToRight").position.y - rightObj.transform.Find("CenterToRight").position.y;
        rightObj.transform.position = rightObjPosition;
    }
}