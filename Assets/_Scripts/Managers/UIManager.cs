using DG.Tweening;
using MyBox;
using StairwayTest.Gameplay;
using StairwayTest.SO;
using StairwayTest.Utilities;
using StairwayTest.Utilities.Interface;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StairwayTest.Manager
{
    public class UIManager : SingletonPersistent<UIManager>, ILoadExternalClasses
    {
        private InventoryManager inventoryManager;

        [Foldout("Crafting UI", true)]
        [SerializeField] private CraftingButtonBehaviour[] allCraftingBtn;
        [SerializeField] private ItemTypeEnum itemCategoryToDisplay;
        [SerializeField] private Color itemLockedColor;
        [SerializeField] private Color itemUnlockedColor;

        [Foldout("Item Categories UI", true)]
        [SerializeField] private Button allCategoriesBtn;
        [SerializeField] private Button weaponBtn;
        [SerializeField] private Button miningToolsBtn;
        [SerializeField] private Button farmingToolsBtn;
        [SerializeField] private Button otherToolsBtn;
        private Button lastSelectedBtn;

        [Foldout("Item Details UI", true)]
        [SerializeField] private RectTransform itemDetailParent;
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text itemNameTxt;
        [SerializeField] private TMP_Text itemDescriptionTxt;
        [SerializeField] private RectTransform itemRecipePrefab;
        private List<RectTransform> spawnedItemRecipePrefabs = new List<RectTransform> ();
        private Tween itemDetailPanelTween;

        [Foldout("Item Detail Box UI", true)]
        [SerializeField] private Image detailBoxObj;
        [SerializeField] private Canvas objCanvas;
        [SerializeField] private Vector2 detailBoxOffset;
        [SerializeField] private TMP_Text itemNameDetailTxt;

        [Foldout("Inventory UI", true)]
        [SerializeField] private RectTransform inventoryItemParent;
        [SerializeField] private RectTransform inventoryContentPrefab;

        void Start()
        {
            LoadExternalClassInstance();
            SubscribeToEvent(true);

            ShowInventory();
            ShowCraftingItemBtns(true);
            CategoriesBtnAddListener(true);
            StartCoroutine(DetailBoxFollowCursor());
        }

        private void OnDisable()
        {
            CategoriesBtnAddListener(false);
            SubscribeToEvent(false);
        }

        #region Crafting Buttons
        private void ShowCraftingItemBtns(bool _showAll)
        {
            for (int i = 0; i < allCraftingBtn.Length; i++)
            {
                ItemSO itemSO = allCraftingBtn[i].ButtonItemSO;
                Image itemImg = allCraftingBtn[i].transform.GetChild(0).GetComponent<Image>();

                allCraftingBtn[i].transform.GetChild(0).GetComponent<Image>().sprite = itemSO.itemSprite;
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
        private void CategoriesBtnAddListener(bool _isSubscribing)
        {
            if (_isSubscribing)
            {
                allCategoriesBtn.onClick.AddListener(() => { ShowCraftingItemBtns(true); });
                weaponBtn.onClick.AddListener(() => { ShowItemByCategory(ItemTypeEnum.WEAPON); });
                miningToolsBtn.onClick.AddListener(() => { ShowItemByCategory(ItemTypeEnum.MINING_TOOLS); });
                farmingToolsBtn.onClick.AddListener(() => { ShowItemByCategory(ItemTypeEnum.FARMING_TOOLS); });
                otherToolsBtn.onClick.AddListener(() => { ShowItemByCategory(ItemTypeEnum.OTHERS); });
            }
            else
            {
                allCategoriesBtn.onClick.RemoveAllListeners();
                weaponBtn.onClick.RemoveAllListeners();
                miningToolsBtn.onClick.RemoveAllListeners();
                farmingToolsBtn.onClick.RemoveAllListeners();
                otherToolsBtn.onClick.RemoveAllListeners();
            }
        }
        #endregion

        #region Item Detail UI
        public void UpdateItemDetailUI(ItemSO _itemSO, bool _isItemUnlocked)
        {
            ShakeItemDetailPanel();

            foreach (RectTransform child in itemDetailParent)
            {
                child.gameObject.SetActive(_isItemUnlocked);
            }

            itemDescriptionTxt.gameObject.SetActive(!string.IsNullOrWhiteSpace(_itemSO.itemDescription) && _itemSO.isItemUnlocked);

            itemDescriptionTxt.text = _itemSO.itemDescription;
            itemNameTxt.text = _itemSO.itemName;
            itemImage.sprite = _itemSO.itemSprite;


            if (_itemSO == null || !_itemSO.isItemUnlocked) return;

            foreach (RectTransform child in spawnedItemRecipePrefabs)
            {
                Destroy(child.gameObject);
            }
            spawnedItemRecipePrefabs.Clear();

            for (int i = 0; i < _itemSO.itemRecipe.Length; i++)
            {
                RectTransform recipePrefabs = Instantiate(itemRecipePrefab, itemDetailParent);
                spawnedItemRecipePrefabs.Add(recipePrefabs);

                recipePrefabs.GetChild(1).GetComponent<Image>().sprite = _itemSO.itemRecipe[i].rawItemSO.itemSprite;
                recipePrefabs.GetChild(2).GetComponent<TMP_Text>().text = ($"{_itemSO.itemRecipe[i].rawItemSO.itemAmountInInventory} / {_itemSO.itemRecipe[i].amountNeeded}");
            }
        }

        private void ShakeItemDetailPanel()
        {
           if(itemDetailPanelTween != null && itemDetailPanelTween.IsPlaying())
           {
                itemDetailPanelTween.Kill();
                itemDetailPanelTween.Restart();
                return;
           } 
                
           itemDetailPanelTween = itemDetailParent.DOPunchScale(new Vector3(0f, 0f, Random.Range(.5f, 1f)), .25f);
        }
        #endregion

        #region Small Detail Box
        private IEnumerator DetailBoxFollowCursor()
        {
            while (true)
            {
                Vector2 mousePosition;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    objCanvas.transform as RectTransform,
                    Input.mousePosition,
                    objCanvas.worldCamera,
                    out mousePosition
                );
                detailBoxObj.rectTransform.anchoredPosition = mousePosition + detailBoxOffset;
                yield return null;
            }
        }

        public void ToggleDetailBoxObj(bool _isSowing, ItemSO _itemSO)
        {
            detailBoxObj.gameObject.SetActive(_isSowing);
            itemNameDetailTxt.text = _itemSO.isItemUnlocked ? $"{_itemSO.itemName}" : "???";
        }
        #endregion

        #region Inventory UI
        private void ShowInventory()
        {
            foreach (RectTransform child in inventoryItemParent)
            {
                Destroy(child.gameObject);
            }

            foreach (ItemSO items in inventoryManager.OwnedItemList)
            {
                RectTransform spawnedItemRect = Instantiate(inventoryContentPrefab, inventoryItemParent);
                
                InventoryButtonBehaviour invBtn = spawnedItemRect.GetComponent<InventoryButtonBehaviour>();
                invBtn.ButtonItemSO = items;

                Image itemImg = spawnedItemRect.GetChild(0).GetComponent<Image>();
                itemImg.sprite = items.itemSprite;

                TMP_Text itemAmountTxt = spawnedItemRect.GetChild(1).GetComponent<TMP_Text>();
                itemAmountTxt.text = items.itemAmountInInventory.ToString();
                itemAmountTxt.gameObject.SetActive(items.isStackable);
            }
        }
        #endregion

        public void SetLastSelectedButton(Button _button) => lastSelectedBtn = _button;

        public void SelectLastSelectedButton() => lastSelectedBtn.Select();

        public void LoadExternalClassInstance()
        {
            inventoryManager = InventoryManager.Instance;
        }

        private void SubscribeToEvent(bool _isSubscribing)
        {
            if(_isSubscribing)
            {
                CraftingManager.Instance.onItemCrafted += ShowInventory;
            }
            else
            {
                CraftingManager.Instance.onItemCrafted -= ShowInventory;
            }
        }
    }
}
