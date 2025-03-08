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

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            uiManager.UpdateItemDetailUI(buttonItemSO, buttonItemSO.isItemUnlocked);
            uiManager.ToggleDetailBoxObj(true, buttonItemSO);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            uiManager.ToggleDetailBoxObj(false, buttonItemSO);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            craftManager.CraftItem(buttonItemSO);
            uiManager.UpdateItemDetailUI(ButtonItemSO, ButtonItemSO.isItemUnlocked);
        }
    }
}