using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    /* ---------------- 인스펙터 --------------- */
    [Header("아이템 목록")]
    [SerializeField]
    private GameObject[] itemPrefabs;

    /* -------------- 이벤트 함수 -------------- */
    private void Start()
    {
        SpawnItem();
    }

    private void SpawnItem()
    {
        int randomIndex = Random.Range(0, itemPrefabs.Length);
        Instantiate(itemPrefabs[randomIndex], transform.position, Quaternion.identity);
    }
}
