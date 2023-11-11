using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    private bool isSkill;
    private float skillTimer;

    [Header("오브젝트")]
    [SerializeField]
    private GameObject SkillPivot;
    // Start is called before the first frame update


    public bool IsSkill
    {
        get { return isSkill; }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f") && SkillPivot.transform.childCount > 0)
        {
            isSkill = true;
            SkillPivot.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        }

        if (isSkill)
        {
            skillTimer += Time.deltaTime;
            if (skillTimer >= this.GetComponent<SkillActive>().SkillCoolTime)
            {
                isSkill = false;
                skillTimer = 0;
                SkillPivot.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }

    }
}
