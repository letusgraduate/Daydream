using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private ItemManager itemManager;

    void Start()
    {
        itemManager = GameManager.instance.ItemManager;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UIManager.instance.SetItemSelect();
        }
        if (Input.GetKeyDown(KeyCode.Q) && itemManager.ItemStock > 0) //아이템 버리기
        {
            itemManager.ItemStock--;
            itemManager.RemoveItemList(UIManager.instance.ItemSelect);
            UIManager.instance.SetItemSelect();
        }
        if (Input.GetKeyDown(KeyCode.W) && itemManager.ItemStock > 0 && itemManager.GetIsUsisngItem(UIManager.instance.ItemSelect)) //아이템 사용
        {
            itemManager.ItemStock--;
            itemManager.UseItemList(UIManager.instance.ItemSelect);
            UIManager.instance.SetItemSelect();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && itemManager.ItemStock > 0)
        {
            if (!itemManager.GetIsUsisngItem(0))
                return;
            itemManager.ItemStock--;
            itemManager.UseItemList(0);
            UIManager.instance.SetItemSelect();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && itemManager.ItemStock > 1)
        {
            if (!itemManager.GetIsUsisngItem(1))
                return;
            itemManager.ItemStock--;
            itemManager.UseItemList(1);
            UIManager.instance.SetItemSelect();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && itemManager.ItemStock > 2)
        {
            if (!itemManager.GetIsUsisngItem(2))
                return;
            itemManager.ItemStock--;
            itemManager.UseItemList(2);
            UIManager.instance.SetItemSelect();
        }
    }
}
