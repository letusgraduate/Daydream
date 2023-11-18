using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    /* ------------- 컴포넌트 변수 ------------- */
    private PlayerMain playerMain;

    /* ---------------- 인스펙터 --------------- */
    [Header("현재 아이템 수")]
    [SerializeField, Range(0, 3)]
    private int itemStock = 0;
    [SerializeField]
    private List<Sprite> itemImages = new List<Sprite>();
    [SerializeField]
    private List<bool> isActiveItems = new List<bool>();
    [SerializeField]
    private List<int> itemNums = new List<int>();

    /* ---------------- 프로퍼티 --------------- */
    public int ItemStock
    {
        get { return itemStock; }
        set
        {
            itemStock = value;
            UIManager.instance.ShowItemUI();
        }
    }

    /* -------------- 이벤트 함수 -------------- */
    private void Start()
    {
        playerMain = GameManager.instance.Player.GetComponent<PlayerMain>();
    }

    /* --------------- 외부 참조 --------------- */
    public void ItemList(int count, Sprite itemImage, bool isUsisngItem, int itemNum)
    {
        itemImages.Add(itemImage);
        isActiveItems.Add(isUsisngItem);
        itemNums.Add(itemNum);
    }

    public Sprite GetItemImages(int num)
    {
        return itemImages[num];
    }

    public bool GetIsUsisngItem(int num)
    {
        return isActiveItems[num];
    }

    /* --------------- 콜백 함수 --------------- */
    public void RemoveItemList(int num)
    {
        if (!isActiveItems[num])
            RemovePassiveItem(itemNums[num]);

        itemImages.RemoveAt(num);
        isActiveItems.RemoveAt(num);
        itemNums.RemoveAt(num);

        UIManager.instance.SetItemUI();
        UIManager.instance.ShowItemUI();
    }

    public void UseItemList(int num)
    {
        ActiveItem(itemNums[num]);

        itemImages.RemoveAt(num);
        isActiveItems.RemoveAt(num);
        itemNums.RemoveAt(num);

        UIManager.instance.SetItemUI();
        UIManager.instance.ShowItemUI();
    }

    public void PassiveItem(int num)
    {
        switch (num)
        {
            case 0:
                MaxHPUpgrade(true);
                break;
            default:
                break;
        }
    }

    public void RemovePassiveItem(int num)
    {
        switch (num)
        {
            case 0:
                MaxHPUpgrade(false);
                break;
            default:
                break;
        }
    }

    public void ActiveItem(int num)
    {
        switch (num)
        {
            case 0:
                HPHeal();
                break;
            default:
                break;
        }
    }
    
    /* -------------- 아이템 효과 -------------- */
    public void HPHeal()
    {
        playerMain.Hp += 20;
    }

    public void MaxHPUpgrade(bool use)
    {
        if (use)
        {
            playerMain.MaxHp += 20;
        }
        else
        {
            playerMain.MaxHp -= 20;
        }
    }
}

