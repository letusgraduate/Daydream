using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMain : MonoBehaviour
{
    /* ---------------- 인스펙터 --------------- */
    [Header("데미지 설정")]
    [SerializeField]
    private int damage = 10;

    /* ---------------- 프로퍼티 --------------- */
    public int Damage { get { return damage; } private set { damage = value; } }
}
