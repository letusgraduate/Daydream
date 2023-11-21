using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    /* -------------- 이벤트 함수 -------------- */
    private void Start()
    {
        GameManager.instance.SpawnPortal(transform.position);
    }
}
