using DG.Tweening;
using MyBox;
using StairwayTest.Gameplay;
using StairwayTest.SO;
using StairwayTest.Utilities;
using StairwayTest.Utilities.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace StairwayTest.Manager
{
    public class UIManager : SingletonPersistent<UIManager>, ILoadExternalClasses
    {
        private GameManager gameManager;

        [Foldout("Crafting UI"), SerializeField] private Button[] allCraftingBtn;
        [Foldout("Crafting UI"), SerializeField] private ItemTypeEnum itemCategoryToDisplay;
        [Foldout("Crafting UI"), SerializeField] private Color itemLockedColor;
        [Foldout("Crafting UI"), SerializeField] private Color itemUnlockedColor;

        [Foldout("Item Categories"), SerializeField] private Button allCategoriesBtn;
        [Foldout("Item Categories"), SerializeField] private Button weaponBtn;
        [Foldout("Item Categories"), SerializeField] private Button miningToolsBtn;
        [Foldout("Item Categories"), SerializeField] private Button farmingToolsBtn;
        [Foldout("Item Categories"), SerializeField] private Button otherToolsBtn;
        private Button lastSelectedBtn;


        void Start()
        {
            LoadExternalClassInstance();
            ShowCraftingItemBtns(true);
            CategoriesBtnAddListener();
        }

        public void LoadExternalClassInstance()
        {
            gameManager = GameManager.Instance;
        }

        private void ShowCraftingItemBtns(bool _showAll)
        {
            for (int i = 0; i < allCraftingBtn.Length; i++)
            {
                ItemSO itemSO = gameManager.AllItemSO[i];
                Image itemImg = allCraftingBtn[i].transform.GetChild(0).GetComponent<Image>();

                itemImg.sprite = itemSO.itemSprite;
                itemImg.color = itemSO.isItemUnlocked ? itemUnlockedColor : itemLockedColor;

                if (_showAll)
                    allCraftingBtn[i].gameObject.SetActive(true);
                else
                    allCraftingBtn[i].gameObject.SetActive(itemSO.itemType == itemCategoryToDisplay);

                allCraftingBtn[i].GetComponent<RectTransform>().DOPunchScale(new Vector3(.15f, .15f), .25f);
            }
        }

        private void ShowItemByCategory(ItemTypeEnum _itemType)
        {
            itemCategoryToDisplay = _itemType;
            ShowCraftingItemBtns(false);
        }

        private void CategoriesBtnAddListener()
        {
            allCategoriesBtn.onClick.RemoveAllListeners();
            weaponBtn.onClick.RemoveAllListeners();
            miningToolsBtn.onClick.RemoveAllListeners();
            farmingToolsBtn.onClick.RemoveAllListeners();
            otherToolsBtn.onClick.RemoveAllListeners();

            allCategoriesBtn.onClick.AddListener(() => { ShowCraftingItemBtns(true); });
            weaponBtn.onClick.AddListener(() => { ShowItemByCategory(ItemTypeEnum.WEAPON); });
            miningToolsBtn.onClick.AddListener(() => { ShowItemByCategory(ItemTypeEnum.MINING_TOOLS); });
            farmingToolsBtn.onClick.AddListener(() => { ShowItemByCategory(ItemTypeEnum.FARMING_TOOLS); });
            otherToolsBtn.onClick.AddListener(() => { ShowItemByCategory(ItemTypeEnum.OTHERS); });
        }

        public void SetLastSelectedButton(Button _button) => lastSelectedBtn = _button;

        public void SelectLastSelectedButton() => lastSelectedBtn.Select();
    }
}
