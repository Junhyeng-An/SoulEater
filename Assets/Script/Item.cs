using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment,
    Consumables,
    Enchant
}


[System.Serializable]
public class Item
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;
    public List<ItemEffect> effect;
    public bool Use()
    {
        bool isUsed = false;
        foreach (ItemEffect eft in effect)
        {
            isUsed = eft.ExcuteRole();
        }

        return isUsed;
    }
    
}
