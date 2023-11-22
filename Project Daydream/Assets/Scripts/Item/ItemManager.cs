using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    /* ------------- 컴포넌트 변수 ------------- */
    private PlayerMain playerMain;
    private SkillController skillController;

    /* ------------- 부활 변수 ------------- */
    private bool resurrection = false;
    /* ---------------- 인스펙터 --------------- */
    [Header("현재 아이템 수")]
    [SerializeField, Range(0, 3)]
    private int itemStock = 0;
    [SerializeField, Range(0, 3)]
    private int maxItemCount = 1;
    [SerializeField]
    private List<Sprite> itemImages = new List<Sprite>();
    [SerializeField]
    private List<bool> isActiveItems = new List<bool>();
    [SerializeField]
    private List<int> itemNums = new List<int>();

    /* ---------------- 프로퍼티 --------------- */
    public bool Resurrection
    {
        get { return resurrection; }
        set { resurrection = value; }
    }
    public int ItemStock
    {
        get { return itemStock; }
        set
        {
            itemStock = value;
            UIManager.instance.ShowItemUI();
        }
    }
    public int MaxItemCount
    {
        get { return maxItemCount; }
        set
        {
            maxItemCount = value;
            UIManager.instance.SetItemMaxUI();
        }
    }

    /* -------------- 이벤트 함수 -------------- */
    private void Start()
    {
        playerMain = GameManager.instance.Player.GetComponent<PlayerMain>();
        skillController = GameManager.instance.Player.GetComponent<SkillController>();
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

    public bool GetIsActiveItem(int num)
    {
        if (isActiveItems.Count > num)
            return isActiveItems[num];
        else
            return false;
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
            case 1:
                PoewrUpgrade(true);
                break;
            case 2:
                ResurrectionItem(true);
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
            case 1:
                PoewrUpgrade(false);
                break;
            case 2:
                ResurrectionItem(false);
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
            case 1:
                PoewrPotion();
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

    public void PoewrPotion()
    {
        skillController.DamageMultiple += 2;
        StartCoroutine(PoewrPotionTime());
    }

    private IEnumerator PoewrPotionTime()
    {
        yield return new WaitForSeconds(15f);
        skillController.DamageMultiple -= 2;
    }

    public void MaxHPUpgrade(bool use)
    {
        if (use)
            playerMain.MaxHp += 20;
        else
            playerMain.MaxHp -= 20;
    }

    public void PoewrUpgrade(bool use)
    {
        if (use)
            skillController.DamageMultiple += 1;
        else
            skillController.DamageMultiple -= 1;
    }

    public void ResurrectionItem(bool use)
    {
        if (use)
            resurrection = true;
        else
            resurrection = false;
    }

    public void UseResurrectionItem()
    {
        ItemStock--;
        ResurrectionItem(false);
        RemoveItemList(itemNums.IndexOf(2));
    }
}
