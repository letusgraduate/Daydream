using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    /* ---------------- 인스펙터 --------------- */
    [Header("현재 아이템 수")]
    [SerializeField, Range(0, 3)]
    private int itemCount = 0;

    /* ---------------- 프로퍼티 --------------- */
    public int ItemCount
    {
        get { return itemCount; }
        set
        {
            itemCount = value;
            UIManager.instance.ShowItemUI();
        }
    }
}
