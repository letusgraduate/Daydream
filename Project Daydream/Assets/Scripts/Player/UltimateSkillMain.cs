using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateSkillMain : MonoBehaviour
{
    /* ---------------- 인스펙터 --------------- */
    [Header("궁 스킬 번호")]
    [SerializeField, Range(0, 100)]
    private int ultimateSkillNum;

    [Header("스킬 쿨타임")]
    [SerializeField, Range(0f, 100f)]
    private float ultimateSkillCoolTime = 5f;

    /* ---------------- 프로퍼티 --------------- */
    public int UltimateSkillNum
    {
        get { return ultimateSkillNum; }
    }

    public float UltimateSkillCoolTime
    {
        get { return ultimateSkillCoolTime; }
    }
}
