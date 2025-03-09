using DG.Tweening;
using StairwayTest.Manager;
using StairwayTest.Utilities.Interface;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace StairwayTest.Gameplay
{
    public class ButtonBehaviour : MonoBehaviour, ILoadExternalClasses, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
    {
        protected UIManager uiManager;
        private Tween punchTween;

        private void Start()
        {
            LoadExternalClassInstance();
            SubscribeToEvent(true);
        }

        private void OnDisable()
        {
            SubscribeToEvent(false);
        }

        public virtual void LoadExternalClassInstance()
        {
            uiManager = UIManager.Instance;
        }

        #region Button Event Handler
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnButtonSelectedOrPointerEnter();
        }

        public void OnSelect(BaseEventData eventData)
        {
            OnButtonSelectedOrPointerEnter();
        }

        public void OnDeselect(BaseEventData eventData)
        {
            OnButtonDeselectedOrPointerExit();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnButtonDeselectedOrPointerExit();
        }

        protected virtual void OnButtonSelectedOrPointerEnter()
        {
            uiManager.SetLastSelectedButton(this.GetComponent<Button>());
            this.GetComponent<RectTransform>().DOScale(1.15f, .25f).SetEase(Ease.InBack);
        }

        protected virtual void OnButtonDeselectedOrPointerExit()
        {
            this.GetComponent<RectTransform>().DOScale(1f, .25f).SetEase(Ease.OutBack);
        }

        protected virtual void OnButtonClick()
        {
            if (punchTween != null && punchTween.IsPlaying()) return;

            punchTween = this.GetComponent<RectTransform>().DOPunchScale(new Vector3(-.15f, -.15f), .25f);
        }
        #endregion

        private void SubscribeToEvent(bool _isSubscribing)
        {
            if (_isSubscribing)
            {
                this.GetComponent<Button>().onClick.AddListener(OnButtonClick);
            }
            else
            {
                this.GetComponent<Button>().onClick.RemoveAllListeners();
            }
        }
    }
}
