using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    SpriteRenderer sr;
    public GameObject go;

    [Header("설정")]
    [SerializeField, Range(0f, 10f)]
    public float SkillCoolTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        sr = go.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
            sr.material.color = Color.blue;

    }
}
