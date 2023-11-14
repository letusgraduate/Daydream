using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    /* ------------- 컴포넌트 변수 ------------- */
    private SkillManager skillManager;

    /* ------------ 스킬 확인 변수 ------------- */
    private bool isUltimateSkill;
    private bool isSkillA;
    private bool isSkillS;
    private bool isSkillD;

    /* ----------- 입력 값 저장 변수 ----------- */
    private bool ultimateSkillInput;
    private bool skillAInput;
    private bool skillSInput;
    private bool skillDInput;

    /* ---------------- 인스펙터 --------------- */
    [Header("오브젝트 연결")]
    [SerializeField]
    private GameObject skillManagerObject;
    [SerializeField]
    private GameObject[] bulletPrefab;

    /* ---------------- 프로퍼티 --------------- */
    public bool IsUltimateSkill
    {
        get { return isUltimateSkill; }
    }
    public bool IsSkillA
    {
        get { return isSkillA; }
    }

    public bool IsSkillS
    {
        get { return isSkillS; }
    }

    public bool IsSkillD
    {
        get { return isSkillD; }
    }

    /* -------------- 이벤트 함수 -------------- */
    void Update()
    {
        skillManager = skillManagerObject.GetComponent<SkillManager>();
        ultimateSkillInput = Input.GetKeyDown(KeyCode.F);
        skillAInput = Input.GetKeyDown(KeyCode.Z);
        skillSInput = Input.GetKeyDown(KeyCode.X);
        skillDInput = Input.GetKeyDown(KeyCode.C);

        /* 스킬 사용 */
        UltimateSkill();
        SkillA();
        SkillS();
        SkillD();
    }

    /* --------------- 기능 함수 --------------- */
    private void UltimateSkill()
    {
        // 키입력과 스킬 사용중이 아니고 궁스킬이 있을 때 스킬 사용 가능
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

    IEnumerator UltimateSkillCoolOut(float second)
    {
        yield return new WaitForSeconds(second);
        isUltimateSkill = false;
        skillManagerObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void SkillA()
    {
        // 키입력과 스킬 사용중이 아닐 때 스킬 사용 가능
        if (!skillAInput || isSkillA)
            return;

        isSkillA = true;

        /* 불릿 소환 */
        GameObject bullet = Instantiate(bulletPrefab[0]);
        bullet.GetComponent<BulletMain>().Direction = this.transform.localScale.x;//불렛 방향 설정
        bullet.transform.position = new Vector3(this.transform.position.x + this.transform.localScale.x, this.transform.position.y, 0);//소환 위치 조정
        StartCoroutine(SkillACoolOut(skillManager.SkillACoolTime));//불렛 방향
    }

    IEnumerator SkillACoolOut(float second)
    {
        yield return new WaitForSeconds(second);
        isSkillA = false;
    }

    private void SkillS()
    {
        // 키입력과 스킬 사용중이 아닐 때 스킬 사용 가능
        if (!skillSInput || isSkillS)
            return;

        isSkillS = true;

        /* 불릿 소환 */
        GameObject bullet = Instantiate(bulletPrefab[1]);
        bullet.GetComponent<BulletMain>().Direction = this.transform.localScale.x;//불렛 방향 설정
        bullet.transform.position = new Vector3(this.transform.position.x + this.transform.localScale.x, this.transform.position.y, 0);//소환 위치 조정
        StartCoroutine(SkillSCoolOut(skillManager.SkillSCoolTime));
    }

    IEnumerator SkillSCoolOut(float second)
    {
        yield return new WaitForSeconds(second);
        isSkillS = false;
    }

    private void SkillD()
    {
        // 키입력과 스킬 사용중이 아닐 때 스킬 사용 가능
        if (!skillDInput || isSkillD)
            return;

        isSkillD = true;

        /* 불릿 소환 */
        GameObject bullet = Instantiate(bulletPrefab[2]);
        bullet.GetComponent<BulletMain>().Direction = this.transform.localScale.x;//불렛 방향 설정
        bullet.transform.position = new Vector3(this.transform.position.x + this.transform.localScale.x, this.transform.position.y, 0);//소환 위치 조정
        StartCoroutine(SkillDCoolOut(skillManager.SkillDCoolTime));
    }

    IEnumerator SkillDCoolOut(float second)
    {
        yield return new WaitForSeconds(second);
        isSkillD = false;
    }
}
