using MyBox;
using StairwayTest.SO;
using StairwayTest.Utilities.Interface;
using UnityEngine;

namespace StairwayTest.Manager
{
    public class TestingManager : MonoBehaviour, ILoadExternalClasses
    {
        private GameManager gameManager;
        private InventoryManager inventoryManager;
        private UIManager uiManager;

        [Foldout("Key Code", true)]
        [SerializeField] private KeyCode unlockAllItemKeyCode = KeyCode.F1;
        [SerializeField] private KeyCode infinityResourcesKeyCode = KeyCode.F2;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            LoadExternalClassInstance();
        }

        // Update is called once per frame
        void Update()
        {
            UnlockAllItemHandler();
            InfinityResourceHandler();
        }

        private void UnlockAllItemHandler()
        {
            if (Input.GetKeyDown(unlockAllItemKeyCode))
            {
                foreach (var item in gameManager.AllItemSO)
                {
                    item.isItemUnlocked = true;
                }
                uiManager.ShowCraftingItemBtns(true);
            }
        }

        private void InfinityResourceHandler()
        {
            if(Input.GetKeyDown(infinityResourcesKeyCode))
            {
                foreach(var item in inventoryManager.OwnedItemList)
                {
                    if(item.isStackable)
                    {
                        item.itemAmountInInventory += 20;
                    }
                }
                uiManager.ShowInventory();
            }
        }

        public void LoadExternalClassInstance()
        {
            gameManager = GameManager.Instance;
            uiManager = UIManager.Instance;
            inventoryManager = InventoryManager.Instance;
        }
    }
}
