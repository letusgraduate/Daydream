using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    /* --------------스킬 쿨타임-------------- */
    private float ultimateSkillCoolTime;

    private float skillACoolTime = 2f;
    private float skillSCoolTime = 3f;
    private float skillDCoolTime = 4f;

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

}