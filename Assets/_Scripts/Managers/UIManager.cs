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
        private GameManager gameManager;

        [Foldout("Crafting UI", true)]
        [SerializeField] private CraftingButtonBehaviour[] allCraftingBtn;
        [SerializeField] private ItemTypeEnum itemCategoryToDisplay;
        [SerializeField] private Color itemLockedColor;
        [SerializeField] private Color itemUnlockedColor;

        [Foldout("Item Categories", true)]
        [SerializeField] private Button allCategoriesBtn;
        [SerializeField] private Button weaponBtn;
        [SerializeField] private Button miningToolsBtn;
        [SerializeField] private Button farmingToolsBtn;
        [SerializeField] private Button otherToolsBtn;
        private Button lastSelectedBtn;

        [Foldout("Item Details", true)]
        [SerializeField] private RectTransform itemDetailParent;
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text itemNameTxt;
        [SerializeField] private TMP_Text itemDescriptionTxt;
        [SerializeField] private RectTransform itemRecipePrefab;
        private List<RectTransform> spawnedItemRecipePrefabs = new List<RectTransform> ();
        private Tween itemDetailPanelTween;

        [Foldout("Item Detail Box", true)]
        [SerializeField] private Image detailBoxObj;
        [SerializeField] private Canvas objCanvas;
        [SerializeField] private Vector2 detailBoxOffset;
        [SerializeField] private TMP_Text itemNameDetailTxt;

        void Start()
        {
            LoadExternalClassInstance();
            ShowCraftingItemBtns(true);
            CategoriesBtnAddListener(true);
            StartCoroutine(DetailBoxFollowCursor());
        }

        private void OnDisable()
        {
            CategoriesBtnAddListener(false);
        }

        public void LoadExternalClassInstance()
        {
            gameManager = GameManager.Instance;
        }

        private void ShowCraftingItemBtns(bool _showAll)
        {
            for (int i = 0; i < allCraftingBtn.Length; i++)
            {
                ItemSO itemSO = allCraftingBtn[i].ButtonItemSO;
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

        public void UpdateItemDetailUI(ItemSO _itemSO, bool _isItemUnlocked)
        {
            ShakeItemDetailPanel();

            foreach (RectTransform child in itemDetailParent)
            {
                child.gameObject.SetActive(_isItemUnlocked);
            }

            if (string.IsNullOrWhiteSpace(_itemSO.itemDescription))
                itemDescriptionTxt.gameObject.SetActive(false);

            itemDescriptionTxt.text = _itemSO.itemDescription;
            itemNameTxt.text = _itemSO.itemName;
            itemImage.sprite = _itemSO.itemSprite;


            if (_itemSO == null) return;

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
                recipePrefabs.GetChild(2).GetComponent<TMP_Text>().text = ($"0 / {_itemSO.itemRecipe[i].amountNeeded}");
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

        public void SetLastSelectedButton(Button _button) => lastSelectedBtn = _button;

        public void SelectLastSelectedButton() => lastSelectedBtn.Select();
    }
}
