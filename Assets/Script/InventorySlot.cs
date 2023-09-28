using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class InventorySlot : MonoBehaviour, IPointerUpHandler, IPointerEnterHandler
{
    public int slotnum;
    public Item item;
    public Image itemIcon;

    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
    }

    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        bool isUse = false;
        try
        {
            isUse = item.Use();
        } catch (NullReferenceException e)
        { }
        

        if (isUse)
        {
            Inventory.instance.RemoveItem(slotnum);
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        try
        {
            Debug.Log(item.Script());
        }
        catch (NullReferenceException e)
        {
            Debug.Log("false");
        }
    }
    
}
