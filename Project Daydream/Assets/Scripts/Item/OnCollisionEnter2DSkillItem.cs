using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionEnter2DSkillItem : MonoBehaviour
{
    [Header("오브젝트")]
    [SerializeField]
    private GameObject[] skillPrefabs; // GameObject 배열로 수정
    [SerializeField]
    private GameObject skillManager;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("SkillItem"))
        {
            if (skillManager.transform.childCount > 0)
            {
                Destroy(skillManager.transform.GetChild(0).gameObject);
            }

            int skillNumber = other.gameObject.GetComponent<SkillItem>().SkillNumber;
            skillManager.GetComponent<SkillManager>().UltimateSkillCoolTime = other.gameObject.GetComponent<SkillItem>().SkillCoolTime;
            GameObject skill = Instantiate(skillPrefabs[skillNumber], skillManager.transform);
            skill.transform.localPosition = Vector3.zero;
            Destroy(other.gameObject);
        }
    }
}
