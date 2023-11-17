using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private ItemManager itemManager;
    // Start is called before the first frame update
    void Start()
    {
        itemManager = GameManager.instance.ItemManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UIManager.instance.ItemPickCountSettings();
            UIManager.instance.SETItemPick();
        }
        //아이템 버리기
        if (Input.GetKeyDown(KeyCode.Q) && itemManager.ItemCount > 0)
        {
            itemManager.ItemCount--;
            itemManager.RemoveItemList(UIManager.instance.ItemPickCount);
            UIManager.instance.ItemPickCountSettings();
            UIManager.instance.SETItemPick();
        }
        //아이템 사용
        if (Input.GetKeyDown(KeyCode.W) && itemManager.ItemCount > 0 && itemManager.GetIsUsisngItem(UIManager.instance.ItemPickCount))
        {
            itemManager.ItemCount--;
            itemManager.UseItemList(UIManager.instance.ItemPickCount);
            UIManager.instance.ItemPickCountSettings();
            UIManager.instance.SETItemPick();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && itemManager.ItemCount > 0)
        {
            if (!itemManager.GetIsUsisngItem(0))
                return;
            itemManager.ItemCount--;
            itemManager.UseItemList(0);
            UIManager.instance.ItemPickCountSettings();
            UIManager.instance.SETItemPick();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && itemManager.ItemCount > 1)
        {
            if (!itemManager.GetIsUsisngItem(1))
                return;
            itemManager.ItemCount--;
            itemManager.UseItemList(1);
            UIManager.instance.ItemPickCountSettings();
            UIManager.instance.SETItemPick();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && itemManager.ItemCount > 2)
        {
            if (!itemManager.GetIsUsisngItem(2))
                return;
            itemManager.ItemCount--;
            itemManager.UseItemList(2);
            UIManager.instance.ItemPickCountSettings();
            UIManager.instance.SETItemPick();
        }
    }
}
