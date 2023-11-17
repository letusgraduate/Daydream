using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    /* ------------- 컴포넌트 변수 ------------- */
    private GameObject player;
    private PlayerMain playerMain;
    /* ---------------- 인스펙터 --------------- */
    [Header("현재 아이템 수")]
    [SerializeField, Range(0, 3)]
    private int itemCount = 0;
    [SerializeField]
    private List<Sprite> itemImages = new List<Sprite>();
    [SerializeField]
    private List<bool> isUsisngItems = new List<bool>();
    [SerializeField]
    private List<int> itemNums = new List<int>();

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
    public void ItemList(int count, Sprite itemImage, bool isUsisngItem, int itemNum)
    {
        itemImages.Add(itemImage);
        isUsisngItems.Add(isUsisngItem);
        itemNums.Add(itemNum);
    }
    private void Start()
    {
        player = GameManager.instance.Player;
        playerMain = player.GetComponent<PlayerMain>();
    }
    /* --------------- 외부 참조 --------------- */
    public Sprite GetItemImages(int num)
    {
        return itemImages[num];
    }
    public bool GetIsUsisngItem(int num)
    {
        return isUsisngItems[num];
    }
    /* --------------- 콜백 함수 --------------- */
    public void RemoveItemList(int num)
    {
        if (!isUsisngItems[num])
        {
            RemovePassiveItem(itemNums[num]);
        }
        itemImages.RemoveAt(num);
        isUsisngItems.RemoveAt(num);
        itemNums.RemoveAt(num);
        UIManager.instance.SetItemUI();
        UIManager.instance.ShowItemUI();
    }
    public void UseItemList(int num)
    {
        ActiveItem(itemNums[num]);
        itemImages.RemoveAt(num);
        isUsisngItems.RemoveAt(num);
        itemNums.RemoveAt(num);
        UIManager.instance.SetItemUI();
        UIManager.instance.ShowItemUI();
    }

    public void PassiveItem(int num)
    {
        switch (num)
        {
            case 0:
                MapHPUpgrade(true);
                break;
        }
    }
    public void RemovePassiveItem(int num)
    {
        switch (num)
        {
            case 0:
                MapHPUpgrade(false);
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
        }
    }

    /* --------------- 아이템 효과--------------- */
    public void HPHeal()
    {
        playerMain.Hp += 20;
    }
    public void MapHPUpgrade(bool use)
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

