using StairwayTest.Manager;
using StairwayTest.SO;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StairwayTest.Gameplay
{
    public class CraftingButtonBehaviour : ButtonBehaviour
    {
        private CraftingManager craftManager;
        [SerializeField] protected ItemSO buttonItemSO;
        public ItemSO ButtonItemSO
        {
            get { return buttonItemSO; }
            set { buttonItemSO = value; }
        }

        public override void LoadExternalClassInstance()
        {
            base.LoadExternalClassInstance();
            craftManager = CraftingManager.Instance;
        }

        protected override void OnButtonSelectedOrPointerEnter()
        {
            base.OnButtonSelectedOrPointerEnter();
            uiManager.UpdateItemDetailUI(buttonItemSO, buttonItemSO.isItemUnlocked);
            uiManager.ShowDetailBoxObj(buttonItemSO);
        }

        protected override void OnButtonDeselectedOrPointerExit()
        {
            base.OnButtonDeselectedOrPointerExit();
            uiManager.HideDetailBoxObj();
        }

        protected override void OnButtonClick()
        {
            base.OnButtonClick();
            craftManager.CraftItem(buttonItemSO);
            uiManager.UpdateItemDetailUI(ButtonItemSO, ButtonItemSO.isItemUnlocked);
        }
    }
}