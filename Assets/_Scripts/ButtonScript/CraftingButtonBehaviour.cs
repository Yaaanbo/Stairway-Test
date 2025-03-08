using StairwayTest.SO;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StairwayTest.Gameplay
{
    public class CraftingButtonBehaviour : ButtonBehaviour
    {
        [SerializeField] private ItemSO buttonItemSO;
        public ItemSO ButtonItemSO => buttonItemSO;

        public override void OnPointerEnter(PointerEventData eventData)
        {
            uiManager.UpdateItemDetailUI(buttonItemSO, buttonItemSO.isItemUnlocked);
            uiManager.ToggleDetailBoxObj(true, buttonItemSO);
            base.OnPointerEnter(eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            uiManager.ToggleDetailBoxObj(false, buttonItemSO);
            base .OnPointerExit(eventData);
        }
    }
}