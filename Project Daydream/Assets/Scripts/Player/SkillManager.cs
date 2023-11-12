using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    /* --------------스킬 쿨타임-------------- */
    private float ultimateSkillCoolTime;

    private float aSkillCoolTime = 2f;
    private float sSkillCoolTime = 3f;
    private float dSkillCoolTime = 4f;

    /* ---------------- 프로퍼티 --------------- */
    public float UltimateSkillCoolTime
    {
        get { return ultimateSkillCoolTime; }
        set { ultimateSkillCoolTime = value; }
    }

    public float ASkillCoolTime
    {
        get { return aSkillCoolTime; }
    }

    public float SSkillCoolTime
    {
        get { return sSkillCoolTime; }
    }
    public float DSkillCoolTime
    {
        get { return dSkillCoolTime; }
    }

}