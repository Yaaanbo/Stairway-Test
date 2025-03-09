using DG.Tweening;
using UnityEngine;

namespace StairwayTest.Gameplay
{
    public class InventoryButtonBehaviour : CraftingButtonBehaviour
    {
        protected override void OnButtonClick()
        {
            uiManager.UpdateItemDetailUI(ButtonItemSO, ButtonItemSO.isItemUnlocked);
            this.GetComponent<RectTransform>().DOPunchScale(new Vector3(-.15f, -.15f), .25f);
        }
    }
}
