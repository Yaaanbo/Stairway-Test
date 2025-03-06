using StairwayTest.Utilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StairwayTest.Manager
{
    public class ControlManager : SingletonPersistent<ControlManager>
    {
        private const string GAMEPAD_CONTROL_SCHEME_NAME = "Gamepad";

        private PlayerInput playerInput;
        private ButtonManager buttonManager;

        private void Start()
        {
            buttonManager = ButtonManager.Instance;

            playerInput = this.GetComponent<PlayerInput>();

            playerInput.onControlsChanged += OnControlSchemeChanged;
        }

        private void OnDisable()
        {
            playerInput.onControlsChanged -= OnControlSchemeChanged;
        }

        private void OnControlSchemeChanged(PlayerInput _playerInput)
        {
            if(_playerInput.currentControlScheme == GAMEPAD_CONTROL_SCHEME_NAME)
            {
                buttonManager.SelectLastSelectedButton();
            }
        }
    }
}
