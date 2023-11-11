using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    //스킬 아이템
    private bool isUltimateSkill;

    // 일반 스킬
    private bool isASkill;
    private bool isSSkill;
    private bool isDSkill;
    // 스킬 입역 변수
    private bool ultimateSkillInput;
    private bool aSkillInput;
    private bool sSkillInput;
    private bool dSkillInput;
    [Header("오브젝트")]
    [SerializeField]
    private GameObject skillManager;
    // Start is called before the first frame update

    public bool IsUltimateSkill
    {
        get { return IsUltimateSkill; }
    }

    // Update is called once per frame
    void Update()
    {
        ultimateSkillInput = Input.GetKeyDown(KeyCode.F);
        aSkillInput = Input.GetKeyDown(KeyCode.Z);
        sSkillInput = Input.GetKeyDown(KeyCode.X);
        dSkillInput = Input.GetKeyDown(KeyCode.C);

        UltimateSkill();
        ASkill();
        SSkill();
        DSkill();

    }

    private void UltimateSkill()
    {
        if (!ultimateSkillInput || skillManager.transform.childCount <= 0 || isUltimateSkill)
            return;
        isUltimateSkill = true;
        skillManager.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        StartCoroutine(UltimateSkillCoolOut(skillManager.GetComponent<SkillManager>().UltimateSkillCoolTime));
    }

    IEnumerator UltimateSkillCoolOut(float second)
    {
        yield return new WaitForSeconds(second);
        isUltimateSkill = false;
        skillManager.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void ASkill()
    {
        if (!aSkillInput || isASkill)
            return;
        isASkill = true;
        Debug.Log("ASkill Start");
        StartCoroutine(ASkillCoolOut(skillManager.GetComponent<SkillManager>().ASkillCoolTime));
    }

    IEnumerator ASkillCoolOut(float second)
    {
        yield return new WaitForSeconds(second);
        Debug.Log("ASkill End");
        isASkill = false;
    }
    private void SSkill()
    {
        if (!sSkillInput || isSSkill)
            return;
        isSSkill = true;
        Debug.Log("SSkill Start");
        StartCoroutine(SSkillCoolOut(skillManager.GetComponent<SkillManager>().SSkillCoolTime));
    }

    IEnumerator SSkillCoolOut(float second)
    {
        yield return new WaitForSeconds(second);
        Debug.Log("SSkill End");
        isSSkill = false;
    }

    private void DSkill()
    {
        if (!dSkillInput || isDSkill)
            return;
        isDSkill = true;
        Debug.Log("DSkill Start");
        StartCoroutine(DSkillCoolOut(skillManager.GetComponent<SkillManager>().DSkillCoolTime));
    }

    IEnumerator DSkillCoolOut(float second)
    {
        yield return new WaitForSeconds(second);
        Debug.Log("DSkill End");
        isDSkill = false;
    }
}
