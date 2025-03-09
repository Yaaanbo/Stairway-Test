using DG.Tweening;
using MyBox;
using StairwayTest.Manager;
using StairwayTest.Utilities.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace StairwayTest.Gameplay
{
    public class UIAnimation : MonoBehaviour, ILoadExternalClasses
    {
        private GameManager gameManager;

        [Foldout("Animation Configs", true)]
        [SerializeField] private float animDuration = .25f;
        [SerializeField] private float fadeDuration = .5f;
        [SerializeField] private Ease easeType;
        private const float multiplier = 1000f;
        private Vector2 topPanelOriginalPos, leftPanelOriginalPos, rightPanelOriginalPos, midPanelOriginalPos;

        [Foldout("Canvas Groups", true)]
        [SerializeField] private CanvasGroup topPanel;
        [SerializeField] private CanvasGroup bottomPanel;
        [SerializeField] private CanvasGroup blurBackgroundImg;

        [Foldout("Bottom Panel Childs", true)]
        [SerializeField] private RectTransform leftPanel;
        [SerializeField] private RectTransform midPanel;
        [SerializeField] private RectTransform rightPanel;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            LoadExternalClassInstance();
            SetAllPanelOriginalPos();
            FadeAllPanels(false);
            
            SubscribeToEvent(true);
        }

        private void OnDisable()
        {
            SubscribeToEvent(false);
        }

        #region Panel animation functions
        private void SetAllPanelOriginalPos()
        {
            topPanelOriginalPos = topPanel.GetComponent<RectTransform>().anchoredPosition;
            leftPanelOriginalPos = leftPanel.anchoredPosition;
            rightPanelOriginalPos = rightPanel.anchoredPosition;
            midPanelOriginalPos = midPanel.anchoredPosition;
        }

        private void FadeAllPanels(bool _isFadingIn)
        {
            //Canvas Group Alpha
            FadeCanvasGroup(topPanel, _isFadingIn);
            FadeCanvasGroup(bottomPanel, _isFadingIn);
            FadeCanvasGroup(blurBackgroundImg, _isFadingIn);

            //Top panel
            RectTransform topPanelRectTransform = topPanel.GetComponent<RectTransform>();
            FadeRectTransform(topPanelRectTransform, topPanelOriginalPos, new Vector2(topPanelRectTransform.anchoredPosition.x, multiplier), _isFadingIn);

            //Left panel
            FadeRectTransform(leftPanel, leftPanelOriginalPos, new Vector2(-multiplier, leftPanel.anchoredPosition.y), _isFadingIn);

            //Right panel
            FadeRectTransform(rightPanel, rightPanelOriginalPos, new Vector2(multiplier, rightPanel.anchoredPosition.y), _isFadingIn);

            //Mid panel
            FadeRectTransform(midPanel, midPanelOriginalPos, new Vector2(midPanel.anchoredPosition.x, -multiplier), _isFadingIn);
        }

        private void FadeCanvasGroup(CanvasGroup _canvasGroup, bool _isFadingIn)
        {
            _canvasGroup.alpha = _isFadingIn ? 0f : 1f;
            _canvasGroup.DOFade(_isFadingIn ? 1f : 0f, fadeDuration);
        }

        private void FadeRectTransform(RectTransform _rectTransform, Vector2 _originalPos, Vector2 _startPos, bool _isFadingIn)
        {
            _rectTransform.anchoredPosition = _isFadingIn ? _startPos : _originalPos;
            _rectTransform.DOAnchorPos(_isFadingIn ? _originalPos : _startPos, animDuration).SetEase(easeType);
        }
        #endregion

        #region Events
        private void SubscribeToEvent(bool _isSubscribing)
        {
            if (_isSubscribing)
            {
                gameManager.onGamePaused += OnGamePausedUI;
            }
            else
            {
                gameManager.onGamePaused -= OnGamePausedUI;
            }
        }

        private void OnGamePausedUI(object _sender, GameManager.OnGamePausedArgs _e)
        {
            FadeAllPanels(_e.isPaused);
        }
        #endregion

        public void LoadExternalClassInstance()
        {
            gameManager = GameManager.Instance;
        }
    }
}
