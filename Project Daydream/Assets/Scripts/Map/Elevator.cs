using UnityEngine;

public class Elevator : MonoBehaviour
{
    /* ---------------- 인스펙터 ---------------- */
    [SerializeField] private bool isUpDown; // 이동 방향이 x축인지 y축인지 설정
    [SerializeField] private float movingSpeed, amplitude; // 움직이는 속력과 진폭(범위) 설정
    [SerializeField] private string targetName; // 대상 이름 저장(ex. 플레이어)

    /* ---------------- 변수 ---------------- */
    Rigidbody2D rigid;
    Vector2 initialPosition;

    void Start() // 초기화
    {
        rigid = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    private void Update()
    {
        // 위아래로 움직이는 오브젝트일 때
        if (isUpDown)
        {
            rigid.transform.position = new Vector2(initialPosition.x, initialPosition.y + amplitude * Mathf.Sin(Time.time * movingSpeed));
        }
        // 좌우로 움직이는 오브젝트일 때
        else
        {
            rigid.transform.position = new Vector2(initialPosition.x + amplitude * Mathf.Sin(Time.time * movingSpeed), initialPosition.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // 엘리베이터 활성화
    {
        if (collision.gameObject.name == targetName) { collision.transform.SetParent(transform); }
    }

    private void OnCollisionExit2D(Collision2D collision) // 엘리베이터 비활성화
    {
        if (collision.gameObject.name == targetName) { collision.transform.SetParent(null); }
    }
}