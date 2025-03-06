using StairwayTest.Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace StairwayTest.Gameplay
{
    public class ButtonBehaviour : MonoBehaviour, ISelectHandler
    {
        private ButtonManager buttonManager;

        private void Start()
        {
            buttonManager = ButtonManager.Instance;
        }

        public void OnSelect(BaseEventData eventData)
        {
            buttonManager.SetLastSelectedButton(this.GetComponent<Button>());
        }
    }
}
