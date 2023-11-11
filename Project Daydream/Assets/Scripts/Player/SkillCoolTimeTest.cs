using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTimeTest : MonoBehaviour
{
    public SpriteRenderer go;

    [Header("설정")]
    [Range(0, 20)]
    public float skillTime = 10.0f;
    [Range(0, 20)]
    public float skillCoolTime = 10.0f;

    public Image skillFillAmount;
    public GameObject SkillObject;
    GameObject SkillPrefab;

    bool isUseSkill = true;

    private void Awake()
    {
        SkillPrefab = SkillObject.transform.GetChild(0).gameObject;
        skillCoolTime = SkillPrefab.GetComponent<SkillController>().SkillCoolTime;
    }

    public void UseSkill() //스킬 사용
    {
        if (Input.GetKeyDown (KeyCode.E))
        {
            go.material.color = Color.blue;

            isUseSkill = false;

            skillFillAmount.fillAmount = 1;

            StartCoroutine(SkillCoroutine());
        }
    }
    IEnumerator SkillCoroutine() //스킬 사용중
    {
        while (skillFillAmount.fillAmount > 0)
        {
            go.material.color = Color.red;

            skillFillAmount.fillAmount -= 1 * Time.smoothDeltaTime / skillTime;
            yield return null;
        }

        StartCoroutine(ResetSkillCoroutine());
    }

    IEnumerator ResetSkillCoroutine() //스킬 쿨타임
    {
        while (skillFillAmount.fillAmount < 1)
        {
            go.material.color = Color.blue;

            skillFillAmount.fillAmount += 1 * Time.smoothDeltaTime / skillCoolTime;

            yield return null;
        }

        isUseSkill = true;
    }
}