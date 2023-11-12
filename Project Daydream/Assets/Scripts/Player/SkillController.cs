using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    private SkillManager skillManager;
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
    private GameObject skillManagerObject;
    [SerializeField]
    private GameObject[] bulletPrefab;
    // Start is called before the first frame update

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


    // Update is called once per frame
    void Update()
    {
        skillManager = skillManagerObject.GetComponent<SkillManager>();
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
        if (!ultimateSkillInput || skillManagerObject.transform.childCount <= 0 || isUltimateSkill)
            return;
        isUltimateSkill = true;
        skillManagerObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        GameObject bullet = Instantiate(bulletPrefab[3]);
        bullet.GetComponent<BulletMain>().Direction = this.transform.localScale.x;
        bullet.transform.position = new Vector3(this.transform.position.x + this.transform.localScale.x, this.transform.position.y, 0);
        StartCoroutine(UltimateSkillCoolOut(skillManager.UltimateSkillCoolTime));
    }

    IEnumerator UltimateSkillCoolOut(float second)
    {
        yield return new WaitForSeconds(second);
        isUltimateSkill = false;
        skillManagerObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void ASkill()
    {
        if (!aSkillInput || isASkill)
            return;
        isASkill = true;
        Debug.Log("ASkill Start");
        GameObject bullet = Instantiate(bulletPrefab[0]);
        bullet.GetComponent<BulletMain>().Direction = this.transform.localScale.x;
        bullet.transform.position = new Vector3(this.transform.position.x + this.transform.localScale.x, this.transform.position.y, 0);
        StartCoroutine(ASkillCoolOut(skillManager.ASkillCoolTime));
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
        GameObject bullet = Instantiate(bulletPrefab[1]);
        bullet.GetComponent<BulletMain>().Direction = this.transform.localScale.x;
        bullet.transform.position = new Vector3(this.transform.position.x + this.transform.localScale.x, this.transform.position.y, 0);
        StartCoroutine(SSkillCoolOut(skillManager.SSkillCoolTime));
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
        GameObject bullet = Instantiate(bulletPrefab[2]);
        bullet.GetComponent<BulletMain>().Direction = this.transform.localScale.x;
        bullet.transform.position = new Vector3(this.transform.position.x + this.transform.localScale.x, this.transform.position.y, 0);
        StartCoroutine(DSkillCoolOut(skillManager.DSkillCoolTime));
    }

    IEnumerator DSkillCoolOut(float second)
    {
        yield return new WaitForSeconds(second);
        Debug.Log("DSkill End");
        isDSkill = false;
    }
}
