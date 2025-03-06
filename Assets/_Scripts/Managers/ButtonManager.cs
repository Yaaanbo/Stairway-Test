using StairwayTest.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace StairwayTest.Manager
{
    public class ButtonManager : SingletonPersistent<ButtonManager>
    {
        private Button lastSelectedBtn;


        private void SetCraftButtonIcon()
        {

        }

        public void SetLastSelectedButton(Button _button) => lastSelectedBtn = _button;

        public void SelectLastSelectedButton() => lastSelectedBtn.Select();
    }
}
