using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /* ------------- 컴포넌트 변수 ------------- */
    private Transform player;

    /* ---------------- 인스펙터 --------------- */
    [Header("설정")]
    [SerializeField, Range(0f, 10f)]
    private float xPos = 0f;
    [SerializeField, Range(0f, 10f)]
    private float yPos = 2.5f;

    /* -------------- 이벤트 함수 -------------- */
    private void Awake()
    {
        player = GameManager.instance.Player.GetComponent<Transform>();
    }

    private void Update()
    {
        transform.position = new Vector3(player.position.x + xPos, player.position.y + yPos, transform.position.z);
    }
}
