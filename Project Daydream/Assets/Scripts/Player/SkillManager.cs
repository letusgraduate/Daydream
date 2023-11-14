using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    /* -------------- 스킬 쿨타임 -------------- */
    private float ultimateSkillCoolTime;

    /* ---------------- 인스펙터 --------------- */
    [Header("오브젝트 연결")]
    [SerializeField]
    private GameObject[] ultimateSkills;

    [Header("일반 스킬 쿨타임")]
    [SerializeField]
    private float skillACoolTime = 4f;
    [SerializeField]
    private float skillSCoolTime = 2f;
    [SerializeField]
    private float skillDCoolTime = 0.5f;

    /* ---------------- 프로퍼티 --------------- */
    public float UltimateSkillCoolTime
    {
        get { return ultimateSkillCoolTime; }
        set { ultimateSkillCoolTime = value; }
    }

    public float SkillACoolTime
    {
        get { return skillACoolTime; }
    }

    public float SkillSCoolTime
    {
        get { return skillSCoolTime; }
    }

    public float SkillDCoolTime
    {
        get { return skillDCoolTime; }
    }

    /* --------------- 외부 참조 --------------- */
    public GameObject GetUltimateSkill(int num)
    {

        return ultimateSkills[num];
    }
}