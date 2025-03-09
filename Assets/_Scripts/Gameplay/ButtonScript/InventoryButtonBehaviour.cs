using UnityEngine.EventSystems;

namespace StairwayTest.Gameplay
{
    public class InventoryButtonBehaviour : CraftingButtonBehaviour
    {
        protected override void OnButtonClick()
        {
            uiManager.HideDetailBoxObj();
            uiManager.UpdateItemDetailUI(ButtonItemSO, ButtonItemSO.isItemUnlocked);
        }
    }
}
