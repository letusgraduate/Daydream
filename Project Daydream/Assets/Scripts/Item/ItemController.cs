using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    /* ------------- 컴포넌트 변수 ------------- */
    private ItemManager itemManager;

    /* -------------- 이벤트 함수 -------------- */
    void Start()
    {
        itemManager = GameManager.instance.ItemManager;
    }

    /* --------------- 외부 참조 --------------- */
    public void SelectItem()
    {
        UIManager.instance.SetItemSelect();
    }

    public void DropItem()
    {
        if (itemManager.ItemStock > 0) //아이템 버리기
        {
            itemManager.ItemStock--;
            itemManager.RemoveItemList(UIManager.instance.ItemSelect);
            UIManager.instance.SetItemSelect();
        }
    }

    public void UseSelectItem()
    {
        if (itemManager.ItemStock > 0 && itemManager.GetIsActiveItem(UIManager.instance.ItemSelect))
        {
            itemManager.ItemStock--;
            itemManager.UseItemList(UIManager.instance.ItemSelect);
            UIManager.instance.SetItemSelect();
        }
    }

    public void UseItem1()
    {
        if (itemManager.ItemStock > 0 && itemManager.GetIsActiveItem(0))
        {
            itemManager.ItemStock--;
            itemManager.UseItemList(0);
            UIManager.instance.SetItemSelect();
        }
    }

    public void UseItem2()
    {
        if (itemManager.ItemStock > 1 && itemManager.GetIsActiveItem(1))
        {
            itemManager.ItemStock--;
            itemManager.UseItemList(1);
            UIManager.instance.SetItemSelect();
        }
    }

    public void UseItem3()
    {
        if (itemManager.ItemStock > 2 && itemManager.GetIsActiveItem(2))
        {
            itemManager.ItemStock--;
            itemManager.UseItemList(2);
            UIManager.instance.SetItemSelect();
        }
    }
}
