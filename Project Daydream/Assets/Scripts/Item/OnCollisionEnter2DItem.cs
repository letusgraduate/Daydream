using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionEnter2DItem : MonoBehaviour
{
    [Header("오브젝트 연결")]
    [SerializeField]
    private GameObject[] itemPrefabs; // GameObject 배열로 수정
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ITEM"))
            Destroy(other.gameObject);
    }
}
