using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour
{
    [Header("아이템 번호")]
    [SerializeField, Range(0, 100)]
    private int skillNumber;

    [Space(10f)]
    [Header("스킬 쿨타임")]
    [SerializeField, Range(0f, 100f)]
    private float skillCoolTime = 5f;


    public int SkillNumber
    {
        get { return skillNumber; }
    }
    public float SkillCoolTime
    {
        get { return skillCoolTime; }
    }

}
