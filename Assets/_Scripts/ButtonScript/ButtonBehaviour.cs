using DG.Tweening;
using StairwayTest.Manager;
using StairwayTest.Utilities.Interface;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace StairwayTest.Gameplay
{
    public class ButtonBehaviour : MonoBehaviour, ILoadExternalClasses, ISelectHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        protected UIManager uiManager;
        private Tween punchTween;

        private void Start()
        {
            LoadExternalClassInstance();
        }

        public virtual void LoadExternalClassInstance()
        {
            uiManager = UIManager.Instance;
        }

        public void OnSelect(BaseEventData eventData)
        {
            uiManager.SetLastSelectedButton(this.GetComponent<Button>());
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            this.GetComponent<RectTransform>().DOScale(1.05f, .25f).SetEase(Ease.InBack);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            this.GetComponent<RectTransform>().DOScale(1f, .25f).SetEase(Ease.OutBack);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (punchTween != null && punchTween.IsPlaying()) return;

            punchTween = this.GetComponent<RectTransform>().DOPunchScale(new Vector3(-.15f, -.15f), .25f);
        }
    }
}
