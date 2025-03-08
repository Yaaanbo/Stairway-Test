using UnityEngine.EventSystems;

namespace StairwayTest.Gameplay
{
    public class InventoryButtonBehaviour : CraftingButtonBehaviour
    {
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            uiManager.ToggleDetailBoxObj(false, buttonItemSO);
        }
    }
}
