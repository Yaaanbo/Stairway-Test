using MyBox;
using StairwayTest.SO;
using StairwayTest.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace StairwayTest.Manager
{
    public class InventoryManager : SingletonPersistent<InventoryManager>
    {
        [Foldout("Owned Item", true)]
        [SerializeField] private List<ItemSO> ownedItemList = new List<ItemSO>();
        public List<ItemSO> OwnedItemList => ownedItemList;

        [Foldout("Inventory Slots")]
        [SerializeField] private int maxInventorySlot = 15;

        public bool IsInventoryFull => ownedItemList.Count >= maxInventorySlot;

        public void AddItem(ItemSO _item)
        {
            _item.itemAmountInInventory++;
            if (ownedItemList.Contains(_item) && _item.isStackable) return;

            ownedItemList.Add(_item);
        }

        public void RemoveItem(ItemSO _item, int _amountToSubtract)
        {
            _item.itemAmountInInventory -= _amountToSubtract;

            if (_item.itemAmountInInventory <= 0)
            {
                _item.itemAmountInInventory = 0;
                ownedItemList.Remove(_item);
            }
        }
    }
}
