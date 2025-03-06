using DG.Tweening;
using MyBox;
using StairwayTest.Manager;
using StairwayTest.SO;
using StairwayTest.Utilities.Interface;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace StairwayTest.Gameplay
{
    public class ButtonBehaviour : MonoBehaviour, ILoadExternalClasses, ISelectHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private UIManager uiManager;

        private void Start()
        {
            LoadExternalClassInstance();
        }

        public void LoadExternalClassInstance()
        {
            uiManager = UIManager.Instance;
        }

        public void OnSelect(BaseEventData eventData)
        {
            uiManager.SetLastSelectedButton(this.GetComponent<Button>());
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            this.GetComponent<RectTransform>().DOScale(1.05f, .25f).SetEase(Ease.InBack);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            this.GetComponent<RectTransform>().DOScale(1f, .25f).SetEase(Ease.OutBack);
        }
    }
}
