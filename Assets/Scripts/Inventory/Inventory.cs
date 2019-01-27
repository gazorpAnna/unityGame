using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializedField] List<Item> items;
    [SerializedField] ItemSlot[] itemSLots;
    [SerializedField] Transform itemsParent;

    private void OnValidate()
    {
        if (itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentInChlidren<ItemSLot>();
        }
        RefreshUI();
    }
    private void RefreshUI()
    {
        int i = 0;
        for (; i < items.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = items[i];
        }

        for (; i < itemSlots.Length; i++)
        {
            itemSLots[i].Item = null;
        }

    }
    public bool AddItem(ItemSlot item)
    {
        if (IsFull())
            return false;

        itemsParent.Add(item);
        RefreshUI();
        return true;
    }

    public bool RemoveItem(ItemSlot item)
    {
        if (items.Remove(item))
        {
            RefreshUI();
            return true;

        }
        return false;
    }

    public bool isFull()
    {
        return itemsParent.Count >= itemsSlots.Length;
    }

}