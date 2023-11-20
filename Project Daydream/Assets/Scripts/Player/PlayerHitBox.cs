using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    /* ------------- 컴포넌트 변수 ------------- */
    private PlayerMain playerMain;

    /* --------------- 피격 관련 --------------- */
    private int takeDamage = 0;

    /* -------------- 이벤트 함수 -------------- */
    private void Awake()
    {
        playerMain = GetComponentInParent<PlayerMain>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /* 피격 */
        if (collision.gameObject.CompareTag("Enemy"))
        {
            takeDamage = collision.gameObject.GetComponent<DamageMain>().Damage;
            playerMain.OnHit(collision.transform.position, takeDamage); // Enemy의 위치 정보 매개변수
        }
        if (collision.gameObject.CompareTag("Enemy Attack"))
        {
            takeDamage = collision.gameObject.GetComponent<DamageMain>().Damage;
            playerMain.OnHit(collision.transform.position, takeDamage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* 피격 */
        if (collision.CompareTag("Enemy Attack"))
        {
            takeDamage = collision.GetComponent<DamageMain>().Damage;
            playerMain.OnHit(collision.transform.position, takeDamage);
        }
        if (collision.CompareTag("Trap"))
        {
            takeDamage = collision.GetComponent<DamageMain>().Damage;
            playerMain.OnHit(collision.transform.position, takeDamage);
        }

        /* 아이템 획득 */
        if (collision.gameObject.layer == 15) // Currency
            playerMain.GetCurrency(collision.gameObject);
        if (collision.CompareTag("UltimateSkillItem"))
            playerMain.GetUltimateSkill(collision.gameObject);
        if (collision.CompareTag("Item"))
            playerMain.GetItem(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC") && Input.GetKey(KeyCode.E))
        {
            UIManager.instance.ShowTraitUI();
        }
    }
}
