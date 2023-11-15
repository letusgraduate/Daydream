using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    /* ------------- 컴포넌트 변수 ------------- */
    private SkillManager skillManager;
    private Transform ultimateSkillAnchor;
    private Animator anim;
    private PlayerController playerController;

    /* ------------ 스킬 확인 변수 ------------- */
    private bool isUltimateSkill = false;
    private bool isSkillA = false;
    private bool isSkillS = false;
    private bool isSkillD = false;

    /* ---------------- 인스펙터 --------------- */
    [Header("오브젝트 연결")]
    [SerializeField]
    private GameObject[] bulletPrefabs;

    /* ---------------- 프로퍼티 --------------- */
    public bool IsUltimateSkill { get { return isUltimateSkill; } }
    public bool IsSkillA { get { return isSkillA; } }
    public bool IsSkillS { get { return isSkillS; } }
    public bool IsSkillD { get { return isSkillD; } }
    public bool IsSkillAttack { get; }

    /* -------------- 이벤트 함수 -------------- */
    private void Start()
    {
        ultimateSkillAnchor = GetComponent<PlayerMain>().UltimateSkillAnchor;
        skillManager = GameManager.instance.SkillManager;
        anim = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    /* --------------- 외부 참조 --------------- */
    public void UltimateSkill()
    {
        if (ultimateSkillAnchor.childCount <= 0 || isUltimateSkill) // 스킬 사용중이 아니고 궁스킬이 있을 때
            return;

        playerController.IsAttack = true;
        isUltimateSkill = true;
        anim.SetTrigger("doSkillD-1");

        ultimateSkillAnchor.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.gray; //스킬 이미지 회색으로 전환

        /* 투사체 발사 */
        GameObject bullet = Instantiate(bulletPrefabs[3], transform.position, transform.rotation);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.Shot((int)Mathf.Sign(transform.localScale.x));

        StartCoroutine(UltimateSkillCoolOut(skillManager.UltimateSkillActiveTime, skillManager.UltimateSkillCoolTime));
    }

    public void SkillA()
    {
        if (isSkillA) // 스킬 사용중이 아닐 때
            return;

        playerController.IsAttack = true;
        isSkillA = true;
        anim.SetTrigger("doSkillA");

        /* 투사체 발사 */
        GameObject bullet = Instantiate(bulletPrefabs[0], transform.position, transform.rotation);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.Shot((int)Mathf.Sign(transform.localScale.x));

        StartCoroutine(SkillACoolOut(skillManager.SkillAActiveTime, skillManager.SkillACoolTime));
    }

    public void SkillS()
    {
        if (isSkillS) // 스킬 사용중이 아닐 때
            return;

        playerController.IsAttack = true;
        isSkillS = true;
        anim.SetTrigger("doSkillS");

        /* 투사체 발사 */
        GameObject bullet = Instantiate(bulletPrefabs[1], transform.position, transform.rotation);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.Shot((int)Mathf.Sign(transform.localScale.x));

        StartCoroutine(SkillSCoolOut(skillManager.SkillSActiveTime, skillManager.SkillSCoolTime));
    }

    public void SkillD()
    {
        if (isSkillD) // 스킬 사용중이 아닐 때
            return;

        playerController.IsAttack = true;
        isSkillD = true;
        anim.SetTrigger("doSkillD");

        /* 투사체 발사 */
        GameObject bullet = Instantiate(bulletPrefabs[2], transform.position, transform.rotation);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.Shot((int)Mathf.Sign(transform.localScale.x));

        StartCoroutine(SkillDCoolOut(skillManager.SkillDActiveTime, skillManager.SkillDCoolTime));
    }

    /* ----- 애니메이션 시간/쿨타임 코루틴 ----- */
    private IEnumerator UltimateSkillCoolOut(float activeTime, float coolTime)
    {
        yield return new WaitForSeconds(activeTime);
        playerController.IsAttack = false;

        yield return new WaitForSeconds(coolTime);
        isUltimateSkill = false;
        ultimateSkillAnchor.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private IEnumerator SkillACoolOut(float activeTime, float coolTime)
    {
        yield return new WaitForSeconds(activeTime);
        playerController.IsAttack = false;

        yield return new WaitForSeconds(coolTime);
        isSkillA = false;
    }

    private IEnumerator SkillSCoolOut(float activeTime, float coolTime)
    {
        yield return new WaitForSeconds(activeTime);
        playerController.IsAttack = false;

        yield return new WaitForSeconds(coolTime);
        isSkillS = false;
    }

    private IEnumerator SkillDCoolOut(float activeTime, float coolTime)
    {
        yield return new WaitForSeconds(activeTime);
        playerController.IsAttack = false;

        yield return new WaitForSeconds(coolTime);
        isSkillD = false;
    }
}
