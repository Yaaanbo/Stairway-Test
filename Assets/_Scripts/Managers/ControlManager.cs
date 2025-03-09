using StairwayTest.Utilities;
using StairwayTest.Utilities.Interface;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StairwayTest.Manager
{
    public class ControlManager : SingletonPersistent<ControlManager>, ILoadExternalClasses
    {
        private const string GAMEPAD_CONTROL_SCHEME_NAME = "Gamepad";

        private UIManager uiManager;

        private PlayerInput playerInput;
        public PlayerInput PlayerInput => playerInput;

        private InputSystem_Actions inputActions;
        public InputSystem_Actions InputActions => inputActions;

        public EventHandler onPauseInput;

        private void Start()
        {
            LoadExternalClassInstance();

            playerInput = this.GetComponent<PlayerInput>();
            inputActions = new InputSystem_Actions();
            
            ToggleInputAction(true);
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
                playerInput.onControlsChanged += OnControlSchemeChanged;
                inputActions.Player.Pause.performed += PauseInput;
            }
            else
            {
                playerInput.onControlsChanged -= OnControlSchemeChanged;
                inputActions.Player.Pause.performed -= PauseInput;
            }
        }

        private void OnControlSchemeChanged(PlayerInput _playerInput)
        {
            if(IsUsingGamepad())
            {
                uiManager.SelectLastSelectedButton();
            }
        }

        public bool IsUsingGamepad() => playerInput.currentControlScheme == GAMEPAD_CONTROL_SCHEME_NAME;

        private void PauseInput(InputAction.CallbackContext _context)
        {
            onPauseInput?.Invoke(this, EventArgs.Empty);
        }

        public void ToggleInputAction(bool _isEnabling)
        {
            if (_isEnabling)
                inputActions.Enable();
            else
                inputActions.Disable();
        }

        public void LoadExternalClassInstance()
        {
            uiManager = UIManager.Instance;
        }
    }
}
