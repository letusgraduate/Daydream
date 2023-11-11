using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillActive : MonoBehaviour
{
    [Header("오브젝트")]
    [SerializeField]
    private GameObject[] SkillPrefabs; // GameObject 배열로 수정

    [SerializeField]
    private GameObject SkillPivot;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("SkillItem"))
        {
            if (SkillPivot.transform.childCount > 0)
            {
                Destroy(SkillPivot.transform.GetChild(0).gameObject);
            }

            int skillNumber = other.gameObject.GetComponent<SkillItem>().SkillNumber;
            GameObject skill = Instantiate(SkillPrefabs[skillNumber], SkillPivot.transform);
            skill.transform.localPosition = Vector3.zero;
            Destroy(other.gameObject);
        }
    }

    public class SkillItem : MonoBehaviour
    {
        public int SkillNumber;
    }
}