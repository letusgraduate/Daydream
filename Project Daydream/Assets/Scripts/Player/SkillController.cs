using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    /* ------------- 컴포넌트 변수 ------------- */
    private SkillManager skillManager;
    private Transform ultimateSkillAnchor;

    /* ------------ 스킬 확인 변수 ------------- */
    private bool isUltimateSkill;
    private bool isSkillA;
    private bool isSkillS;
    private bool isSkillD;

    /* ---------------- 인스펙터 --------------- */
    [Header("오브젝트 연결")]
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
    private void Start()
    {
        ultimateSkillAnchor = GetComponent<PlayerMain>().UltimateSkillAnchor;
        skillManager = GameManager.instance.SkillManager;
    }

    /* --------------- 외부 참조 --------------- */
    public void UltimateSkill()
    {
        // 스킬 사용중이 아니고 궁스킬이 있을 때
        if (ultimateSkillAnchor.childCount <= 0 || isUltimateSkill)
            return;

        isUltimateSkill = true;
        ultimateSkillAnchor.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.gray; //스킬 이미지 회색으로 전환

        // 불렛 소환
        GameObject bullet = Instantiate(bulletPrefab[3]);
        bullet.GetComponent<BulletMain>().Direction = this.transform.localScale.x; //불렛 방향 설정
        bullet.transform.position = new Vector2(this.transform.position.x + this.transform.localScale.x, this.transform.position.y);//소환 위치 조정
        StartCoroutine(UltimateSkillCoolOut(skillManager.UltimateSkillCoolTime));
    }

    public void SkillA()
    {
        if (isSkillA) // 스킬 사용중이 아닐 때
            return;

        isSkillA = true;

        /* 불릿 소환 */
        GameObject bullet = Instantiate(bulletPrefab[0]);
        bullet.GetComponent<BulletMain>().Direction = this.transform.localScale.x; //불렛 방향 설정
        bullet.transform.position = new Vector2(this.transform.position.x + this.transform.localScale.x, this.transform.position.y);//소환 위치 조정
        StartCoroutine(SkillACoolOut(skillManager.SkillACoolTime)); //불렛 방향
    }

    public void SkillS()
    {
        if (isSkillS) // 스킬 사용중이 아닐 때
            return;

        isSkillS = true;

        /* 불릿 소환 */
        GameObject bullet = Instantiate(bulletPrefab[1]);
        bullet.GetComponent<BulletMain>().Direction = this.transform.localScale.x; //불렛 방향 설정
        bullet.transform.position = new Vector2(this.transform.position.x + this.transform.localScale.x, this.transform.position.y);//소환 위치 조정
        StartCoroutine(SkillSCoolOut(skillManager.SkillSCoolTime));
    }

    public void SkillD()
    {
        if (isSkillD) // 스킬 사용중이 아닐 때
            return;

        isSkillD = true;

        /* 불릿 소환 */
        GameObject bullet = Instantiate(bulletPrefab[2]);
        bullet.GetComponent<BulletMain>().Direction = this.transform.localScale.x; //불렛 방향 설정
        bullet.transform.position = new Vector2(this.transform.position.x + this.transform.localScale.x, this.transform.position.y);//소환 위치 조정
        StartCoroutine(SkillDCoolOut(skillManager.SkillDCoolTime));
    }

    /* ------------- 쿨타임 코루틴 ------------- */
    private IEnumerator UltimateSkillCoolOut(float second)
    {
        yield return new WaitForSeconds(second);
        isUltimateSkill = false;
        ultimateSkillAnchor.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private IEnumerator SkillACoolOut(float second)
    {
        yield return new WaitForSeconds(second);
        isSkillA = false;
    }

    private IEnumerator SkillSCoolOut(float second)
    {
        yield return new WaitForSeconds(second);
        isSkillS = false;
    }

    private IEnumerator SkillDCoolOut(float second)
    {
        yield return new WaitForSeconds(second);
        isSkillD = false;
    }
}
