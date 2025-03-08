using MyBox;
using StairwayTest.SO;
using StairwayTest.Utilities;
using StairwayTest.Utilities.Interface;
using System;
using UnityEngine;

namespace StairwayTest.Manager
{
    public class CraftingManager : SingletonPersistent<CraftingManager>, ILoadExternalClasses
    {
        private InventoryManager inventoryManager;
        private GameManager gameManager;

        public Action onItemCrafted;

        private void Start()
        {
            LoadExternalClassInstance();
        }

        public void CraftItem(ItemSO _itemToCraft)
        {
            if (inventoryManager.IsInventoryFull || !_itemToCraft.isItemUnlocked)
            {
                Debug.Log("Craft : Inventory Full Or Item Isn't Available");
                return;
            }

            ItemSO itemTocraft = gameManager.AllItemSO.Find(x => x == _itemToCraft);

            for (int i = 0; i < _itemToCraft.itemRecipe.Length; i++)
            {
                ItemSO itemNeeded = _itemToCraft.itemRecipe[i].rawItemSO;
                int amountInInventory = itemNeeded.itemAmountInInventory;

                if (amountInInventory < _itemToCraft.itemRecipe[i].amountNeeded || !inventoryManager.OwnedItemList.Contains(itemNeeded))
                {
                    Debug.Log("Craft : Not Enough Material");
                    return;
                }
                inventoryManager.RemoveItem(itemNeeded, itemTocraft.itemRecipe[i].amountNeeded);
            }
            Debug.Log("Craft : Item Crafted Succesfully");
            inventoryManager.AddItem(itemTocraft);
            onItemCrafted?.Invoke();
        }

        public void LoadExternalClassInstance()
        {
            inventoryManager = InventoryManager.Instance;
            gameManager = GameManager.Instance;
        }
    }
}
