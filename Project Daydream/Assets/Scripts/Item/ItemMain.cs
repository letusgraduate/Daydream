using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMain : MonoBehaviour
{
    /* ---------------- 인스펙터 --------------- */
    [Header("이미지 정보")]
    [SerializeField]
    private Sprite itemImage;

    [Header("아이템 번호")]
    [SerializeField, Range(0, 100)]
    private int itemNum;

    [Header("사용 아이템 유무")]
    [SerializeField]
    private bool isUsisngItem;

    /* ---------------- 프로퍼티 --------------- */
    public Sprite ItemImage
    {
        get { return itemImage; }
    }
    public int ItemNum
    {
        get { return itemNum; }
    }

    public bool IsUsisngItem
    {
        get { return isUsisngItem; }
    }
}
