using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionEnter2DSkillItem : MonoBehaviour
{
    /* ---------------- 인스펙터 --------------- */
    [Header("오브젝트 연결")]
    [SerializeField]
    private GameObject[] skillPrefabs; // GameObject 배열로 수정
    [SerializeField]
    private SkillManager skillManager;

    /* -------------- 이벤트 함수 -------------- */
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("SkillItem"))
        {
            //현재 가지고 있는 궁스킬 삭제
            if (skillManager.transform.childCount > 0)
            {
                Destroy(skillManager.transform.GetChild(0).gameObject);
            }

            int num = other.gameObject.GetComponent<UltimateSkillMain>().UltimateSkillNum; // 먹은 스킬 아이템의 종류 파악
            skillManager.GetComponent<SkillManager>().UltimateSkillCoolTime = other.gameObject.GetComponent<UltimateSkillMain>().UltimateSkillCoolTime; //궁스킬 쿨타임 설정
            GameObject skill = Instantiate(skillManager.GetUltimateSkill(num), skillManager.transform); // 궁스킬 프리팹 소환
            skill.transform.localPosition = Vector3.zero;

            //스킬 아이템 삭제
            Destroy(other.gameObject);
        }
    }
}