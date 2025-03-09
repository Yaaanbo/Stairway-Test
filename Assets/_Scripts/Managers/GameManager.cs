using MyBox;
using StairwayTest.SO;
using StairwayTest.Utilities;
using StairwayTest.Utilities.Interface;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace StairwayTest.Manager
{
    public class GameManager : SingletonPersistent<GameManager>, ILoadExternalClasses
    {
        private ControlManager controlManager;

        [Foldout("Item Scriptable Objects", true)]
        [SerializeField] private List<ItemSO> allItemSO = new List<ItemSO>();
        public List<ItemSO> AllItemSO => allItemSO;

        //Pause Manager
        private bool isPaused = false;

        public EventHandler<OnGamePausedArgs> onGamePaused;
        public class OnGamePausedArgs : EventArgs
        {
            public bool isPaused;

            public OnGamePausedArgs(bool _isPaused)
            {
                isPaused = _isPaused;
            }
        }

        private void Start()
        {
            LoadExternalClassInstance();
            SubscribeToEvent(true);
        }

        private void OnDisable()
        {
            SubscribeToEvent(false);
        }

        private void SubscribeToEvent(bool _isSubscribing)
        {
            if (_isSubscribing)
            {
                controlManager.onPauseInput += OnPauseInputPressed;
            }
            else
            {
                controlManager.onPauseInput -= OnPauseInputPressed;
            }
        }

        private void OnPauseInputPressed(object _sender, EventArgs _e)
        {
            isPaused = !isPaused;
            onGamePaused?.Invoke(this, new OnGamePausedArgs(isPaused));
        }

        public void LoadExternalClassInstance()
        {
            controlManager = ControlManager.Instance;
        }
    }
}
