using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    /* -------------- 스킬 특성-- -------------- */
    private int skillUpgrade = 0;

    /* -------------- 스킬 쿨타임 -------------- */
    private float ultimateSkillCoolTime;

    /* ---------------- 인스펙터 --------------- */
    [Header("오브젝트 연결")]
    [SerializeField]
    private GameObject[] ultimateSkills;

    [Header("스킬 이미지")]
    [SerializeField]
    private List<Sprite> skillImages = new List<Sprite>();
    [SerializeField]
    private Sprite[] ultimateSkillImages;
    [SerializeField]
    private Sprite ultimateSkillImage;

    [Header("일반 스킬 쿨타임")]
    [SerializeField]
    private float skillACoolTime = 10f;
    [SerializeField]
    private float skillSCoolTime = 5f;
    [SerializeField]
    private float skillDCoolTime = 0.6f;

    [Header("스킬 동작 시간")] // 애니메이션 길이
    [SerializeField]
    private float skillAActiveTime = 0.7f;
    [SerializeField]
    private float skillSActiveTime = 0.7f;
    [SerializeField]
    private float skillDActiveTime = 0.6f;
    [SerializeField]
    private float ultimateSkillActiveTime = 1.5f;

    /* ---------------- 프로퍼티 --------------- */
    /* 특성 */
    public int SkillUpgrade { get { return skillUpgrade; } set { skillUpgrade = value; } }

    /* 쿨타임 */
    public float UltimateSkillCoolTime { get { return ultimateSkillCoolTime; } set { ultimateSkillCoolTime = value; } }
    public float SkillACoolTime { get { return skillACoolTime; } }
    public float SkillSCoolTime { get { return skillSCoolTime; } }
    public float SkillDCoolTime { get { return skillDCoolTime; } }

    /* 동작 시간 */
    public float SkillAActiveTime { get { return skillAActiveTime; } }
    public float SkillSActiveTime { get { return skillSActiveTime; } }
    public float SkillDActiveTime { get { return skillDActiveTime; } }
    public float UltimateSkillActiveTime { get { return ultimateSkillActiveTime; } }

    /* 궁스킬 이미지 */
    public Sprite UltimateSkillImage
    {
        get { return ultimateSkillImage; }
        set { ultimateSkillImage = value; }
    }

    /* --------------- 외부 참조 --------------- */
    public GameObject GetUltimateSkill(int num)
    {
        return ultimateSkills[num];
    }

    public Sprite SkillImages(int num)
    {
        return skillImages[num];
    }

    public void SetUltimateSkillImage(int num)
    {
        UltimateSkillImage = ultimateSkillImages[num];
    }
}