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

    /* -------------- 이벤트 함수 -------------- */
    private void Start()
    {
        skillManager = GameManager.instance.SkillManager;
        anim = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerController>();
        ultimateSkillAnchor = GetComponent<PlayerMain>().UltimateSkillAnchor;
    }

    /* --------------- 외부 참조 --------------- */
    public void UltimateSkill()
    {
        if (ultimateSkillAnchor.childCount <= 0 || isUltimateSkill) // 스킬 사용중이 아니고 궁스킬이 있을 때
            return;

        playerController.IsAttack = true;
        isUltimateSkill = true;
        anim.SetTrigger("doUltimateSkill");

        ultimateSkillAnchor.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.gray; //스킬 이미지 회색으로 전환

        StartCoroutine(DelayBullet(bulletPrefabs[2], 1.1f)); // 투사체 발사
        StartCoroutine(UltimateSkillCoolOut(skillManager.UltimateSkillActiveTime, skillManager.UltimateSkillCoolTime));

        UIManager.instance.SetUltimateSkillCoolTimeUI();
    }

    public void SkillA()
    {
        if (isSkillA) // 스킬 사용중이 아닐 때
            return;

        playerController.IsAttack = true;
        isSkillA = true;
        anim.SetTrigger("doSkillA");

        StartCoroutine(DelayBullet(bulletPrefabs[0], 0.2f)); // 투사체 발사
        StartCoroutine(SkillACoolOut(skillManager.SkillAActiveTime, skillManager.SkillACoolTime));
        
        UIManager.instance.SetSkillACoolTimeUI();
    }

    public void SkillS()
    {
        if (isSkillS) // 스킬 사용중이 아닐 때
            return;

        playerController.IsAttack = true;
        isSkillS = true;
        anim.SetTrigger("doSkillS");

        StartCoroutine(DelayBullet(bulletPrefabs[1], 0.2f)); // 투사체 발사
        StartCoroutine(SkillSCoolOut(skillManager.SkillSActiveTime, skillManager.SkillSCoolTime));

        UIManager.instance.SetSkillSCoolTimeUI();
    }

    public void SkillD()
    {
        if (isSkillD) // 스킬 사용중이 아닐 때
            return;

        playerController.IsAttack = true;
        isSkillD = true;
        anim.SetTrigger("doSkillD");

        StartCoroutine(SkillDCoolOut(skillManager.SkillDActiveTime, skillManager.SkillDCoolTime));

        UIManager.instance.SetSkillDCoolTimeUI();
    }

    /* ---------- 투사체 발사 코루틴 ----------- */
    private IEnumerator DelayBullet(GameObject prefab, float time)
    {
        yield return new WaitForSeconds(time);

        GameObject bullet = Instantiate(prefab, transform.position, transform.rotation);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.Shot((int)Mathf.Sign(transform.localScale.x));
    }

    /* ----- 애니메이션 시간/쿨타임 코루틴 ----- */
    private IEnumerator UltimateSkillCoolOut(float activeTime, float coolTime)
    {
        yield return new WaitForSeconds(activeTime);
        playerController.IsAttack = false;

        yield return new WaitForSeconds(coolTime - activeTime);
        isUltimateSkill = false;
        ultimateSkillAnchor.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private IEnumerator SkillACoolOut(float activeTime, float coolTime)
    {
        yield return new WaitForSeconds(activeTime);
        playerController.IsAttack = false;

        yield return new WaitForSeconds(coolTime - activeTime);
        isSkillA = false;
    }

    private IEnumerator SkillSCoolOut(float activeTime, float coolTime)
    {
        yield return new WaitForSeconds(activeTime);
        playerController.IsAttack = false;

        yield return new WaitForSeconds(coolTime - activeTime);
        isSkillS = false;
    }

    private IEnumerator SkillDCoolOut(float activeTime, float coolTime)
    {
        yield return new WaitForSeconds(activeTime);
        playerController.IsAttack = false;

        yield return new WaitForSeconds(coolTime - activeTime);
        isSkillD = false;
    }
}
