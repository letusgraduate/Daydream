using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    /* --------------컴포넌트 변수-------------- */
    private SkillManager skillManager;

    /* --------------스킬 확인 변수-------------- */
    //스킬 아이템
    private bool isUltimateSkill;
    // 일반 스킬
    private bool isASkill;
    private bool isSSkill;
    private bool isDSkill;

    /* ----------- 입력 값 저장 변수 ----------- */
    private bool ultimateSkillInput;
    private bool aSkillInput;
    private bool sSkillInput;
    private bool dSkillInput;
    /* ---------------- 인스펙터 --------------- */

    [Header("오브젝트")]
    [SerializeField]
    private GameObject skillManagerObject;
    [SerializeField]
    private GameObject[] bulletPrefab;

    /* ---------------- 프로퍼티 --------------- */
    public bool IsUltimateSkill
    {
        get { return isUltimateSkill; }
    }
    public bool IsASkill
    {
        get { return isASkill; }
    }

    public bool IsSSkill
    {
        get { return isSSkill; }
    }

    public bool IsDSkill
    {
        get { return isDSkill; }
    }


    /* -------------- 이벤트 함수 -------------- */
    void Update()
    {
        skillManager = skillManagerObject.GetComponent<SkillManager>();
        ultimateSkillInput = Input.GetKeyDown(KeyCode.F);
        aSkillInput = Input.GetKeyDown(KeyCode.Z);
        sSkillInput = Input.GetKeyDown(KeyCode.X);
        dSkillInput = Input.GetKeyDown(KeyCode.C);

        //스킬 사용
        UltimateSkill();
        ASkill();
        SSkill();
        DSkill();

    }

    /* --------------- 기능 함수 --------------- */
    private void UltimateSkill()
    {
        //키입력과 스킬 사용중이 아니고 궁스킬이 있을 때 스킬 사용 가능
        if (!ultimateSkillInput || skillManagerObject.transform.childCount <= 0 || isUltimateSkill)
            return;
        isUltimateSkill = true;
        skillManagerObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.gray; //스킬 이미지 회색으로 전환

        // 불렛 소환
        GameObject bullet = Instantiate(bulletPrefab[3]);
        bullet.GetComponent<BulletMain>().Direction = this.transform.localScale.x;//불렛 방향 설정
        bullet.transform.position = new Vector3(this.transform.position.x + this.transform.localScale.x, this.transform.position.y, 0);//소환 위치 조정
        StartCoroutine(UltimateSkillCoolOut(skillManager.UltimateSkillCoolTime));
    }

    //스킬 쿨타임
    IEnumerator UltimateSkillCoolOut(float second)
    {
        yield return new WaitForSeconds(second);
        isUltimateSkill = false;
        skillManagerObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void ASkill()
    {
        //키입력과 스킬 사용중이 아닐 때 스킬 사용 가능
        if (!aSkillInput || isASkill)
            return;
        isASkill = true;

        // 불렛 소환
        GameObject bullet = Instantiate(bulletPrefab[0]);
        bullet.GetComponent<BulletMain>().Direction = this.transform.localScale.x;//불렛 방향 설정
        bullet.transform.position = new Vector3(this.transform.position.x + this.transform.localScale.x, this.transform.position.y, 0);//소환 위치 조정
        StartCoroutine(ASkillCoolOut(skillManager.ASkillCoolTime));
    }

    //스킬 쿨타임
    IEnumerator ASkillCoolOut(float second)
    {
        yield return new WaitForSeconds(second);
        isASkill = false;
    }
    private void SSkill()
    {
        //키입력과 스킬 사용중이 아닐 때 스킬 사용 가능
        if (!sSkillInput || isSSkill)
            return;
        isSSkill = true;

        // 불렛 소환
        GameObject bullet = Instantiate(bulletPrefab[1]);
        bullet.GetComponent<BulletMain>().Direction = this.transform.localScale.x;//불렛 방향 설정
        bullet.transform.position = new Vector3(this.transform.position.x + this.transform.localScale.x, this.transform.position.y, 0);//소환 위치 조정
        StartCoroutine(SSkillCoolOut(skillManager.SSkillCoolTime));
    }

    //스킬 쿨타임
    IEnumerator SSkillCoolOut(float second)
    {
        yield return new WaitForSeconds(second);
        isSSkill = false;
    }

    private void DSkill()
    {
        //키입력과 스킬 사용중이 아닐 때 스킬 사용 가능
        if (!dSkillInput || isDSkill)
            return;
        isDSkill = true;

        // 불렛 소환
        GameObject bullet = Instantiate(bulletPrefab[2]);
        bullet.GetComponent<BulletMain>().Direction = this.transform.localScale.x;//불렛 방향 설정
        bullet.transform.position = new Vector3(this.transform.position.x + this.transform.localScale.x, this.transform.position.y, 0);//소환 위치 조정
        StartCoroutine(DSkillCoolOut(skillManager.DSkillCoolTime));
    }

    //스킬 쿨타임
    IEnumerator DSkillCoolOut(float second)
    {
        yield return new WaitForSeconds(second);
        isDSkill = false;
    }
}
