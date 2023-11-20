using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PutItem : MonoBehaviour
{
    public List<Sprite> Armor = new List<Sprite>();
    public List<Sprite> Containters = new List<Sprite>();
    public List<Sprite> Food = new List<Sprite>();
    public List<Sprite> Shields = new List<Sprite>();
    public List<Sprite> Hud = new List<Sprite>();
    public List<Sprite> Jewelry = new List<Sprite>();
    public List<Sprite> Gems = new List<Sprite>();
    public List<Sprite> Magic = new List<Sprite>();
    public List<Sprite> Misc = new List<Sprite>();
    public List<Sprite> Keys = new List<Sprite>();
    public List<Sprite> Potions = new List<Sprite>();
    public List<Sprite> Reagents = new List<Sprite>();
    public List<Sprite> Resources = new List<Sprite>();
    public List<Sprite> Tools = new List<Sprite>();
    public List<Sprite> Treasure = new List<Sprite>();
    public List<Sprite> Weapons = new List<Sprite>();
    public List<Sprite> Ranged = new List<Sprite>();
    public List<Sprite> Ammo = new List<Sprite>();


    public List<Image> ContentSlotImage;


    public void AddArmor()
    {
        foreach (Image image in ContentSlotImage)
        { image.enabled = false; }

        int i = 0;
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = true;
            image.sprite = Armor[i++];
            if (i == Armor.Count)
                return;
        }
    }

    public void AddShield()
    {
        foreach (Image image in ContentSlotImage)
        { image.enabled = false; }

        int i = 0;
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = true;
            image.sprite = Shields[i++];
            if (i == Shields.Count)
                return;
        }
    }

    public void AddContainters()
    {
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = false;
        }

        int i = 0;
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = true;
            image.sprite = Containters[i++];
            if (i == Containters.Count)
                return;
        }
    }

    public void AddFood()
    {
        foreach (Image image in ContentSlotImage)
        { image.enabled = false; }

        int i = 0;
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = true;
            image.sprite = Food[i++];
            if (i == Food.Count)
            return;
        }

    }

    public void AddJewelry()
    {
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = false;
        }

        int i = 0;
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = true;
            image.sprite = Jewelry[i++];
            if (i == Jewelry.Count)
                return;
        }
    }

    public void AddGems()
    {
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = false;
        }

        int i = 0;
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = true;
            image.sprite = Gems[i++];
            if (i == Gems.Count)
                return;
        }
    }

    public void AddMagic()
    {
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = false;
        }

        int i = 0;
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = true;
            image.sprite = Magic[i++];
            if (i == Magic.Count)
                return;
        }
    }

    public void AddMisc()
    {
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = false;
        }

        int i = 0;
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = true;
            image.sprite = Misc[i++];
            if (i == Misc.Count)
                return;
        }
    }

    public void AddHud()
    {
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = false;
        }

        int i = 0;
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = true;
            image.sprite = Hud[i++];
            if (i == Hud.Count)
                return;
        }
    }

    public void AddKeys()
    {
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = false;
        }

        int i = 0;
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = true;
            image.sprite = Keys[i++];
            if (i == Keys.Count)
                return;
        }
    }

    public void AddPotions()
    {
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = false;
        }

        int i = 0;
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = true;
            image.sprite = Potions[i++];
            if (i == Potions.Count)
                return;
        }
    }

    public void AddReagents()
    {
        foreach (Image image in ContentSlotImage)
        {
        image.enabled = false;
        }

        int i = 0;
        foreach (Image image in ContentSlotImage)
        {
        image.enabled = true;
        image.sprite = Reagents[i++];
        if (i == Reagents.Count)
            return;
        }
    }

    public void AddResources()
    {
        foreach (Image image in ContentSlotImage)
        {
        image.enabled = false;
        }

        int i = 0;
        foreach (Image image in ContentSlotImage)
        {
        image.enabled = true;
        image.sprite = Resources[i++];
        if (i == Resources.Count)
        return;
        }
    }

    public void AddTools()
    {
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = false;
        }

        int i = 0;
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = true;
            image.sprite = Tools[i++];
            if (i == Tools.Count)
                return;
        }
    }

    public void AddAmmo()
    {
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = false;
        }

        int i = 0;
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = true;
            image.sprite = Ammo[i++];
            if (i == Ammo.Count)
                return;
        }
    }

    public void AddRanged()
    {
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = false;
        }

        int i = 0;
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = true;
            image.sprite = Ranged[i++];
            if (i == Ranged.Count)
                return;
        }
    }

    public void AddTreasure()
    {
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = false;
        }

        int i = 0;
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = true;
                image.sprite = Treasure[i++];
                if (i == Treasure.Count)
            return;
        }
    }

    public void AddWeapons()
    {
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = false;
        }

        int i = 0;
        foreach (Image image in ContentSlotImage)
        {
            image.enabled = true;
            image.sprite = Weapons[i++];
            if (i == Weapons.Count)
                return;
        }
    }

}

